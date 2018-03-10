
// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Moddified by : Bill O'Toole
// Date:   03/07/18
// ----------------------------------------------------------------------------
using System;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

    [Serializable]
    public class MoveAxis
    {
        public KeyCode Positive;
        public KeyCode Negative;

        public MoveAxis(KeyCode positive, KeyCode negative)
        {
            Positive = positive;
            Negative = negative;
        }

        public static implicit operator float(MoveAxis axis)
        {
            return (Input.GetKey(axis.Positive)
                ? 1.0f : 0.0f) -
                (Input.GetKey(axis.Negative)
                ? 1.0f : 0.0f);
        }
    }

    public FloatVariable MoveRate;
    public MoveAxis Horizontal = new MoveAxis(KeyCode.D, KeyCode.A);
    public MoveAxis Vertical = new MoveAxis(KeyCode.W, KeyCode.S);
    public MoveAxis ZedAxis = new MoveAxis(KeyCode.E, KeyCode.Q);
    public MoveAxis Pitch = new MoveAxis(KeyCode.Z, KeyCode.C);

    private void Update()
    {
        Vector3 moveNormal = new Vector3(Horizontal, Vertical, ZedAxis).normalized;
        
        transform.position += moveNormal * Time.deltaTime * MoveRate.Value;
        //transform.rotation += moveNormal * Time.deltaTime * MoveRate.Value;
    }
}
