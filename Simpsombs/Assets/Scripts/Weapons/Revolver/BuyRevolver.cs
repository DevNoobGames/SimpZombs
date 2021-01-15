using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRevolver : MonoBehaviour
{
    public GameObject WeaponCosts;
    public GameObject player;
    public GameObject Weapon;
    public GameObject Mechanics;
    public GameObject TextDisplay;
    public Text AmmoText;
    public Text MoneyText;
    public float TheDistance;
    public AudioSource BuySound;

    [Header ("Script Variables")]
    public int ListNumber = 1; //Change which number in list it is
    public string WeaponName = "Revolver";
    public float WeaponCost;
    public float AmmoCost;
    public int AmmoAmount;

    private void Start()
    {
        //SCRIPT VARIABLES
        WeaponCost = WeaponCosts.GetComponent<WeaponCosts>().Revolver;
        AmmoCost = WeaponCosts.GetComponent<WeaponCosts>().RevolverAmmo20;
        //FINALLY TO DO:
        //--Change FireRevolver script to approapiate script

        //OTHER
        TheDistance = 99999;
    }

    private void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        //========= IF NOT HAVE THIS WEAPON YET========\\
        if (player.GetComponent<ActiveWeapon>().Weapon1 != ListNumber && player.GetComponent<ActiveWeapon>().Weapon2 != ListNumber)
        {
            if (TheDistance <= 4)
            {
                TextDisplay.GetComponent<Text>().text = "Buy " + WeaponName + "\n" + WeaponCost + "$";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= WeaponCost)
                {
                    BuySound.Play();
                    player.GetComponent<PlayerStatistics>().Money -= WeaponCost;
                    MoneyText.text = player.GetComponent<PlayerStatistics>().Money + "$";
                    if (player.GetComponent<ActiveWeapon>().Weapon1 == 99)
                    {
                        //Buy catapult in slot 1
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon1 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(true);
                        player.GetComponent<ActiveWeapon>().ActiveWeaponSlot = 1;


                        AmmoText.text = Mechanics.GetComponent<FireRevolver>().CurrentAmmo + "/" + Mechanics.GetComponent<FireRevolver>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().Weapon2 == 99)
                    {
                        //buy catapult for slot 2
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon2 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(true);
                        player.GetComponent<ActiveWeapon>().ActiveWeaponSlot = 2;


                        AmmoText.text = Mechanics.GetComponent<FireRevolver>().CurrentAmmo + "/" + Mechanics.GetComponent<FireRevolver>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 1)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon1 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(true);
                        AmmoText.text = Mechanics.GetComponent<FireRevolver>().CurrentAmmo + "/" + Mechanics.GetComponent<FireRevolver>().ReserveAmmo;
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 2)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon2 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(true);
                        AmmoText.text = Mechanics.GetComponent<FireRevolver>().CurrentAmmo + "/" + Mechanics.GetComponent<FireRevolver>().ReserveAmmo;
                    }
                }
            }
        }

        //========= IF ALREADY HAVE THIS WEAPON, BUY BULLETS========\\
        else if (player.GetComponent<ActiveWeapon>().Weapon1 == ListNumber || player.GetComponent<ActiveWeapon>().Weapon2 == ListNumber)
        {
            if (TheDistance <= 4)
            {
                if (Mechanics.GetComponent<FireRevolver>().ReserveAmmo < Mechanics.GetComponent<FireRevolver>().MaxAmmo)
                {
                    TextDisplay.GetComponent<Text>().text = "20 AMMO" + "\n" + AmmoCost + "$";
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
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= AmmoCost && Mechanics.GetComponent<FireRevolver>().ReserveAmmo < Mechanics.GetComponent<FireRevolver>().MaxAmmo)
                {
                    BuySound.Play();
                    Mechanics.GetComponent<FireRevolver>().ReserveAmmo += AmmoAmount;
                    if (Mechanics.GetComponent<FireRevolver>().ReserveAmmo > Mechanics.GetComponent<FireRevolver>().MaxAmmo)
                    {
                        Mechanics.GetComponent<FireRevolver>().ReserveAmmo = Mechanics.GetComponent<FireRevolver>().MaxAmmo;
                    }

                    //Only show text if weapon is active
                    if (Weapon.activeInHierarchy)
                    {
                        AmmoText.text = Mechanics.GetComponent<FireRevolver>().CurrentAmmo + "/" + Mechanics.GetComponent<FireRevolver>().ReserveAmmo;
                    }

                    player.GetComponent<PlayerStatistics>().Money -= AmmoCost;
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
