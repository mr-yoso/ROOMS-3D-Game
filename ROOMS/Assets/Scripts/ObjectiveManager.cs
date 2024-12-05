using UnityEngine;
using TMPro;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance { get; private set; } // Singleton instance

    public int totalZombiesToKill = 20;
    private int zombiesKilled = 0;

    public TextMeshProUGUI objectiveText;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateObjectiveText();
    }

    public void ZombieKilled()
    {
        zombiesKilled++;
        UpdateObjectiveText();

        if (zombiesKilled >= totalZombiesToKill)
        {
            ObjectiveComplete();
        }
    }

    private void UpdateObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = $"Kill {zombiesKilled}/{totalZombiesToKill} Zombies";
        }
        else
        {
            Debug.LogWarning("Objective Text is not assigned in ObjectiveManager.");
        }
    }

    private void ObjectiveComplete()
    {
        objectiveText.text = "Objective Complete!";
        Debug.Log("Objective Complete!");
        if (LevelOneManager.Instance != null)
        {
            LevelOneManager.Instance.TransitionToNextScene();
        }
        else
        {
            Debug.LogError("LevelOneManager instance not found.");
        }
    }

    public void ResetObjective()
    {
        zombiesKilled = 0; // Reset kill count
        UpdateObjectiveText();
    }
}
