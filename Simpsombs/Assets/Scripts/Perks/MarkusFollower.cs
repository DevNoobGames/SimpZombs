using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MarkusFollower : MonoBehaviour
{
    public GameObject Markus;
    public bool Running;
    public GameObject player;
    public float TheDistance;
    public GameObject TextDisplay;
    public GameObject UnlockCosts;
    public GameObject[] Zombies;
    public GameObject MarkusPerkImg;

    void Start()
    {
        MarkusPerkImg.SetActive(false);
        Running = false;
        TheDistance = 99999;
    }

    void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(TheDistance);

        if (TheDistance <= 4 && !Running)
        {
            TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().MarkusFollower + "$" + "\n" + "ZOMBIES CHASE MARKUS";
        }
        if (TheDistance > 4)
        {
            TextDisplay.GetComponent<Text>().text = "";
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().MarkusFollower && !Running)
            {
                //StartCoroutine(OpenTheDoor());
                Zombies = GameObject.FindGameObjectsWithTag("BasicZombie");
                TextDisplay.GetComponent<Text>().text = "";
                StartCoroutine(FollowMarkus());
            }
        }
    }
    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    IEnumerator FollowMarkus()
    {
        Running = true;
        MarkusPerkImg.SetActive(true);
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().MarkusFollower;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString();

        //This takes 30 seconds, it's tested//
        foreach (GameObject Zomb in Zombies)
        {
            Zomb.GetComponent<AIMovement>().Player = Markus;
        }

        yield return new WaitForSeconds(10);
        foreach (GameObject Zomb in Zombies)
        {
            Zomb.GetComponent<AIMovement>().Player = Markus;
        }

        yield return new WaitForSeconds(10);
        foreach (GameObject Zomb in Zombies)
        {
            Zomb.GetComponent<AIMovement>().Player = Markus;
        }

        yield return new WaitForSeconds(10);

        foreach (GameObject Zomb in Zombies)
        {
            Zomb.GetComponent<AIMovement>().Player = player;
        }
        Running = false;
        MarkusPerkImg.SetActive(false);
    }

}
