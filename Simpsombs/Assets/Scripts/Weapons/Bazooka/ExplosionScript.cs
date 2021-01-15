using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public Collider SphereCollider;
    public GameObject Player;

    private void Start()
    {
        Player = GameObject.Find("FirstPersonCharacter");
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BasicZombie")
        {
            Debug.Log("Zombay");
            collision.transform.SendMessage("DeductPoints", 90000, SendMessageOptions.DontRequireReceiver);
            
        }
        if (collision.gameObject.tag == "FPSController")
        {
            Debug.Log("Hit player");
            Player.GetComponent<PlayerStatistics>().health -= 1;
        }

        SphereCollider.enabled = false;
    }


}
