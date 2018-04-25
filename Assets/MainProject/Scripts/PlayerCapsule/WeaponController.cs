using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    Gun equippedGun;
    public Transform weaponHold;
    public Gun startingGun;
    MyCapsuleController myCapContr;

    void Start()
    {
        myCapContr = GetComponent<MyCapsuleController>();

        if(startingGun != null)
        {
            EquipGun(startingGun);
        }
    }

    void FixedUpdate()
    {
        weaponHold.LookAt(myCapContr.lookPos);
    }

    public void EquipGun(Gun gunToEquip)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun = Instantiate(gunToEquip , weaponHold.position, weaponHold.rotation);
        equippedGun.transform.parent = weaponHold;
    }

    public void Shoot()
    {
        if(equippedGun != null)
        {
            equippedGun.Shoot();
        }
    }
}
