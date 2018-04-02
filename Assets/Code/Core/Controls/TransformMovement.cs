using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour {
    public FloatVariable MoveRate;
    private void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * MoveRate.Value;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * MoveRate.Value;

        transform.Translate(x, 0, z);
    }
}
