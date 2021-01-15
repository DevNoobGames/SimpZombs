using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoors : MonoBehaviour
{
    public GameObject UnlockCosts;
    public GameObject Door1;
    public GameObject Door2;
    public GameObject TextDisplay;
    public GameObject destroyCollider;
    public GameObject player;
    public float TheDistance;

    private void Start()
    {
        TheDistance = 99999;
    }
    void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(TheDistance);

        if (TheDistance <= 4)
        {
            TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().Prison + "$" + "\n" + "OPEN DOORS";
        }
        if (TheDistance > 4)
        {
            TextDisplay.GetComponent<Text>().text = "";
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().Prison)
            {
                StartCoroutine(OpenTheDoor());
            }
        }
    }
    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    IEnumerator OpenTheDoor()
    {
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().Prison;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString() + "$";
        Destroy(destroyCollider);

        foreach (GameObject Spawnpoint in GameObject.FindGameObjectsWithTag("SpawnPointPrison"))
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>().SpawnPoints.Add(Spawnpoint);
        }


        this.GetComponent<Collider>().enabled = false;
        Door1.GetComponent<Animator>().enabled = true;
        Door2.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(1.9f);
        Door1.GetComponent<Animator>().enabled = false;
        Door2.GetComponent<Animator>().enabled = false;
        Destroy(gameObject);
    }
}
