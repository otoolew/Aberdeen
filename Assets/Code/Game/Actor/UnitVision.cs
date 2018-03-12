
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour {

    public List<GameObject> objectList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        objectList.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        objectList.Remove(other.gameObject);
    }
}
