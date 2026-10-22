using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 6;
    private Brick[] bricks;

    private Buttons buttons;
    public int level { get; private set; } = 1;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void FindSceneReferences()
    {
        bricks = FindObjectsByType<Brick>(FindObjectsSortMode.None);
        Buttons[] sceneButtons = FindObjectsByType<Buttons>(FindObjectsSortMode.None);

        if (sceneButtons.Length == 1)
        {
            buttons = sceneButtons[0];
        }
        else if (sceneButtons.Length == 0)
        {
            buttons = null;
            Debug.LogWarning("No Buttons controller was found in the active scene.", this);
        }
        else
        {
            buttons = null;
            Debug.LogError($"Expected one Buttons controller in the active scene, but found {sceneButtons.Length}.", this);
        }
    }

    private void LoadLevel(int levelId)
    {
        level = levelId;

        if (level > NUM_LEVELS)
        {
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            LoadLevel(1);
            return;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        FindSceneReferences();
    }

    public void OnIncorrectPlayerInput()
    {
        lives--;

        if (lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }

    private void ResetLevel()
    {
        if (buttons != null)
        {
            buttons.RestartRound();
            return;
        }

        Debug.LogError("Cannot restart the pattern because the active scene has no Buttons controller.", this);
    }

    private void GameOver()
    {
        // Start a new game immediately
        // You can also load a "GameOver" scene instead
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;

        LoadLevel(1);
    }

    public void OnButtonHit(Buttons buttons)
    {
        if (Cleared())
        {
            LoadLevel(level + 1);
        }
    }

    public void OnPatternCompleted()
    {
        Debug.Log($"Pattern for level {level} completed. Add the next hard-coded pattern before advancing.", this);
    }

    private bool Cleared()
    {

        return true;
    }

}
