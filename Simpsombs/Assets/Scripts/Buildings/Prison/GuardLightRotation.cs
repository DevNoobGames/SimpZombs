using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardLightRotation : MonoBehaviour
{
    public bool forward;
    public float maxRotation;
    public float minRotation;
    public float moveSpeed;
    public bool WeirdEulerNumbers;

    void Start()
    {
        forward = true;
    }

    void Update()
    {
        if (WeirdEulerNumbers)
        {
            if (forward == true)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y >= maxRotation && transform.eulerAngles.y <= minRotation)
                {
                    forward = false;
                }
            }
            else if (forward == false)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= minRotation && transform.eulerAngles.y >= maxRotation)
                {
                    forward = true;
                }
            }
        }
        else if (!WeirdEulerNumbers)
        {
            if (forward == true)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y >= maxRotation)
                {
                    forward = false;
                }
            }
            else if (forward == false)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - moveSpeed, transform.eulerAngles.z);
                if (transform.eulerAngles.y <= minRotation)
                {
                    forward = true;
                }
            }
        }
    }
}
