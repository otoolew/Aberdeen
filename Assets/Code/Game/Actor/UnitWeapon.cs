using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitWeapon : MonoBehaviour {
    
    [SerializeField] private WeaponType weapon;
    [SerializeField] private GameObject weaponHolder;
    private float coolDownDuration;
    private float nextReadyTime;
    private float coolDownTimeLeft;


    void Start()
    {
        Initialize(weapon, weaponHolder);
    }

    public void Initialize(WeaponType equipedWeapon, GameObject weaponHolder)
    {
        weapon = equipedWeapon;
    
        coolDownDuration = weapon.WeaponCoolDown;
        weapon.Initialize(weaponHolder);
    }
    // Update is called once per frame
    void Update()
    {
        bool coolDownComplete = (Time.time > nextReadyTime);
        if (coolDownComplete)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FireWeapon();
            }
        }
        else
        {
            CoolDown();
        }
    }
    private void CoolDown()
    {
        coolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(coolDownTimeLeft);
    }

    public void FireWeapon()
    {
        nextReadyTime = coolDownDuration + Time.time;
        coolDownTimeLeft = coolDownDuration;

        weapon.TriggerAbility();
    }
}
