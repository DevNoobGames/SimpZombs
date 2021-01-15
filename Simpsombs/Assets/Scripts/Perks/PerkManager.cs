using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PerkManager : MonoBehaviour
{
    public PlayerStatistics Player;

    public GameObject playerMoney;
    public FireBazooka FireBazookaS;
    public FireAutoRiffle FireAutoRiffleS;
    public FireCatapult FireCatapultS;
    public FireRevolver FireRevolverS;
    public Text AmmoText;

    public bool InstaKill;
    public GameObject InstaKillIcon;
    public bool Frozen;
    public GameObject FrozenIcon;

    private void Start()
    {
        InstaKillIcon.SetActive(false);
        FrozenIcon.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InstaKill")
        {
            Debug.Log("INSTAKILL");
            //InstaKill = false;
            //StopAllCoroutines();
            StopCoroutine(InstaKillRunner());
            InstaKillIcon.SetActive(true);
            Destroy(other.gameObject);
            InstaKill = true;
            StartCoroutine(InstaKillRunner());
        }

        if (other.gameObject.tag == "Airstrike")
        {
            Debug.Log("Airstrike");
            GameObject[] Zombies;
            Zombies = GameObject.FindGameObjectsWithTag("BasicZombie");
            foreach (GameObject zombie in Zombies)
            {
                zombie.GetComponent<EnemyHealth>().Health = 0;
            }
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FullHealth")
        {
            Debug.Log("Full health");
            Player.HealthRunner();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "MaxAmmo")
        {
            Debug.Log("Max Ammo");
            FireBazookaS.ReserveAmmo = FireBazookaS.MaxAmmo;
            FireAutoRiffleS.ReserveAmmo = FireAutoRiffleS.MaxAmmo;
            FireCatapultS.ReserveAmmo = FireCatapultS.MaxAmmo;
            FireRevolverS.ReserveAmmo = FireRevolverS.MaxAmmo;
            AmmoText.text = "MAX AMMO";
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "MoneyChest")
        {
            Debug.Log("Money!");
            if (Player.Money <= 10000)
            {
                Player.Money += 1000;
            }
            else
            {
                Player.Money += Mathf.Round(Player.Money / 10);
            }
            playerMoney.GetComponent<PlayerStatistics>().MoneyText.text = playerMoney.GetComponent<PlayerStatistics>().Money.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "FreezeZombies")
        {
            Debug.Log("Zombies freeze");
            StopCoroutine(ZombieFreezer());
            FrozenIcon.SetActive(true);
            Frozen = true;
            GameObject[] Zombies;
            Zombies = GameObject.FindGameObjectsWithTag("BasicZombie");
            foreach (GameObject zombie in Zombies)
            {
                zombie.GetComponent<AIMovement>().enabled = false;
                zombie.GetComponent<NavMeshAgent>().speed = 0;
            }
            StartCoroutine(ZombieFreezer());
            Destroy(other.gameObject);
        }

    }

    IEnumerator ZombieFreezer()
    {
        yield return new WaitForSeconds(10);
        GameObject[] Zombies;
        Zombies = GameObject.FindGameObjectsWithTag("BasicZombie");
        foreach (GameObject zombie in Zombies)
        {
            zombie.GetComponent<AIMovement>().enabled = true;
        }
        Frozen = false;
        FrozenIcon.SetActive(false);
    }

    IEnumerator InstaKillRunner()
    {
        yield return new WaitForSeconds(30);
        InstaKill = false;
        InstaKillIcon.SetActive(false);
    }

}
