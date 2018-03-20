﻿// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;

public class UnitHealth : MonoBehaviour {
    public FloatVariable HP;
    public bool ResetHP;
    public FloatReference StartingHP;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;

    private void Start()
    {
        if (ResetHP)
            HP.SetValue(StartingHP);
    }

    private void DealDamage(FloatReference damage)
    {
        HP.ApplyChange(-damage);
        if (HP.Value <= 0.0f)
        {
            DeathEvent.Invoke();
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();
    //    if (damage != null)
    //    {
    //        HP.ApplyChange(-damage.DamageAmount);
    //        DamageEvent.Invoke();
    //    }

    //    if (HP.Value <= 0.0f)
    //    {
    //        DeathEvent.Invoke();
    //    }
    //}
}
