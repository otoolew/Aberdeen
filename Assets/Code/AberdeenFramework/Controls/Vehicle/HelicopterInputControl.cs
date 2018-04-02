using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterInputControl : MonoBehaviour {

    
    public float m_Speed = 12f;                 // How fast the copter moves forward and back.
    public float m_TurnSpeed = 180f;            // How fast the copter turns in degrees per second.
    private Rigidbody m_Rigidbody;              // Reference used to move the copter.
    private float m_VerticalInputValue;         // The current value of the movement input.
    private float m_RotationInputValue;         // The current value of the turn input.
    private float m_LiftInputValue;             // The current value of the turn input.

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_VerticalInputValue = 0f;
        m_RotationInputValue = 0f;
        m_LiftInputValue = 0f;
    }

    private void Update()
    {
        // Store the value of both input axes.
        m_VerticalInputValue = Input.GetAxis("Vertical");
        m_RotationInputValue = Input.GetAxis("Horizontal");
        m_LiftInputValue = Input.GetAxis("Lift");
    }


    private void EngineAudio()
    {
        //// If there is no input (the copter is stationary)...
        //if (Mathf.Abs(m_VerticalInputValue) < 0.1f && Mathf.Abs(m_RotationInputValue) < 0.1f)
        //{
        //    // ... and if the audio source is currently playing the driving clip...
        //    if (m_MovementAudio.clip == m_EngineDriving)
        //    {
        //        // ... change the clip to idling and play it.
        //        m_MovementAudio.clip = m_EngineIdling;
        //        m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
        //        m_MovementAudio.Play();
        //    }
        //}
        //else
        //{
        //    // Otherwise if the copter is moving and if the idling clip is currently playing...
        //    if (m_MovementAudio.clip == m_EngineIdling)
        //    {
        //        // ... change the clip to driving and play.
        //        m_MovementAudio.clip = m_EngineDriving;
        //        m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
        //        m_MovementAudio.Play();
        //    }
        //}
    }


    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Move();
        Turn();
        Lift();
    }


    private void Move()
    {
        // Create a vector in the direction the copter is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_VerticalInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_RotationInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
    private void Lift()
    {
        // Create a vector in the direction the copter is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 lift = transform.up * m_LiftInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + lift);
    }
}
