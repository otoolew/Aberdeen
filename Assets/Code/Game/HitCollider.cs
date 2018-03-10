using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitCollider : MonoBehaviour
{
    public string Location;
    public UnityEvent DamageEvent;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit "+Location);
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();

        if (damage != null)
        {
            DamageEvent.Invoke();
        }
  
    }
}
