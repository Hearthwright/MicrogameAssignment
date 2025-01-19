using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Sprite inactiveSprite; // Sprite for when spikes are inactive
    public Sprite warningSprite; // Sprite for when spikes are about to be active
    public Sprite activeSprite; // Sprite for when spikes are active
    public float activationChance = 0.25f; //Determines chance of spike activationg (Default 25%)

    private SpriteRenderer spriteRenderer;
    private bool isActive = false; // Determines if the spikes are currently active
    private bool isWarning = false; //Determines if the spike are in a warning state
    private float activeTime = 0f; // Tracks how long spikes have been active
    private float cooldownTime = 0f; // Tracks how long it has been since spikes were last active

    // Reference to the GameManager to trigger Game Over
    public GameManager gameManager;
    public TitleScreenManager titleScreenManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inactiveSprite;
    }

    private void Update()
    {
        // Player is only permitted to move, if the game has started, the player has not lost, and the player has not won
        if(TitleScreenManager.gameStarted && !GameManager.isGameOver && !GameManager.hasWon)
        {
            activeTime += Time.deltaTime;

            // Handle cooldown and activation cycle
            if (cooldownTime > 0f)
            {
                cooldownTime -= Time.deltaTime;
                if (cooldownTime <= 0f)
                {
                    // Reset to inactive state after cooldown period
                    isActive = false;
                    spriteRenderer.sprite = inactiveSprite;
                }
            }
            else if (activeTime >= 1f)
            {
                // Reset the timer every second
                activeTime = 0f;

                // Decide whether the spikes should activate
                // Spikes cannot activate if they are already in a warning state
                if (Random.value < activationChance && !isWarning)
                {
                    StartCoroutine(ActivateSpikes());
                }
            }
        }
        else
        {
            StopCoroutine(ActivateSpikes());
        }
    }

    private IEnumerator ActivateSpikes()
    {
        // Show warning sprite for 1 second
        isWarning = true;
        spriteRenderer.sprite = warningSprite;
        yield return new WaitForSeconds(1f);

        // Spike is now active
        isWarning = false;
        isActive = true;
        Debug.Log("Spikes Active");
        spriteRenderer.sprite = activeSprite;  // Change to active sprite

        cooldownTime = 1f; // 1 second cooldown after the spikes are active

        // Wait for 1 second while active
        yield return new WaitForSeconds(1f);

        // After 1 second, go into cooldown, spikes deactivate
        isActive = false;
        spriteRenderer.sprite = inactiveSprite;  // Set back to inactive sprite
    }

    // States whether or not the spikes are currently active
    public bool IsActive()
    {
        return isActive;
    }
}