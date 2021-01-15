using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenFrontDoors : MonoBehaviour
{
    public GameObject UnlockCosts;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject TextDisplay;
    public GameObject player;
    public float TheDistance;
    public bool isOpened;

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
                TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().Store + "$" + "\n" + "OPEN DOORS";
            }
            if (TheDistance > 4)
            {
                TextDisplay.GetComponent<Text>().text = "";
            }

            if (Input.GetButtonDown("Action"))
            {
                if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().Store)
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
        isOpened = true;
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().Store;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";

        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointStore"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }

        this.GetComponent<Collider>().enabled = false;
        Door1.GetComponent<Animator>().enabled = true;
        Door2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(0.9f);
        Door1.GetComponent<Animator>().enabled = false;
        Door2.GetComponent<Animator>().enabled = false;
        Destroy(gameObject);
    }
}
