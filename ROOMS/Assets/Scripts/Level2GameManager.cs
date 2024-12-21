using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class Level2GameManager : MonoBehaviour
{
    public float delayBeforeTransition = 2f;
    public static Level2GameManager Instance;
    public TextMeshProUGUI objectiveText;
    public Boolean getGoblet = false;
    public GameObject enemy;
    public float interval = 1.5f;
    public List<Vector3> spawnPositions;
    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            DontDestroyOnLoad(gameObject);
            return;
        }

        Instance = this;
    }

    public void ResetObjective()
    {
        getGoblet = false;
    }

    public void PickUpObj()
    {
        getGoblet = true;
    }

    public void TriggerWaves()
    {
       // Debug.Log("wassup");
        if (getGoblet)
        {
            StartCoroutine(SpawnWaves());
            StartCoroutine(StartTimer(180));
            GameObject.FindGameObjectWithTag("MainRoom").SetActive(false);
            //Debug.Log("YOU ARE IN MAIN ROOM");
        }
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            StartCoroutine(SpawnEnemy(interval, enemy, spawnPositions, 3)); // Spawn for 10 seconds
            yield return new WaitForSeconds(30); // Pause for 5 seconds (or any desired duration)
        }
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy, List<Vector3> positions, float duration)
    {
        float spawnEndTime = Time.time + duration;
        while (Time.time < spawnEndTime)
        {
            foreach (var position in positions)
            {
                GameObject newEnemy = Instantiate(enemy, position, Quaternion.identity);
            }
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator StartTimer(float duration)
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            objectiveText.text = $"Survive the zombie waves for {remainingTime:F0} seconds";
            yield return new WaitForSeconds(1);
            remainingTime--;
        }
        objectiveText.text = "Objective Complete!";
        Invoke(nameof(ObjectiveComplete), delayBeforeTransition);
    }

    private void ObjectiveComplete()
    {
        objectiveText.text = "Objective Complete!";
        // Debug.Log("Objective Complete!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
