
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour {

    // List on InSight GameObjects
    public List<GameObject> objectsInSight = new List<GameObject>();
    public GameObject currentTarget;


    // Vision Checks are done here. If they pass, add the gameobject to the target list.
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I SENSE someone is close!");
        // Object Enters

        // Object RayCast

        Debug.Log("I CAN see you!");
        objectsInSight.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("I CANNOT see you!");
        objectsInSight.Add(other.gameObject);
    }

    public float DistanceFromTarget()
    {
        float result = 0f;
        return result;
    }
    public GameObject SetCurrentTarget(GameObject target)
    {
        return currentTarget.gameObject;
    }


}
