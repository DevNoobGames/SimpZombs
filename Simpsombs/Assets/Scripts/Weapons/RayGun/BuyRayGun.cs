using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyRayGun : MonoBehaviour
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

    [Header("Script Variables")]
    public int ListNumber = 4; //Change which number in list it is
    public string WeaponName = "Raygun";
    public float WeaponCost;
    //public float AmmoCost;
    //public int AmmoAmount;

    void Start()
    {
        //SCRIPT VARIABLES
        WeaponCost = WeaponCosts.GetComponent<WeaponCosts>().RayGunCost;
        //AmmoCost = WeaponCosts.GetComponent<WeaponCosts>().AutoRiffeAmmo40;
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

                        AmmoText.text = Mechanics.GetComponent<FireRayGun>().CurrentAmmo + "/ ∞";
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

                        AmmoText.text = Mechanics.GetComponent<FireRayGun>().CurrentAmmo + "/ ∞";
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 1)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon1 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon1].Mechanics.SetActive(true);
                        AmmoText.text = Mechanics.GetComponent<FireRayGun>().CurrentAmmo + "/ ∞";
                    }
                    else if (player.GetComponent<ActiveWeapon>().ActiveWeaponSlot == 2)
                    {
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(false);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(false);
                        player.GetComponent<ActiveWeapon>().Weapon2 = ListNumber;
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Weapon.SetActive(true);
                        player.GetComponent<ActiveWeapon>().weaponlist[player.GetComponent<ActiveWeapon>().Weapon2].Mechanics.SetActive(true);
                        AmmoText.text = Mechanics.GetComponent<FireRayGun>().CurrentAmmo + "/ ∞";
                    }
                }
            }
        }

        //========= IF ALREADY HAVE THIS WEAPON, BUY BULLETS========\\
        if (player.GetComponent<ActiveWeapon>().Weapon1 == ListNumber || player.GetComponent<ActiveWeapon>().Weapon2 == ListNumber)
        {
            if (TheDistance <= 4)
            {
                    TextDisplay.GetComponent<Text>().text = "Already owned";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }
        }
    }

    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }
}
