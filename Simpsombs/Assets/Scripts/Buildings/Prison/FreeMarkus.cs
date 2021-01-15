using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreeMarkus : MonoBehaviour
{
    public Animator MarkusAnim;
    public GameObject Markus;
    public Collider MarkusOldCollider;
    public Collider MarkusNewCollider;
    public GameObject LittleBazooka;
    public GameObject UnlockCosts;
    public GameObject OldPrison;
    public GameObject NewPrison;
    public GameObject player;
    public float TheDistance;
    public GameObject TextDisplay;


    void Start()
    {
        TheDistance = 99999;
    }

    void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        //Debug.Log(TheDistance);

        if (TheDistance <= 4)
        {
            TextDisplay.GetComponent<Text>().text = UnlockCosts.GetComponent<UnlockCosts>().PrisonCell + "$" + "\n" + "FREE MARKUS";
        }
        if (TheDistance > 4)
        {
            TextDisplay.GetComponent<Text>().text = "";
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= UnlockCosts.GetComponent<UnlockCosts>().PrisonCell)
            {
                //StartCoroutine(OpenTheDoor());
                OpenDoor();
            }
        }
    }
    void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    /*IEnumerator OpenTheDoor()
    {
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().PrisonCell;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString();
        yield re
        Destroy(gameObject);
    }*/

    void OpenDoor()
    {
        player.GetComponent<PlayerStatistics>().Money -= UnlockCosts.GetComponent<UnlockCosts>().PrisonCell;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString();
        Instantiate(NewPrison, OldPrison.transform.position, transform.rotation);
        MarkusAnim.enabled = true;
        MarkusOldCollider.enabled = false;
        MarkusNewCollider.enabled = true;
        Markus.GetComponent<BuyBazooka>().enabled = false;
        LittleBazooka.GetComponent<BuyBazooka>().enabled = true;
        Destroy(OldPrison);
        Destroy(gameObject);
    }
}
