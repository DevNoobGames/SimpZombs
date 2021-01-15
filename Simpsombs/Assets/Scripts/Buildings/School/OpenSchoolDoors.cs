using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSchoolDoors : MonoBehaviour
{
    public bool isOpened;
    public GameObject UnlockCosts;
    public GameObject DoorHinge1;
    public GameObject DoorHinge2;
    public GameObject DoorHinge3;
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
                TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().School + "$" + "\n" + "OPEN SCHOOL";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().School)
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
        box1.GetComponent<OpenSchoolDoors>().isOpened = true;
        box2.GetComponent<OpenSchoolDoors>().isOpened = true;
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().School;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";

        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointSchool"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }

        //If needed, remove colliders. Wait for animation to finish
        box1.GetComponent<Collider>().enabled = false;
        box2.GetComponent<Collider>().enabled = false;
        DoorHinge1.GetComponent<Animator>().enabled = true;
        DoorHinge2.GetComponent<Animator>().enabled = true;
        DoorHinge3.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.9f);

        //Finish animation, confirm isOpened, and if needed, destroy the obj
        DoorHinge1.GetComponent<Animator>().enabled = false;
        DoorHinge2.GetComponent<Animator>().enabled = false;
        DoorHinge3.GetComponent<Animator>().enabled = false;
        //Destroy(gameObject);
    }
}
