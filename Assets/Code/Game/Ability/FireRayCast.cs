using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRayCast : MonoBehaviour {

    LineRenderer lineRenderer;
    public Transform FirePoint;
    public int WeaponRange;
    private WaitForSeconds effectDuration = new WaitForSeconds(.07f);
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        
    }

    private void Fire()
    {
        //Declare a raycast hit to store information about what our raycast has hit.
        RaycastHit hit;
        //Start our ShotEffect coroutine to turn our laser line on and off
        StartCoroutine(ShotEffect());
        //Set the start position for our visual effect for our laser to the position of gunEnd
        lineRenderer.SetPosition(0, FirePoint.position);

        //Check if our raycast has hit anything
        if (Physics.Raycast(FirePoint.position, FirePoint.transform.forward, out hit, WeaponRange))
        {
            //Set the end position for our laser line 
            lineRenderer.SetPosition(1, hit.point);
            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            //if we did not hit anything, set the end of the line to a position directly away from
            lineRenderer.SetPosition(1, FirePoint.transform.forward * WeaponRange);
        }
    }

    private IEnumerator ShotEffect()
    {

        //Turn on our line renderer
        lineRenderer.enabled = true;
        //Wait for .07 seconds
        yield return effectDuration;

        //Deactivate our line renderer after waiting
        lineRenderer.enabled = false;
    }
}
