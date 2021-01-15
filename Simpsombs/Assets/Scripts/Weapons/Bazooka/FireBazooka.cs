using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBazooka : MonoBehaviour
{
    public GameObject Player;
    public GameObject projectile;
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
    public Animation BazookaAnims;
    public AudioSource reloadSound;
    public AudioSource shotSound;

    public GameObject CrossHairs;
    public GameObject Flash;

    private void Start()
    {
        reloading = false;
        SpecialDamageAmount = StandardDamageAmount;
        CurrentAmmo = 2;
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
                //CREATE BAZOOKA
                GameObject DonutShot = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
                DonutShot.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);

                canShoot = false;
                if (!ZoomedIn)
                {
                    BazookaAnims.Play("BazookaShot");
                }
                if (ZoomedIn)
                {
                    BazookaAnims.Play("BazookaZoomShot");
                }

                shotSound.Play();
                Flash.SetActive(true);
                StartCoroutine(TurnOffFlash());
                StartCoroutine(WaitForNewShot());

                /*RaycastHit Shot;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot))
                {
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
                    {
                        Debug.Log(Vector3.Distance(hit.transform.position, transform.position));
                        if (hit.transform.tag == "BasicZombie")
                        {
                            Player.GetComponent<PlayerStatistics>().Money += CashRewardOnShot;
                            Player.GetComponent<PlayerStatistics>().UpdateMoneyText();
                            Debug.Log("Zombie Hit!!");
                            //Instantiate(BloodAnimation, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                        }
                        if (hit.collider.tag == "ZombieHead")
                        {
                            SpecialDamageAmount = HeadShotDamage;
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
                    */
                { 
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
            BazookaAnims.Play("BazookaZoom");
            Player.GetComponent<Camera>().fieldOfView = 20;
            CrossHairs.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            ZoomedIn = false;
            BazookaAnims.Rewind("BazookaZoom");
            BazookaAnims.Play("BazookaZoom");
            BazookaAnims.Sample();
            BazookaAnims.Stop("BazookaZoom");
            Player.GetComponent<Camera>().fieldOfView = 60;
            CrossHairs.SetActive(true);
        }
    }

    IEnumerator Reload()
    {
        //ZOOM OUT
        ZoomedIn = false;
        BazookaAnims.Rewind("BazookaZoom");
        BazookaAnims.Play("BazookaZoom");
        BazookaAnims.Sample();
        BazookaAnims.Stop("BazookaZoom");
        Player.GetComponent<Camera>().fieldOfView = 60;
        CrossHairs.SetActive(false);

        reloading = true;
        canShoot = false;
        //yield return new WaitForSeconds(0.5f);
        BazookaAnims.Play("BazookaReload");
        reloadSound.Play();
        yield return new WaitForSeconds(2f);

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
            BazookaAnims.Play("BazookaZoom");
            Player.GetComponent<Camera>().fieldOfView = 40;
            CrossHairs.SetActive(false);
        }
    }

    IEnumerator GrabbingGunAnim()
    {
        BazookaAnims.Play("BazookaGrab");
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }

    IEnumerator TurnOffFlash()
    {
        yield return new WaitForSeconds(0.3f);
        Flash.SetActive(false);
    }

    IEnumerator WaitForNewShot()
    {
        yield return new WaitForSeconds(1);
        canShoot = true;
    }
}
