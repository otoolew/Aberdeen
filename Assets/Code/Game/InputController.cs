using Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityInput = UnityEngine.Input;

public class InputController : Singleton<InputController>
{

    private List<Interactive> Selections = new List<Interactive>();
    /// <summary>
    /// How quickly flick velocity is accumulated with movements
    /// </summary>
    const float k_FlickAccumulationFactor = 0.8f;

    /// <summary>
    /// How far mouse must move before starting a drag
    /// </summary>
    public float dragThresholdMouse;

    /// <summary>
    /// How long before a touch can no longer be considered a tap
    /// </summary>
    public float tapTime = 0.2f;

    /// <summary>
    /// How long before a touch is considered a hold
    /// </summary>
    public float holdTime = 0.8f;

    /// <summary>
    /// Sensitivity of mouse-wheel based zoom
    /// </summary>
    public float mouseWheelSensitivity = 1.0f;

    /// <summary>
    /// How many mouse buttons to track
    /// </summary>
    public int trackMouseButtons = 2;

    /// <summary>
    /// Flick movement threshold
    /// </summary>
    public float flickThreshold = 2f;


    /// <summary>
    /// Mouse button info
    /// </summary>
    List<MouseButtonInfo> m_MouseInfo;


    /// <summary>
    /// Tracks if any of the mouse buttons were pressed this frame
    /// </summary>
    public bool mouseButtonPressedThisFrame { get; private set; }

    /// <summary>
    /// Tracks if the mouse moved this frame
    /// </summary>
    public bool mouseMovedOnThisFrame { get; private set; }

    /// <summary>
    /// Current mouse pointer info
    /// </summary>
    public PointerInfo basicMouseInfo { get; private set; }

    /// <summary>
    /// Event called when a pointer press is detected
    /// </summary>
    public event Action<PointerActionInfo> pressed;

    /// <summary>
    /// Event called when a pointer is released
    /// </summary>
    public event Action<PointerActionInfo> released;

    /// <summary>
    /// Event called when a pointer is tapped
    /// </summary>
    public event Action<PointerActionInfo> tapped;

    /// <summary>
    /// Event called when a drag starts
    /// </summary>
    public event Action<PointerActionInfo> startedDrag;

    /// <summary>
    /// Event called when a pointer is dragged
    /// </summary>
    public event Action<PointerActionInfo> dragged;

    /// <summary>
    /// Event called when a pointer starts a hold
    /// </summary>
    public event Action<PointerActionInfo> startedHold;

    /// <summary>
    /// Event called when the user scrolls the mouse wheel
    /// </summary>
    public event Action<WheelInfo> spunWheel;

    /// <summary>
    /// Event called whenever the mouse is moved
    /// </summary>
    public event Action<PointerInfo> mouseMoved;

    protected override void Awake()
    {
        base.Awake();

        // Mouse specific initialization
        if (UnityInput.mousePresent)
        {
            m_MouseInfo = new List<MouseButtonInfo>();
            basicMouseInfo = new MouseCursorInfo { currentPosition = UnityInput.mousePosition };

            for (int i = 0; i < trackMouseButtons; ++i)
            {
                m_MouseInfo.Add(new MouseButtonInfo
                {
                    currentPosition = UnityInput.mousePosition,
                    mouseButtonId = i
                });
            }
            if (Selections.Count > 0)
            {
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    foreach (var sel in Selections)
                    {
                        if (sel != null) sel.Deselect();
                    }
                    Selections.Clear();
                }
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
                return;

            var interact = hit.transform.GetComponent<Interactive>();
            if (interact == null)
                return;

            Selections.Add(interact);
            interact.Select();
        }

        UnityInput.simulateMouseWithTouches = false;
    }

    /// <summary>
    /// Update all input
    /// </summary>
    void Update()
    {
        if (basicMouseInfo != null)
        {
            // Mouse was detected as present
            UpdateMouse();
        }
        // Handle touches
    }

