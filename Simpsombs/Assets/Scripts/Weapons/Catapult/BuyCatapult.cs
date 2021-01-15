using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyCatapult : MonoBehaviour
{
    public GameObject WeaponCosts;
    public GameObject player;
    public GameObject Catapult;
    public GameObject CatapultMechanics;
    public GameObject TextDisplay;
    public Text AmmoText;
    public Text MoneyText;
    public float TheDistance;
    public AudioSource BuySound;

    private void Start()
    {
        TheDistance = 99999;
    }

    private void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        //========= IF NOT HAVE A CATAPULT YET========\\
        if (player.GetComponent<ActiveWeapon>().Weapon1 != 0 && player.GetComponent<ActiveWeapon>().Weapon2 != 0)
        {
            if (TheDistance <= 4)
            {
                TextDisplay.GetComponent<Text>().text = "Buy Catapult" + "\n" + WeaponCosts.GetComponent<WeaponCosts>().Catapult + "$";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= WeaponCosts.GetComponent<WeaponCosts>().Catapult)
                {
                    BuySound.Play();
                    player.GetComponent<PlayerStatistics>().Money -= WeaponCosts.GetComponent<WeaponCosts>().Catapult;
                    MoneyText.text = player.GetComponent<PlayerStatistics>().Money + "$";
                    if (player.GetComponent<ActiveWeapon>().Weapon1 == 99)
                    {
                        //Buy catapult in slot 1
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon1 = 0;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(true);
                        player.GetComponent<ActiveWeapon>().ActiveWeaponSlot = 1;

                        AmmoText.text = CatapultMechanics.GetComponent<FireCatapult>().CurrentAmmo + "/" + CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().Weapon2 == 99)
                    {
                        //buy catapult for slot 2
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon2 = 0;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(true);
                        player.GetComponent<ActiveWeapon>().ActiveWeaponSlot = 2;

                        AmmoText.text = CatapultMechanics.GetComponent<FireCatapult>().CurrentAmmo + "/" + CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 1)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon1 = 0;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(true);
                        AmmoText.text = CatapultMechanics.GetComponent<FireCatapult>().CurrentAmmo + "/" + CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 2)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon2 = 0;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(true); AmmoText.text = CatapultMechanics.GetComponent<FireCatapult>().CurrentAmmo + "/" + CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo;
                    }
                }
            }
        }

        //========= IF ALREADY HAVE A CATAPULT, BUY BULLETS========\\
        else if (player.GetComponent<ActiveWeapon>().Weapon1 == 0 || player.GetComponent<ActiveWeapon>().Weapon2 == 0)
        {
            if (TheDistance <= 4)
            {
                if (CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo < CatapultMechanics.GetComponent<FireCatapult>().MaxAmmo)
                {
                    TextDisplay.GetComponent<Text>().text = "10 AMMO" + "\n" + WeaponCosts.GetComponent<WeaponCosts>().CatapultAmmo10 + "$";
                }
                else
                {
                    TextDisplay.GetComponent<Text>().text = "MAX";
                }
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= WeaponCosts.GetComponent<WeaponCosts>().CatapultAmmo10 && CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo < CatapultMechanics.GetComponent<FireCatapult>().MaxAmmo)
                {
                    BuySound.Play();
                    CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo += 10;
                    if (CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo > CatapultMechanics.GetComponent<FireCatapult>().MaxAmmo)
                    {
                        CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo = CatapultMechanics.GetComponent<FireCatapult>().MaxAmmo;
                    }

                    if (Catapult.activeInHierarchy == true)
                    {
                        AmmoText.text = CatapultMechanics.GetComponent<FireCatapult>().CurrentAmmo + "/" + CatapultMechanics.GetComponent<FireCatapult>().ReserveAmmo;
                    }

                    player.GetComponent<PlayerStatistics>().Money -= WeaponCosts.GetComponent<WeaponCosts>().CatapultAmmo10;
                    MoneyText.text = player.GetComponent<PlayerStatistics>().Money + "$";
                }
            }
        }
    }

    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }
}
