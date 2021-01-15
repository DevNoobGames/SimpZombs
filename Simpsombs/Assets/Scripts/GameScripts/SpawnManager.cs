using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> SpawnPoints;
    public int Level;
    public Text LevelText;
    public int SpawnedZombies;

    public GameObject Zombie;

    void Start()
    {
        foreach(GameObject SpawnPoint in GameObject.FindGameObjectsWithTag("SpawnPointGarage"))
        {
            SpawnPoints.Add(SpawnPoint);
        }
        SpawnedZombies = 0;
        Level = 1;
        StartCoroutine(PauseBeforeStarting());
    }

    public void CouroStarter()
    {
        LevelText.text = Level.ToString();
        StartCoroutine(PauseBeforeStarting());
    }

    IEnumerator PauseBeforeStarting()
    {
        LevelText.text = Level.ToString();
        yield return new WaitForSeconds(4);
        StartCoroutine(Spawner());
    }

    public IEnumerator Spawner()
    {
        //Debug.Log("Start with level " + Level + " and zombies " + SpawnedZombies);
        yield return new WaitForSeconds(0.1f);
        if (SpawnedZombies < Level)
        {
            //Debug.Log("Spawn");
            SpawnedZombies += 1;
            int SpawnPointer = Random.Range(0, SpawnPoints.Count);
            Instantiate(Zombie, SpawnPoints[SpawnPointer].transform.position, transform.rotation);
            if (SpawnedZombies < Level)
            {
                StartCoroutine(Spawner());
            }
        }
    }





}
