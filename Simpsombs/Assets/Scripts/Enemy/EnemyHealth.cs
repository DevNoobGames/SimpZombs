using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int Health;
    public GameObject InstaKill;
    public GameObject Airstrike;
    public GameObject FullHeatlh;
    public GameObject MaxAmmo;
    public GameObject MoneyChest;
    public GameObject FreezeZombies;
    public bool Dying;

    public GameObject spawnManager;
    public GameObject Player;

    public Animator Anim;
    public Collider BodyCollider1;
    public Collider BodyCollider2;
    public Collider HeadCollider1;

    //public GameObject TheZombie;

    private void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager");
        Player = GameObject.Find("FirstPersonCharacter");
        InstaKill = GameObject.FindGameObjectWithTag("InstaKill");
        Airstrike = GameObject.FindGameObjectWithTag("Airstrike");
        FullHeatlh = GameObject.FindGameObjectWithTag("FullHealth");
        MaxAmmo = GameObject.FindGameObjectWithTag("MaxAmmo");
        MoneyChest = GameObject.FindGameObjectWithTag("MoneyChest");
        FreezeZombies = GameObject.FindGameObjectWithTag("FreezeZombies");
        Health = 100 * spawnManager.GetComponent<SpawnManager>().Level;
    }

    void DeductPoints(int DamageAmount)
    {
        Health -= DamageAmount;
    }

    void Update()
    {
        if (Health <= 0 && Dying == false)
        {
            Dying = true;
            Anim.SetBool("Dying", true);
            spawnManager.GetComponent<SpawnManager>().SpawnedZombies -= 1;
            Player.GetComponent<PlayerStatistics>().EnemiesKilled += 1;
            Player.GetComponent<PlayerStatistics>().Money += 30;
            Player.GetComponent<PlayerStatistics>().UpdateMoneyText();

            GetComponent<AIMovement>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
            BodyCollider1.enabled = false;
            BodyCollider2.enabled = false;
            HeadCollider1.enabled = false;

            float randValue = Random.value;
            if (randValue < 0.02) //2 Percent of time
            {
                GameObject InstaKillA = Instantiate(InstaKill, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                InstaKillA.GetComponent<TimerToRemove>().Timer = 5;
                InstaKillA.GetComponent<TimerToRemove>().active = true;
            }
            else if (randValue < 0.04)
            {
                GameObject airstrikeA = Instantiate(Airstrike, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                airstrikeA.GetComponent<TimerToRemove>().Timer = 5;
                airstrikeA.GetComponent<TimerToRemove>().active = true;
            }
            else if (randValue < 0.06)
            {
                GameObject FullHeatlhA = Instantiate(FullHeatlh, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                FullHeatlhA.GetComponent<TimerToRemove>().Timer = 5;
                FullHeatlhA.GetComponent<TimerToRemove>().active = true;
            }
            else if (randValue < 0.08)
            {
                GameObject MaxAmmoA = Instantiate(MaxAmmo, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                MaxAmmoA.GetComponent<TimerToRemove>().Timer = 5;
                MaxAmmoA.GetComponent<TimerToRemove>().active = true;
            }
            else if (randValue < 0.10)
            {
                GameObject MoneyChestA = Instantiate(MoneyChest, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                MoneyChestA.GetComponent<TimerToRemove>().Timer = 5;
                MoneyChestA.GetComponent<TimerToRemove>().active = true;
            }
            else if (randValue < 0.12)
            {
                GameObject FreezeZombiesA = Instantiate(FreezeZombies, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
                FreezeZombiesA.GetComponent<TimerToRemove>().Timer = 5;
                FreezeZombiesA.GetComponent<TimerToRemove>().active = true;
            }
            else
            {
                //Do nothing
            }


            if (spawnManager.GetComponent<SpawnManager>().SpawnedZombies <= 0)
            {
                GameObject.FindGameObjectWithTag("LevelWinSound").GetComponent<AudioSource>().Play();
                Player.GetComponent<PlayerStatistics>().HealthRunner();
                spawnManager.GetComponent<SpawnManager>().Level += 1;
                spawnManager.GetComponent<SpawnManager>().CouroStarter();
            }
            //Destroy(gameObject);
            StartCoroutine(EndZombie());


            //this.GetComponent<ZombieFollow>().enabled = false;
            //TheZombie.GetComponent<Animation>().Play("Dying");
            //StartCoroutine(EndZombie());
        }
    }

    IEnumerator EndZombie()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
