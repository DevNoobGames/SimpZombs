﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRevolver : MonoBehaviour
{
    public GameObject Player;
    public PerkManager PerkManager;

    public int CashRewardOnShot;
    public int CashRewardOnHeadshot;

    public int StandardDamageAmount; //Standard amount of damage
    public int SpecialDamageAmount; //Headshots f.i.
    public int HeadShotDamage;
    public RaycastHit hit;
    public GameObject BulletHole;
    public GameObject BloodAnimation;
    public int CurrentAmmo; //Current ammo
    public int ClipSize; //The maximum active ammo you can hold
    public int ReserveAmmo; //Ammo to be used
    public int MaxAmmo; //Max amount of ammo that you can have
    public Text Ammotext;
    public bool reloading;
    public bool canShoot;
    public bool ZoomedIn;
    public Animation RevolverAnims;
    public AudioSource reloadSound;
    public AudioSource shotSound;

    public GameObject CrossHairs;
    public GameObject Flash;

    private void Start()
    {
        reloading = false;
        SpecialDamageAmount = StandardDamageAmount;
        CurrentAmmo = 20;
        Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;
    }

    private void OnEnable()
    {
        canShoot = false;
        reloading = false;
        StartCoroutine(GrabbingGunAnim());
        Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;
        CrossHairs.SetActive(true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CurrentAmmo >= 1 && !reloading && canShoot)
            {
                if (!ZoomedIn)
                {
                    RevolverAnims.Play("RevolverShot");
                }
                if (ZoomedIn)
                {
                    RevolverAnims.Play("RevolverZoomedShot");
                }

                shotSound.Play();
                Flash.SetActive(true);
                StartCoroutine(TurnOffFlash());

                RaycastHit Shot;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
                    {
                        //Debug.Log(Vector3.Distance(hit.transform.position, transform.position));
                        if (hit.transform.tag == "BasicZombie")
                        {
                            Player.GetComponent<PlayerStatistics>().Money += CashRewardOnShot;
                            Player.GetComponent<PlayerStatistics>().UpdateMoneyText();
                            //Debug.Log("Zombie Hit!!");
                            if (PerkManager.InstaKill == true)
                            {
                                SpecialDamageAmount = 9999999;
                            }
                            //Instantiate(BloodAnimation, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                        if (hit.collider.tag == "ZombieHead")
                        {
                            SpecialDamageAmount = HeadShotDamage;
                            if (PerkManager.InstaKill == true)
                            {
                                SpecialDamageAmount = 9999999;
                            }
                            Player.GetComponent<PlayerStatistics>().Money += CashRewardOnHeadshot;
                            Player.GetComponent<PlayerStatistics>().UpdateMoneyText();
                            //Instantiate(BloodAnimation, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                        if (hit.transform.tag == "Spider")
                        {
                            //Instantiate(BloodAnimation, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                        if (hit.transform.tag == "Untagged")
                        {
                            Debug.Log("HIT");
                            //Instantiate(BulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                    }
                    Shot.transform.SendMessage("DeductPoints", SpecialDamageAmount, SendMessageOptions.DontRequireReceiver);
                    SpecialDamageAmount = StandardDamageAmount;

                    CurrentAmmo -= 1;
                    Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;
                }
            }
            if (CurrentAmmo == 0 && ReserveAmmo > 0 && !reloading && canShoot)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (ReserveAmmo > 0 && CurrentAmmo < ClipSize && !reloading && canShoot)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //play anim
            ZoomedIn = true;
            RevolverAnims.Play("RevolverZoom");
            Player.GetComponent<Camera>().fieldOfView = 40;
            CrossHairs.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            ZoomedIn = false;
            RevolverAnims.Rewind("RevolverZoom");
            RevolverAnims.Play("RevolverZoom");
            RevolverAnims.Sample();
            RevolverAnims.Stop("RevolverZoom");
            Player.GetComponent<Camera>().fieldOfView = 60;
            CrossHairs.SetActive(true);
        }
    }

    IEnumerator Reload()
    {
        //ZOOM OUT
        ZoomedIn = false;
        RevolverAnims.Rewind("RevolverZoom");
        RevolverAnims.Play("RevolverZoom");
        RevolverAnims.Sample();
        RevolverAnims.Stop("RevolverZoom");
        Player.GetComponent<Camera>().fieldOfView = 60;
        CrossHairs.SetActive(false);

        reloading = true;
        canShoot = false;
        RevolverAnims.Play("RevolverReload");
        reloadSound.Play();
        yield return new WaitForSeconds(0.6f);

        if (CurrentAmmo == 0)
        {
            Debug.Log("Current ammo is 0");
            if (ReserveAmmo >= ClipSize)
            {
                Debug.Log("First option");
                CurrentAmmo = ClipSize;
                ReserveAmmo -= ClipSize;
            }
            else if (ReserveAmmo < ClipSize)
            {
                Debug.Log("Second option");
                CurrentAmmo = ReserveAmmo;
                ReserveAmmo = 0;
            }
        }
        else if (CurrentAmmo > 0)
        {
            Debug.Log("Current ammo is more than 0");
            int ReloadAmount = ClipSize - CurrentAmmo; //10 - 6, needs to reload 4
            if (ReserveAmmo >= ReloadAmount)
            {
                CurrentAmmo = ClipSize;
                ReserveAmmo -= ReloadAmount;
            }
            else if (ReserveAmmo < ReloadAmount)
            {
                CurrentAmmo += ReserveAmmo;
                ReserveAmmo = 0;
            }

        }
        reloading = false;
        canShoot = true;
        Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;

        CrossHairs.SetActive(true);

        if (Input.GetMouseButton(1))
        {
            ZoomedIn = true;
            RevolverAnims.Play("RevolverZoom");
            Player.GetComponent<Camera>().fieldOfView = 40;
            CrossHairs.SetActive(false);
        }
    }

    IEnumerator GrabbingGunAnim()
    {
        RevolverAnims.Play("RevolverGrab");
        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }

    IEnumerator TurnOffFlash()
    {
        yield return new WaitForSeconds(0.15f);
        Flash.SetActive(false);
    }
}
