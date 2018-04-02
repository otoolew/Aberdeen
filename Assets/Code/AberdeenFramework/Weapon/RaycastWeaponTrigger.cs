﻿using Core.Health;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWeaponTrigger : MonoBehaviour {
    //Ray ray;
    RaycastHit rayHit;
    [HideInInspector] public int weaponDamage = 1;                         
    [HideInInspector] public float weaponRange = 50f;                   
    //[HideInInspector] public LineRenderer lineRenderer;                    
    public Transform FirePoint;
    ParticleSystem particleEffects;                    
    public LayerMask LayerRayMask;
    float timer;
    float effectDuration = 1f;
    private WaitForSeconds shotDuration = new WaitForSeconds(.1f);     // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible.


    public void Initialize()
    {
        //Get and store a reference to our LineRenderer component
        //lineRenderer = FirePoint.GetComponentInChildren<LineRenderer>();
        particleEffects = FirePoint.GetComponentInChildren<ParticleSystem>();
    }

    public void Fire()
    {
        //lineRenderer.enabled = true;
        //lineRenderer.SetPosition(0, FirePoint.transform.position);
        Ray ray = new Ray()
        {
            origin = FirePoint.transform.position,
            direction = FirePoint.transform.forward
        };
        StartCoroutine(ShotEffect());
        if (Physics.Raycast(ray, out rayHit, weaponRange, LayerRayMask))
        {
            Debug.Log("RayHit " + rayHit.collider.name);
            DamageableBehaviour damageHandler = rayHit.collider.GetComponent<DamageableBehaviour>();           
            rayHit.collider.GetComponentInParent<DamageableBehaviour>().TakeDamage(weaponDamage);
            //lineRenderer.SetPosition(1, rayHit.point);
        }
        else
        {
            //lineRenderer.SetPosition(1, ray.origin + ray.direction * weaponRange);
        }
    }

    private IEnumerator ShotEffect()
    {

        //Turn on our line renderer
        //lineRenderer.enabled = true;
        particleEffects.Play();
        //Wait for .07 seconds
        yield return shotDuration;

        //Deactivate our line renderer after waiting
        //lineRenderer.enabled = false;
    }
}
