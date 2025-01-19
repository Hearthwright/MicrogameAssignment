using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime;

    public GameManager gameManager;
    public TitleScreenManager titleScreenManager;

    // Update is called once per frame
    void Update()
    {
        // Timer only works if the game has started, the player has not lost, and the player has not won
        if(TitleScreenManager.gameStarted && !GameManager.isGameOver && !GameManager.hasWon)
        {
            // If there is still time left in the game...
            if (remainingTime > 0)
            {
                // Reduces the remaining time
                remainingTime -= Time.deltaTime;
            }
            // If there is no more time left in the game...
            else if (remainingTime < 0)
            {
                // Sets time to 0, to prevent it from going into the negatives
                remainingTime = 0f;
                // Call GameManager to indicate game has been won
                GameManager.WinCon();
            }
            // Converting Time left to a string
            int seconds = Mathf.FloorToInt(remainingTime);
            timerText.text = seconds.ToString();
        }
    }
}