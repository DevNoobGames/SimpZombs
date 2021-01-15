using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireRayGun : MonoBehaviour
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
    //public int ReserveAmmo; //Ammo to be used
    //public int MaxAmmo; //Max amount of ammo that you can have
    public Text Ammotext;
    public bool reloading;
    public bool canShoot;
    public bool ZoomedIn;
    public Animation RayGunAnims;
    public AudioSource reloadSound;
    public AudioSource shotSound;
    public GameObject ReloadParticles;

    public GameObject CrossHairs;
    public GameObject Flash;
    public GameObject ParticleSystemShot;

    private void Start()
    {
        reloading = false;
        SpecialDamageAmount = StandardDamageAmount;
        CurrentAmmo = 20;
        Ammotext.text = CurrentAmmo + "/ MAX";
    }

    private void OnEnable()
    {
        canShoot = false;
        StopAllCoroutines();
        StartCoroutine(GrabbingGunAnim());
        Ammotext.text = CurrentAmmo + "/ MAX";
        ReloadParticles.SetActive(false);
        if (reloading)
        {
            reloading = false;
            RayGunAnims.Rewind("RayGunReload");
            RayGunAnims.Play("RayGunReload");
            RayGunAnims.Sample();
            RayGunAnims.Stop("RayGunReload");
        }
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
                    RayGunAnims.Play("RayGunShot");
                }
                if (ZoomedIn)
                {
                    RayGunAnims.Play("RayGunZoomedShot");
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
                            GameObject ParticleShot = Instantiate(ParticleSystemShot, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            ParticleShot.GetComponent<ParticleSystemDestroyer>().active = true;
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
                            GameObject ParticleShot = Instantiate(ParticleSystemShot, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            ParticleShot.GetComponent<ParticleSystemDestroyer>().active = true;
                        }
                        if (hit.transform.tag == "Spider")
                        {
                            GameObject ParticleShot = Instantiate(ParticleSystemShot, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            ParticleShot.GetComponent<ParticleSystemDestroyer>().active = true;
                        }
                        if (hit.transform.tag == "Untagged")
                        {
                            Debug.Log("HIT");
                            GameObject ParticleShot = Instantiate(ParticleSystemShot, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                            ParticleShot.GetComponent<ParticleSystemDestroyer>().active = true;
                        }
                    }
                    Shot.transform.SendMessage("DeductPoints", SpecialDamageAmount, SendMessageOptions.DontRequireReceiver);
                    SpecialDamageAmount = StandardDamageAmount;

                    CurrentAmmo -= 1;
                    Ammotext.text = CurrentAmmo + "/ MAX";
                }
            }
            if (CurrentAmmo == 0 && !reloading && canShoot)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetButtonDown("Reload"))
        {
            if (CurrentAmmo < ClipSize && !reloading && canShoot)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //play anim
            if (!reloading)
            {
                ZoomedIn = true;
                RayGunAnims.Play("RayGunZoom");
                Player.GetComponent<Camera>().fieldOfView = 40;
                CrossHairs.SetActive(false);
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (!reloading)
            {
                ZoomedIn = false;
                RayGunAnims.Rewind("RayGunZoom");
                RayGunAnims.Play("RayGunZoom");
                RayGunAnims.Sample();
                RayGunAnims.Stop("RayGunZoom");
                Player.GetComponent<Camera>().fieldOfView = 60;
                CrossHairs.SetActive(true);
            }
        }
    }

    IEnumerator Reload()
    {
        //ZOOM OUT
        ZoomedIn = false;
        RayGunAnims.Rewind("RayGunZoom");
        RayGunAnims.Play("RayGunZoom");
        RayGunAnims.Sample();
        RayGunAnims.Stop("RayGunZoom");
        Player.GetComponent<Camera>().fieldOfView = 60;
        CrossHairs.SetActive(false);
        ReloadParticles.SetActive(true);
        reloading = true;
        canShoot = false;
        RayGunAnims.Play("RayGunReload");
        reloadSound.Play();
        yield return new WaitForSeconds(6);

        CurrentAmmo = ClipSize;

        reloading = false;
        canShoot = true;
        Ammotext.text = CurrentAmmo + "/ MAX";

        CrossHairs.SetActive(true);
        ReloadParticles.SetActive(false);
        RayGunAnims.Rewind("RayGunReload");
        RayGunAnims.Play("RayGunReload");
        RayGunAnims.Sample();
        RayGunAnims.Stop("RayGunReload");
        if (Input.GetMouseButton(1))
        {
            ZoomedIn = true;
            RayGunAnims.Play("RayGunZoom");
            Player.GetComponent<Camera>().fieldOfView = 40;
            CrossHairs.SetActive(false);
        }
    }

    IEnumerator GrabbingGunAnim()
    {
        RayGunAnims.Play("RayGunGrab");
        yield return new WaitForSeconds(0.7f);
        canShoot = true;
    }

    IEnumerator TurnOffFlash()
    {
        yield return new WaitForSeconds(0.15f);
        Flash.SetActive(false);
    }
}
