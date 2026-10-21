using NUnit.Framework.Constraints;
using UnityEngine;

public class Buttons : MonoBehaviour
{

    public enum ButtonColor
    {
        Gray = 0,
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

    //example input and output
    // after the initial animation of the pattern the player would input which uses Buttons(pattern) where pattern is in the form 
    //pattern 1={(1,C1),(1,C2),(1,C3),(1,C4),(1,C5)}, the first level checks for 5 pairs of input to validate that the pattern is correct

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Brick color and column determines the button that is pressed and the number of times it needs to be pressed
    // This script will handle the logic for each button press and update the game state accordingly

    public void OnButtonPress(string buttonColor, string buttonColumn)
    {
        // enums to represent the button colors and columns

        switch (buttonColor)
        {
            case "Blue":
                // handle blue button press
                break;
            case "Green":
                // handle green button press
                break;
            case "Yellow":
                // handle yellow button press
                break;
            case "Orange":
                // handle orange button press
                break;
            case "Red":
                // handle red button press
                break;
            default:
                Debug.LogWarning("Unknown button color: " + buttonColor);
                break;
        }

        switch (buttonColumn)
        {
            case "C1":
                // handle column 1 button press
                break;
            case "C2":
                // handle column 2 button press
                break;
            case "C3":
                // handle column 3 button press
                break;
            case "C4":
                // handle column 4 button press
                break;
            case "C5":
                // handle column 5 button press
                break;
            default:
                Debug.LogWarning("Unknown button column: " + buttonColumn);
                break;
        }

        private void activatePlayerButtons()
    {
        //onResetGame()
        // start on /c1(leftmost)
        // the outline of the selected button is thicker
        //there are 5 buttons and 5 columns
        // brickObject Button1 is sellected by looping  thru the buttons and corresponds to column 1 C1 by using the a and d keys respectively
        // then check that the number of clicks of the space bar key correspond to its color
        //for simplicity's sake currently all of the columns are in ascending order of thei number of clicks

    }
    public void PlayerColumnSelect()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            
        }
      
    }
    
    }
}


