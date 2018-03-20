using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeaponBehavior : MonoBehaviour
{
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
            Debug.Log("RayHit " + rayHit.collider.name);
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * Range);
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
