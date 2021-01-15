using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerToRemove : MonoBehaviour
{
    public float Timer;
    public bool active;

    private void Update()
    {
        if (active)
        {
            Timer -= Time.deltaTime;
        }
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
