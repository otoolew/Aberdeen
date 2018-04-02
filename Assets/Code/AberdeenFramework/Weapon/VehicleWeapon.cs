using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleWeapon : MonoBehaviour {
    public float Damage;
    public float Range;
    public float Rate;
    public float EffectDuration;
    public Transform FirePoint;
    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit rayHit;
    public LayerMask HitMask;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to our LineRenderer component
        lineRenderer = FirePoint.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireWeapon();
        }
        if (Input.GetMouseButton(1))
        {
            Aiming();
        }      
    }

    public void FireWeapon()
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, FirePoint.transform.position);
        ray.origin = FirePoint.transform.position;
        ray.direction = FirePoint.transform.forward;
        StartCoroutine(ShotEffect());
        if (Physics.Raycast(ray, out rayHit, Range, HitMask))
        {
            //Debug.Log("RayHit " + rayHit.collider.name);
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * Range);
        }
    }
    void Aiming()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, transform.position.y, Input.mousePosition.z);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out rayHit, 100, HitMask))
        {
            //var lookPos = rayHit.point - transform.position;
            //lookPos.y = 0;
            //Quaternion rotation = Quaternion.LookRotation(lookPos);
            //transform.rotation = rotation;
            Vector3 hitPoint = rayHit.point;
            Vector3 vector = new Vector3(0, 1, 0);
            var newPoint = hitPoint + vector;
            FirePoint.transform.LookAt(newPoint);
        }

    }
    private IEnumerator ShotEffect()
    {

        //Turn on our line renderer
        lineRenderer.enabled = true;
        //Wait for .07 seconds
        yield return EffectDuration;

        //Deactivate our line renderer after waiting
        lineRenderer.enabled = false;
    }
}
