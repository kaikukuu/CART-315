using System.Collections;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public enum ButtonColor
    {
        Blue = 1,
        Green = 2,
        Yellow = 3,
        Orange = 4,
        Red = 5
    }
    // From right to left
    public enum ButtonColumn
    {
        C1,
        C2,
        C3,
        C4,
        C5
    }
    public enum ButtonRow
    {
        // R1 is the bottom row and R3 is the top row
        R1,
        R2,
        R3
    }

    [System.Serializable]
    public struct PatternStep
    {
        public ButtonColor color;
        public ButtonColumn column;
        public ButtonRow row;
    }

    private const int ColumnCount = 5;
    private const int RowCount = 3;

    [Header("Tiles: C1-R1, C1-R2, C1-R3, then C2-R1 through C5-R3")]
    [SerializeField] private Brick[] gridTiles = new Brick[ColumnCount * RowCount];

    [Header("Button visuals: assign C1 through C5 in order")]
    [SerializeField] private Transform[] columnButtonVisuals = new Transform[ColumnCount];

    [SerializeField, Min(1f)] private float selectedScaleMultiplier = 1.12f; // scale multiplier for the selected column button visual
    [SerializeField, Min(0.05f)] private float tileFlashDuration = 0.6f; // duration for which a tile flashes when activated
    [SerializeField, Min(0f)] private float delayBetweenSteps = 0.5f;

    // Each step specifies how many times Space is pressed, the selected column, and the tile to flash.
    private readonly PatternStep[][] patterns =
    {
        new[]
        {
            new PatternStep { color = ButtonColor.Blue, column = ButtonColumn.C1, row = ButtonRow.R1 },
            new PatternStep { color = ButtonColor.Green, column = ButtonColumn.C2, row = ButtonRow.R2 },
            new PatternStep { color = ButtonColor.Yellow, column = ButtonColumn.C3, row = ButtonRow.R3 },
            new PatternStep { color = ButtonColor.Blue, column = ButtonColumn.C4, row = ButtonRow.R1 },
            new PatternStep { color = ButtonColor.Green, column = ButtonColumn.C5, row = ButtonRow.R2 }
        },
        new[]
        {
            new PatternStep { color = ButtonColor.Blue, column = ButtonColumn.C5, row = ButtonRow.R1 },
            new PatternStep { color = ButtonColor.Green, column = ButtonColumn.C4, row = ButtonRow.R2 },
            new PatternStep { color = ButtonColor.Yellow, column = ButtonColumn.C3, row = ButtonRow.R3 },
            new PatternStep { color = ButtonColor.Green, column = ButtonColumn.C2, row = ButtonRow.R2 },
            new PatternStep { color = ButtonColor.Yellow, column = ButtonColumn.C1, row = ButtonRow.R3 }
        }
    };

    private readonly Vector3[] originalButtonScales = new Vector3[ColumnCount];
    private int currentPatternIndex;
    private int currentStep;
    private int spacePresses;
    private ButtonColumn selectedColumn = ButtonColumn.C1;
    private bool acceptingInput;
    private Coroutine patternCoroutine; // Coroutine reference to manage the pattern playback coroutine

    private void Start()
    {
        if (!ValidateSceneReferences())
        {
            // Disable the Buttons controller if the scene references are not valid to prevent further errors
            enabled = false;
            return;
        }

        CacheOriginalScales();
        UpdateSelectionVisual();
        RestartRound();
    }

    private void Update()
    {
        // Only accept input if the game is currently accepting it
        if (!acceptingInput)
            return;
        // Handle player input for moving the selected column and counting Space presses
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveColumn(-1); // move left
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveColumn(1); // move right
        }
        else if (Input.GetKeyDown(KeyCode.Space)) // count Space presses for the current step
        {
            CountPress();
        }
    }

    private bool ValidateSceneReferences()
    {
        // Ensure that there is exactly one Buttons controller in the scene and that all required references are assigned correctly
        Buttons[] sceneControllers = FindObjectsByType<Buttons>(FindObjectsSortMode.None);
        if (sceneControllers.Length != 1)
        {
            Debug.LogError($"The scene must contain exactly one Buttons controller. Found {sceneControllers.Length}.", this);
            return false;
        }

        if (gridTiles == null || gridTiles.Length != ColumnCount * RowCount)
        {
            Debug.LogError($"Set Grid Tiles to exactly {ColumnCount * RowCount} items in the Inspector.", this);
            return false;
        }

        for (int i = 0; i < gridTiles.Length; i++)
        {
            if (gridTiles[i] == null)
            {
                int column = i / RowCount + 1;
                int row = i % RowCount + 1;
                Debug.LogError($"Assign tile C{column}-R{row} to Grid Tiles, item {i}.", this);
                return false;
            }
        }

        if (columnButtonVisuals == null || columnButtonVisuals.Length != ColumnCount)
        {
            Debug.LogError($"Set Column Button Visuals to exactly {ColumnCount} items in the Inspector.", this);
            return false;
        }

        for (int i = 0; i < columnButtonVisuals.Length; i++)
        {
            if (columnButtonVisuals[i] == null)
            {
                Debug.LogError($"Assign the C{i + 1} button visual to Column Button Visuals, item {i}.", this);
                return false;
            }
        }

        return true;
    }
    // Saves original scales to restore the button visuals after scaling them for selection
    private void CacheOriginalScales()
    {
        for (int i = 0; i < columnButtonVisuals.Length; i++)
            originalButtonScales[i] = columnButtonVisuals[i].localScale;
    }

    // Updates the visual representation of the selected column by scaling the corresponding button visual
    private void UpdateSelectionVisual()
    {
        for (int i = 0; i < columnButtonVisuals.Length; i++)
        {
            float multiplier = i == (int)selectedColumn ? selectedScaleMultiplier : 1f;
            columnButtonVisuals[i].localScale = originalButtonScales[i] * multiplier;
        }
    }

    // Moves the selected column left or right based on the input direction
    private void MoveColumn(int direction)
    {
        // Clamp the selected column to ensure it stays within the valid range of columns
        int nextColumn = Mathf.Clamp((int)selectedColumn + direction, 0, ColumnCount - 1);
        selectedColumn = (ButtonColumn)nextColumn;
        UpdateSelectionVisual();
    }

    // Count the number of Space presses and check if the player's input matches the expected pattern step
    private void CountPress()
    {
        PatternStep[] pattern = patterns[currentPatternIndex];
        PatternStep expected = pattern[currentStep];
        spacePresses++;

        if (spacePresses == (int)expected.color)
        {
            if (selectedColumn != expected.column)
            {
                HandleIncorrectInput();
                return;
            }

            acceptingInput = false;
            StartCoroutine(ConfirmCorrectStep(expected));
        }
    }
    // Confirm that the player's input for the current step is correct, flash the corresponding tile, and proceed to the next step or complete the pattern
    private IEnumerator ConfirmCorrectStep(PatternStep step)
    {
        PatternStep[] pattern = patterns[currentPatternIndex];
        int acceptedStep = currentStep + 1;
        Debug.Log($"Correct input: pattern {currentPatternIndex + 1}, step {acceptedStep}/{pattern.Length} ({step.color}, {step.column}, {step.row}).", this);

        Brick tile = gridTiles[GetGridIndex(step.column, step.row)];
        yield return StartCoroutine(tile.Flash(tileFlashDuration));

        currentStep++;
        spacePresses = 0;

        if (currentStep == pattern.Length)
        {
            HandlePatternComplete();
            yield break;
        }

        acceptingInput = true;
        Debug.Log($"Step {acceptedStep} accepted. Continue with step {currentStep + 1}.", this);
    }

    private int GetGridIndex(ButtonColumn column, ButtonRow row)
    {
        // The array stores all three rows of C1, then all three rows of C2, and so on.
        return (int)column * RowCount + (int)row;
    }
    // Play the pattern, then allow player input after the sequence is shown.
    private IEnumerator PlayPattern()
    {
        PatternStep[] pattern = patterns[currentPatternIndex];
        acceptingInput = false;
        currentStep = 0;
        spacePresses = 0;
        selectedColumn = ButtonColumn.C1;
        UpdateSelectionVisual();

        foreach (PatternStep step in pattern)
        {
            Brick tile = gridTiles[GetGridIndex(step.column, step.row)];
            yield return StartCoroutine(tile.Flash(tileFlashDuration));

            if (delayBetweenSteps > 0f)
                yield return new WaitForSeconds(delayBetweenSteps);
        }

        acceptingInput = true;
        patternCoroutine = null;
        Debug.Log("Pattern playback complete. Enter the sequence with A/D or R/L arrows and Space.", this);
    }

    private IEnumerator PlayNextPattern()
    {
        if (delayBetweenSteps > 0f)
            yield return new WaitForSeconds(delayBetweenSteps);

        yield return StartCoroutine(PlayPattern());
    }
    // Restart the round by stopping any ongoing pattern playback and starting the pattern playback coroutine again
    public void RestartRound()
    {
        if (patternCoroutine != null)
            StopCoroutine(patternCoroutine);

        acceptingInput = false;
        patternCoroutine = StartCoroutine(PlayPattern());
    }

    // Handle the case when the player inputs an incorrect pattern step, stop accepting input, and notify the GameManager of the incorrect input
    private void HandleIncorrectInput()
    {
        acceptingInput = false;
        Debug.LogWarning("Incorrect pattern input.", this);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnIncorrectPlayerInput();
        }
        else
        {
            RestartRound();
        }
    }
    // Handle the completion of the pattern by stopping input and notifying the GameManager that the pattern has been completed
    private void HandlePatternComplete()
    {
        acceptingInput = false;
        Debug.Log($"Pattern {currentPatternIndex + 1} complete!", this);

        if (currentPatternIndex + 1 < patterns.Length)
        {
            currentPatternIndex++;
            patternCoroutine = StartCoroutine(PlayNextPattern());
        }
        else if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPatternCompleted();
        }
    }
}