    /// <summary>
    /// Perform logic to update mouse/pointing device
    /// </summary>
    void UpdateMouse()
    {
        basicMouseInfo.previousPosition = basicMouseInfo.currentPosition;
        basicMouseInfo.currentPosition = UnityInput.mousePosition;
        basicMouseInfo.delta = basicMouseInfo.currentPosition - basicMouseInfo.previousPosition;
        mouseMovedOnThisFrame = basicMouseInfo.delta.sqrMagnitude >= Mathf.Epsilon;
        mouseButtonPressedThisFrame = false;

        // Move event
        if (basicMouseInfo.delta.sqrMagnitude > Mathf.Epsilon)
        {
            if (mouseMoved != null)
            {
                mouseMoved(basicMouseInfo);
            }
        }
        // Button events
        for (int i = 0; i < trackMouseButtons; ++i)
        {
            MouseButtonInfo mouseButton = m_MouseInfo[i];
            mouseButton.delta = basicMouseInfo.delta;
            mouseButton.previousPosition = basicMouseInfo.previousPosition;
            mouseButton.currentPosition = basicMouseInfo.currentPosition;
            if (UnityInput.GetMouseButton(i))
            {
                if (!mouseButton.isDown)
                {
                    // First press
                    mouseButtonPressedThisFrame = true;
                    mouseButton.isDown = true;
                    mouseButton.startPosition = UnityInput.mousePosition;
                    mouseButton.startTime = Time.realtimeSinceStartup;
                    mouseButton.startedOverUI = EventSystem.current.IsPointerOverGameObject(-mouseButton.mouseButtonId - 1);

                    // Reset some stuff
                    mouseButton.totalMovement = 0;
                    mouseButton.isDrag = false;
                    mouseButton.wasHold = false;
                    mouseButton.isHold = false;
                    mouseButton.flickVelocity = Vector2.zero;

                    if (pressed != null)
                    {
                        pressed(mouseButton);
                    }
                }
                else
                {
                    float moveDist = mouseButton.delta.magnitude;
                    // Dragging?
                    mouseButton.totalMovement += moveDist;
                    if (mouseButton.totalMovement > dragThresholdMouse)
                    {
                        bool wasDrag = mouseButton.isDrag;

                        mouseButton.isDrag = true;
                        if (mouseButton.isHold)
                        {
                            mouseButton.wasHold = mouseButton.isHold;
                            mouseButton.isHold = false;
                        }

                        // Did it just start now?
                        if (!wasDrag)
                        {
                            if (startedDrag != null)
                            {
                                startedDrag(mouseButton);
                            }
                        }
                        if (dragged != null)
                        {
                            dragged(mouseButton);
                        }

                        // Flick?
                        if (moveDist > flickThreshold)
                        {
                            mouseButton.flickVelocity =
                                (mouseButton.flickVelocity * (1 - k_FlickAccumulationFactor)) +
                                (mouseButton.delta * k_FlickAccumulationFactor);
                        }
                        else
                        {
                            mouseButton.flickVelocity = Vector2.zero;
                        }
                    }
                    else
                    {
                        // Stationary?
                        if (!mouseButton.isHold &&
                            !mouseButton.isDrag &&
                            Time.realtimeSinceStartup - mouseButton.startTime >= holdTime)
                        {
                            mouseButton.isHold = true;
                            if (startedHold != null)
                            {
                                startedHold(mouseButton);
                            }
                        }
                    }
                }
            }
            else // Mouse button not up
            {
                if (mouseButton.isDown) // Released
                {
                    mouseButton.isDown = false;
                    // Quick enough (with no drift) to be a tap?
                    if (!mouseButton.isDrag &&
                        Time.realtimeSinceStartup - mouseButton.startTime < tapTime)
                    {
                        if (tapped != null)
                        {
                            tapped(mouseButton);
                        }
                    }
                    if (released != null)
                    {
                        released(mouseButton);
                    }
                }
            }
        }

        // Mouse wheel
        if (Mathf.Abs(UnityInput.GetAxis("Mouse ScrollWheel")) > Mathf.Epsilon)
        {
            if (spunWheel != null)
            {
                spunWheel(new WheelInfo
                {
                    scrollAmount = UnityInput.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity
                });
            }
        }
    }

    

}
