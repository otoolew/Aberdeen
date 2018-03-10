// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.Events;
public class GameEventListener : MonoBehaviour {

    [Tooltip("Event to register with.")]
    public GameEvent Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent Response;

    /// <summary>
    /// When Enabled the event registeres itself
    /// </summary>
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    /// <summary>
    /// When Disabled the event registeres itself
    /// </summary>
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    /// <summary>
    /// Methoded called when revent is raised
    /// </summary>
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
