using UnityEngine;

[CreateAssetMenu(menuName = "Weapon Types/Raycast Weapon")]
public class RayCastWeapon : WeaponType {
    public int weaponDamage = 1;
    public float weaponRange = 50f;
    private RaycastWeaponTrigger rcShoot;

    public override void Initialize(GameObject obj)
    {
        rcShoot = obj.GetComponent<RaycastWeaponTrigger>();
        rcShoot.Initialize();

        rcShoot.weaponDamage = weaponDamage;
        rcShoot.weaponRange = weaponRange;
    }

    public override void TriggerAbility()
    {
        rcShoot.Fire();
    }
}
