using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TitleScreenManager : MonoBehaviour
{
    public GameObject titleScreenUI;  // Reference to the Title Screen UI
    public TextMeshProUGUI startText; // Text that appears on the Title Screen

    AudioManager audioManager;

    public static bool gameStarted = false;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        // Game does not start immediatly
        gameStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for space to be pressed to start the game
        if (!gameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(StartGameWithDelay());
        }
    }

    // Coroutine to delay the start of the game by 2 seconds
    private IEnumerator StartGameWithDelay()
    {
        float countdown = 2f; // Start the countdown at 2 seconds

        // Playing Countdown Audio
        audioManager.PlaySFX(audioManager.countdown);

        // While there's time left on the countdown
        while (countdown > 0f)
        {
            // Update the text to show the countdown
            startText.text = "Starting in " + Mathf.Ceil(countdown) + " seconds..."; 
            
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease the countdown
            countdown -= 1f;
        }

        // Once the countdown reaches 0, start the game
        gameStarted = true;
        
        // Hide the title screen
        titleScreenUI.SetActive(false);

        Debug.Log("Game Started!");
    }
}