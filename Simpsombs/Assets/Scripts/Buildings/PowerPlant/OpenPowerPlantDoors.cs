using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPowerPlantDoors : MonoBehaviour
{
    public bool isOpened;
    public GameObject UnlockCosts;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject TextDisplay;
    public GameObject player;
    public float TheDistance;

    public GameObject box1;
    public GameObject box2;


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
                TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().Powerplant + "$" + "\n" + "OPEN SCHOOL";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().Powerplant)
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
        box1.GetComponent<OpenPowerPlantDoors>().isOpened = true;
        box2.GetComponent<OpenPowerPlantDoors>().isOpened = true;
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().Powerplant;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";

        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointNuclear"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }

        //If needed, remove colliders. Wait for animation to finish
        box1.GetComponent<Collider>().enabled = false;
        box2.GetComponent<Collider>().enabled = false;
        Door1.GetComponent<Animator>().enabled = true;
        Door2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.9f);

        //Finish animation, confirm isOpened, and if needed, destroy the obj
        Door1.GetComponent<Animator>().enabled = false;
        Door2.GetComponent<Animator>().enabled = false;
        //Destroy(gameObject);
    }
}
