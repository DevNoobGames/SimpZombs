﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoorFront : MonoBehaviour
{
    public bool isOpened;
    public GameObject UnlockCosts;
    public GameObject DoorFront;
    public GameObject DoorGarage;
    public GameObject TextDisplay;
    public GameObject player;
    public float TheDistance;
    public AudioSource DoorOpenSound;

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
                TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().HouseMainDoor + "$" + "\n" + "OPEN DOOR";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().HouseMainDoor)
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
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().HouseMainDoor;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";
        DoorOpenSound.Play();

        //Add spawnpoints
        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointOutside"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }

        //If needed, remove colliders. Wait for animation to finish
        this.GetComponent<Collider>().enabled = false;
        DoorFront.GetComponent<Animator>().enabled = true;
        DoorGarage.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.9f);

        //Finish animation, confirm isOpened, and if needed, destroy the obj
        DoorGarage.GetComponent<OpenGarageDoor>().isOpened = true;
        DoorFront.GetComponent<Animator>().enabled = false;
        DoorGarage.GetComponent<Animator>().enabled = false;
        //Destroy(gameObject);
    }
}
