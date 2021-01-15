﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBarDoor : MonoBehaviour
{
    public bool isOpened;
    public GameObject UnlockCosts;
    public GameObject DoorHinge;
    public GameObject TextDisplay;
    public GameObject player;
    public float TheDistance;

    private void Start()
    {
        TheDistance = 99999;
    }
    void OnMouseOver()
    {
        if (!isOpened)
        {
            TheDistance = Vector3.Distance(transform.position, player.transform.position);

            //Debug.Log(TheDistance);

            if (TheDistance <= 4)
            {
                TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().Bar + "$" + "\n" + "OPEN DOOR";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().Bar)
                {
                    StartCoroutine(OpenTheDoor());
                }
            }
        }
    }

    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    IEnumerator OpenTheDoor()
    {
        //Set money
        isOpened = true;
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().Bar;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";

        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointBar"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }

        //If needed, remove colliders. Wait for animation to finish
        //this.GetComponent<Collider>().enabled = false;
        DoorHinge.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.9f);

        //Finish animation, confirm isOpened, and if needed, destroy the obj
        DoorHinge.GetComponent<Animator>().enabled = false;
        //Destroy(gameObject);
    }
}
