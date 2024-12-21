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
        CheckCursorState();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCursorState();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void CheckCursorState()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "WinMenu" || currentSceneName == "LoseMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
