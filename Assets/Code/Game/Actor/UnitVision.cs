using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVision : MonoBehaviour {
    LineRenderer lineRenderer;
    Ray ray;
    RaycastHit rayHit;
    public Transform visionPoint;
    public FloatVariable FireRate;
    public FloatVariable RayRange;
    public LayerMask LayerRayMask;
    float timer;
    float effectDuration = 1f;

    private void Start()
    {
        lineRenderer = visionPoint.GetComponent<LineRenderer>();
    }
    public void ScanForTarget()
    {
        timer = 0f;
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, visionPoint.transform.position);
        ray.origin = visionPoint.transform.position;
        ray.direction = visionPoint.transform.forward;
        StartCoroutine(Scanning());
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

    public void ScanForDestination()
    {

    }
    private IEnumerator Scanning()
    {
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
    }


}
