using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColliderSO : ScriptableObject {
    public string Location;
    public FloatVariable HP;
    public bool ResetHP;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + Location);
    }
}
