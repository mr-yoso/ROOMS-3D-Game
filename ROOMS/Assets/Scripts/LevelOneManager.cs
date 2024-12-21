using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneManager : MonoBehaviour
{
    public static LevelOneManager Instance { get; private set; } // Singleton instance

    [Header("Scene Transition")]
    public string nextSceneName = "LowPoly Mysterious Dungeon_Demo"; // Name of the next scene
    public float delayBeforeTransition = 2f; // Delay before transitioning

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Keep across scenes if needed
    }

    private void Start()
    {
        ResetObjectives();
    }

    private void ResetObjectives()
    {
        if (ObjectiveManager.Instance != null)
        {
            ObjectiveManager.Instance.ResetObjective();
        }
        else
        {
            Debug.LogWarning("ObjectiveManager instance not found.");
        }
    }

    public void TransitionToNextScene()
    {
        Debug.Log("Transitioning to the next scene...");
        Invoke(nameof(LoadNextScene), delayBeforeTransition);
    }

    private void LoadNextScene()
    {
        // Cursor.lockState = CursorLockMode.None;
        // Cursor.visible = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
