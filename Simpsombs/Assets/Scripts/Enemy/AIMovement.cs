using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject Player;
    public float speed;//NORMALLY = 5!!
    public float StandardSpeed; 
    public NavMeshObstacle obstacle;
    public float stoppingDistance;
    public bool isAttacking;
    public Animation hitAnim;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("FirstPersonCharacter");
        hitAnim = GameObject.Find("HitAnimation").GetComponent<Animation>();

        stoppingDistance = Random.Range(2.2f, 2.5f);
        agent = GetComponent<NavMeshAgent>();
        agent.avoidancePriority = Random.Range(0, 100);
        float randValue = Random.value;
        if (randValue < 0.2)
        {
            speed = Random.Range(2, 3);
        }
        else if (randValue < 0.8)
        {
            speed = Random.Range(3, 7);
        }
        else
        {
            speed = Random.Range(7, 10);
        }
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //agent.destination = Player.transform.position;
        if (Vector3.Distance(transform.position, Player.transform.position) > stoppingDistance) //Stopping distance was 2 before
        {
            agent.speed = speed;
            obstacle.enabled = false;
            agent.enabled = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            if (isAttacking)
            {
                StopAllCoroutines();
                isAttacking = false;
            }
        }
        else
        {
            agent.speed = 0;
            obstacle.enabled = true;
            agent.enabled = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
        }
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(Player.transform.position);
        }
        Vector3 targetPostition = new Vector3(Player.transform.position.x,
                                this.transform.position.y,
                                Player.transform.position.z);
        this.transform.LookAt(targetPostition);
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(1);
        if (Vector3.Distance(transform.position, Player.transform.position) <= stoppingDistance)
        {
            if (Player.name == "FirstPersonCharacter")
            {
                if (Player.GetComponent<PlayerStatistics>().Attackable == true)
                {
                    Player.GetComponent<PlayerStatistics>().Attackable = false;
                    Player.GetComponent<PlayerStatistics>().health -= 1;
                    Debug.Log("attacked");
                    hitAnim.Play("HitAnim");
                    //StartCoroutine(Player.GetComponent<PlayerStatistics>().ResetAttack());
                }
            }
        }
        isAttacking = false;
    }

}
