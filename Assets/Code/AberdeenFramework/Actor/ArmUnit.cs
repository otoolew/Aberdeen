using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmUnit : MonoBehaviour {
    public GameObject WeaponPrefab;
    public GameObject EquipedWeapon;
    // Use this for initialization
    void Start () {
        EquipedWeapon = Instantiate(WeaponPrefab, transform.position, transform.rotation);
        EquipedWeapon.transform.SetParent(transform);
        Debug.Log("Armed Unit");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
