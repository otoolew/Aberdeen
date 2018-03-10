using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRayCast : MonoBehaviour {

    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit rayHit;
    public Transform FirePoint;
    public FloatVariable FireRate;
    public FloatVariable RayRange;
    public LayerMask LayerRayMask;
    float timer;
    float effectDuration = 1f;

    private void Start()
    {
        lineRenderer = FirePoint.GetComponent<LineRenderer>();
    }
    void Update()
    {
        // Add the time since Update was last called to the timer.
        timer += Time.deltaTime;

        // If the Fire1 button is being press and it's time to fire...
        if (Input.GetMouseButton(0) && timer >= FireRate.Value)
        {
            Fire();
        }
        if (timer >= FireRate.Value * effectDuration)
        {
            // ... disable the effects.
            lineRenderer.enabled = false;
        }
    }

    private void Fire()
    {
        timer = 0f;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);       
        ray.origin = transform.position;
        ray.direction = transform.forward;

        if (Physics.Raycast(ray, out rayHit, RayRange.Value, LayerRayMask))
        {
            Debug.Log("RayHit " + rayHit.collider.name);
            lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, ray.origin + ray.direction * RayRange.Value);
        }
    }

}
