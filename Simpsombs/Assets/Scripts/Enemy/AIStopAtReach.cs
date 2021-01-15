using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStopAtReach : MonoBehaviour
{
    public GameObject Player;
    public NavMeshAgent Agent;
    public NavMeshObstacle Obstacle;
    public float distance;
    public 

    // Start is called before the first frame update
    void Start()
    {
        Agent.enabled = true;
        Obstacle.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Player.transform.position, gameObject.transform.position);
        if (distance <= 1.5f)
        {
            Obstacle.enabled = true;
            Agent.enabled = false;
        }
        if (distance > 1.5f)
        {
            Agent.enabled = true;
            Obstacle.enabled = false;
        }
    }
}
