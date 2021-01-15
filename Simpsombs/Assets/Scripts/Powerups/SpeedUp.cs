using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUp : MonoBehaviour
{
    public SC_FPSController SpeedController;
    public GameObject player;
    public float TheDistance;
    public Text TextDisplay;
    public float Cost;
    public float StandardWalkingSpeed;
    public float StandardRunningSpeed;

    private void Start()
    {
        TheDistance = 99999;
        StandardWalkingSpeed = SpeedController.walkingSpeed;
        StandardRunningSpeed = SpeedController.runningSpeed;
    }

    private void OnMouseOver()
    {
        TheDistance = Vector3.Distance(transform.position, player.transform.position);

        if (TheDistance <= 4)
        {
            TextDisplay.GetComponent<Text>().text = "SPEED UP";
        }
        if (TheDistance > 4)
        {
            TextDisplay.GetComponent<Text>().text = "";
        }

        if (Input.GetButtonDown("Action"))
        {
            if (TheDistance <= 4 && player.GetComponent<PlayerStatistics>().Money >= Cost)
            {
                StartCoroutine(WalkQuicker());
            }
        }
    }

    public void OnMouseExit()
    {
        TextDisplay.GetComponent<Text>().text = "";
    }

    IEnumerator WalkQuicker()
    {
        player.GetComponent<PlayerStatistics>().Money -= Cost;
        player.GetComponent<PlayerStatistics>().MoneyText.text = player.GetComponent<PlayerStatistics>().Money.ToString();
        SpeedController.walkingSpeed = SpeedController.runningSpeed;
        SpeedController.runningSpeed += 3;
        yield return new WaitForSeconds(30);
        SpeedController.walkingSpeed = StandardWalkingSpeed;
        SpeedController.runningSpeed = StandardRunningSpeed;

    }

}
