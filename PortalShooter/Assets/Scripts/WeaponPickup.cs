using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public int id;
    public bool addWeapon;
    public int ammo;

    void OnTriggerEnter(Collider other)
    {
         var ws = other.gameObject.GetComponent<WeaponSystem>();
        if (ws){
            if (addWeapon){
                ws.addWeapon(id);
            }
            ws.addAmmo(id, ammo);
            ws.setWeapon(id);
            Destroy(gameObject);
        }
    }
}
