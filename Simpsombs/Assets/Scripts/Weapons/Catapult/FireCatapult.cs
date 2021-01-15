using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCatapult : MonoBehaviour
{
    public GameObject Player;
    public PerkManager PerkManager;
    public int CashRewardOnShot;
    public int CashRewardOnHeadshot;

    public int StandardDamageAmount;
    public int SpecialDamageAmount;
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
    public Animation ShotAnimation;
    public Animation RockAnimation;
    public Animation CatapultAnims;
    public AudioSource reloadSound;
    public AudioSource shotSound;
    

    private void Start()
    {
        reloading = false;
        SpecialDamageAmount = StandardDamageAmount;
        CurrentAmmo = 10;
        //Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;
    }

    private void OnEnable()
    {
        canShoot = false;
        reloading = false;
        StartCoroutine(GrabbingGunAnim());
        Ammotext.text = CurrentAmmo + "/" + ReserveAmmo;

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (CurrentAmmo >= 1 && !reloading && canShoot)
            {
                ShotAnimation.Play("CatapultShot");
                RockAnimation.Play("CatapultRock");
                shotSound.Play();

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

                            if (PerkManager.InstaKill == true)
                            {
                                SpecialDamageAmount = 9999999;
                            }
                            //Debug.Log("Zombie Hit!!");
                            //Instantiate(BloodAnimation, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                        if (hit.collider.tag == "ZombieHead")
                        {
                            //Debug.Log("Zombie Head!");
                            SpecialDamageAmount = 150;
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
                            //Debug.Log("HIT");
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
    }

    IEnumerator Reload()
    {
        reloading = true;
        canShoot = false;
        CatapultAnims.Play("CatapultReload");
        reloadSound.Play();
        yield return new WaitForSeconds(0.6f);

        if (CurrentAmmo == 0)
        {
            if (ReserveAmmo >= ClipSize)
            {
                CurrentAmmo = ClipSize;
                ReserveAmmo -= ClipSize;
            }
            else if (ReserveAmmo < ClipSize)
            {
                CurrentAmmo = ReserveAmmo;
                ReserveAmmo = 0;
            }
        }
        else if (CurrentAmmo > 0)
        {
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
    }

    IEnumerator GrabbingGunAnim()
    {
        CatapultAnims.Play("CatapultGrab");
        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }
}