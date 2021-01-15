using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutExplosion : MonoBehaviour
{
    public GameObject Explosion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "FPSController")
        {
            Instantiate(Resources.Load("Explosion"), transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
