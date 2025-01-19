using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameEndPanel; // Reference to the Game End screen panel
    public TextMeshProUGUI gameEndText; // Reference to Game End Text

    AudioManager audioManager;

    public static bool isGameOver = false; // Tracks if the player has lost
    public static bool hasWon = false; //Tracks if the player has won

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Panel should start out disabled
        if (gameEndPanel != null)
        {
            gameEndPanel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // If the player gets a game over, checks to see if they press space. If so, then restarts the game.
        if (isGameOver || hasWon)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                RestartGame();
            }
        }
        // Checks if the player has won or lost, and plays the apropriate SFX and displays the appropriate end screen
        if (hasWon)
        {
            audioManager.PlaySFX(audioManager.win);
            DisplayGameEndScreen("You Win! \n \n Press [Space] to restart!", Color.green); // Win text
        }
        else if (isGameOver)
        {
            audioManager.PlaySFX(audioManager.lose);
            DisplayGameEndScreen("Game Over! \n \n Press [Space] to restart!", Color.red); // Game Over text
        }
    }

    // Used to define a lose state
    public static void GameOver()
    {
        // If the game has not ended in a win...
        if(!hasWon)
        {
            // Set isGameOver to true
            isGameOver = true;
            Debug.Log("isGameOver is now true!");
            
        }
    }

    // Used to define a win state
    public static void WinCon()
    {
        // If the game has not ended in a loss...
        if(!isGameOver)
        {
            // Set has Won to true
            hasWon = true;
            Debug.Log("hasWon is now true!");
            
        }
    }

    public void DisplayGameEndScreen(string message, Color textColor)
    {
        Debug.Log("DisplayEndScreen Called");
        // Displays an end screen if either the player lost or won
        if (isGameOver || hasWon)
        {
            if (gameEndPanel != null)
            {
                gameEndPanel.SetActive(true);
            }
            if (gameEndText != null)
            {
                gameEndText.text = message;  // Set the message based on whether it's game over or win
                gameEndText.color = textColor; // Set color (green for win, red for game over)
            }
        }
    }



    // Restart the game
    void RestartGame()
    {
        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
        hasWon = false;
    }
}
