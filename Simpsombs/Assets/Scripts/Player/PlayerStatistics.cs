using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatistics : MonoBehaviour
{
    public float Money;
    public int health;
    public int MaxHealth;
    public int EnemiesKilled;
    public bool Attackable;
    public Text MoneyText;
    public bool CouroRunning;
    public bool HealthCouroRunning;
    public Animation HealthReset;

    public GameObject Health1;
    public GameObject Health2;
    public GameObject Health3;

    [Header ("LoseScreen")]
    public GameObject LoseScreen;
    public Text LoseScreenScoreText;

    [Header("StartScreen")]
    public GameObject Startscreen;

    void Start()
    {
        Cursor.visible = true;
        GameObject.FindGameObjectWithTag("FPSController").GetComponent<SC_FPSController>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Startscreen.SetActive(true);
        Attackable = true;
        MoneyText.text = Money + "$";
    }

    public void UpdateMoneyText()
    {
        MoneyText.text = Money + "$";
    }

    private void Update()
    {
        if (Attackable == false && CouroRunning == false)
        {
            CouroRunning = true;
            StartCoroutine(ResetAttack());
        }
        if (health == 2)
        {
            Health3.SetActive(false);
        }
        if (health == 1)
        {
            Health2.SetActive(false);
            Health3.SetActive(false);
        }
        if (health == 0)
        {
            Health1.SetActive(false);
            Health2.SetActive(false);
            Health3.SetActive(false);
            Debug.Log("GameOver");
            Time.timeScale = 0;
            LoseScreen.SetActive(true);
            GameObject.FindGameObjectWithTag("FPSController").GetComponent<SC_FPSController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            LoseScreenScoreText.text = "You killed " + EnemiesKilled + " Zombies";
        }
    }
    
    public void StartPlaying()
    {
        Startscreen.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        GameObject.FindGameObjectWithTag("FPSController").GetComponent<SC_FPSController>().enabled = true;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1);
        Attackable = true;
        CouroRunning = false;
    }

    public void HealthRunner()
    {
        StartCoroutine(ResetHealth());
    }

    IEnumerator ResetHealth()
    {
        yield return new WaitForSeconds(1);
        if (health < MaxHealth)
        {
            health = MaxHealth;
            Health1.SetActive(true);
            Health2.SetActive(true);
            Health3.SetActive(true);
            HealthReset.Play("HitAnim");
            HealthCouroRunning = false;
        }
    }

}
