using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponType : ScriptableObject
{
    public string WeaponName = "New Ability";
    //public AudioClip WeaponSound;
    public float WeaponCoolDown = 1f;
    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility();
}
