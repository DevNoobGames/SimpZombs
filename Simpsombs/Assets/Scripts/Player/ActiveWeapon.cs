using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public int Weapon1;
    public int Weapon2;
    public int ActiveWeaponSlot;

    public Weapons[] weaponlist;

    [System.Serializable]
    public class Weapons 
    {
        public GameObject Weapon;
        public GameObject Mechanics;
        //public bool Active1;
        //public bool Active2;
    }

    void Start()
    {
        ActiveWeaponSlot = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if scroll, go to weapon 2
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (ActiveWeaponSlot == 1)
            {
                if (Weapon2 != 99)
                {
                    ActiveWeaponSlot = 2;
                    weaponlist[Weapon1].Weapon.SetActive(false);
                    weaponlist[Weapon1].Mechanics.SetActive(false);
                    weaponlist[Weapon2].Weapon.SetActive(true);
                    weaponlist[Weapon2].Mechanics.SetActive(true);
                }
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (ActiveWeaponSlot == 2)
            {
                if (Weapon1 != 99)
                {
                    ActiveWeaponSlot = 1;
                    weaponlist[Weapon1].Weapon.SetActive(true);
                    weaponlist[Weapon1].Mechanics.SetActive(true);
                    weaponlist[Weapon2].Weapon.SetActive(false);
                    weaponlist[Weapon2].Mechanics.SetActive(false);
                }
            }
        }
    }
}
