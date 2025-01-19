using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveDistance = 3.2f; // Determines the distance the player moves every time a key is pressed
    public float moveDelay = 0.2f; // Delay between inputs being read


    // Determines the bounds of the play area
    public float minX = -6.4f;
    public float maxX = 6.4f;
    public float minY = -4.8f;
    public float maxY = 4.8f;

    private float lastMoveTime = - Mathf.Infinity; // Tracks the time since last move

    AudioManager audioManager;

    public ParticleSystem Dust;

    public GameManager gameManager;
    public TitleScreenManager titleScreenManager;
    private Spike spikeScript;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {

        // Set the starting position to "(3, 2)" on the grid
        transform.position = new Vector3(0, 0, 0);

        GameObject spikeObject = GameObject.Find("Spikes");
        if (spikeObject != null)
        {
            spikeScript = spikeObject.GetComponent<Spike>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Player should only be permitted to move, if the game has started, the player has not lost, and the player has not won
        if(TitleScreenManager.gameStarted && !GameManager.isGameOver && !GameManager.hasWon)
        {
            // Only allows movement if enough time has passed, to allow for more precision on the player's part
            if (Time.time - lastMoveTime < moveDelay)
            {
                return;
            }

            // Getting player input to determine player movement
            float horizontal = 0f;
            float vertical = 0f;

            // Player moves up
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                vertical = moveDistance;
            }
            // Player moves down
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                vertical = -moveDistance;
            }
            // Player moves right
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal = -moveDistance;
            }
            // Player moves left
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontal = moveDistance;
            }

            // If there is any movement...
            if (horizontal != 0f || vertical != 0f)
            {
                // Calculate potential new position
                Vector2 currentPosition = transform.position;
                Vector2 newPosition = currentPosition + new Vector2(horizontal, vertical);

                // Check if the new position is within the defined boundaries
                if (newPosition.x >= minX && newPosition.x <= maxX && newPosition.y >= minY && newPosition.y <= maxY)
                {   
                    // Apply the movement only if within bounds
                    transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

                    // Play Audio as player moves
                    audioManager.PlaySFX(audioManager.move);
                    
                    // Create dust particles as player moves
                    CreateDust();
            
                    // Update the time of the last move
                    lastMoveTime = Time.time;
                    Debug.Log("Moved");
                }
            }
            // Check if player is currently on active spikes
            CheckForActiveSpikes();
        }
        
    }
    

    // Check if the player is standing on active spikes
    private void CheckForActiveSpikes()
    {
        // Use Physics2D.OverlapPointAll to check if the player's position is within an active spike's collider
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);  // Get all colliders at the player's position

        foreach (var hit in hits)
        {
            // Check if the collider is a spike and is active
            if (hit.CompareTag("Spike"))
            {
                Spike spike = hit.GetComponent<Spike>(); // Reference the Spike script
                if (spike != null && spike.IsActive()) // Check if the spike is active
                {
                    // Trigger the effect if the spike is active
                    TriggerSpikeEffect();
                }
            }
        }
    }

    // Trigger effect when player is standing on the active spikes
    private void TriggerSpikeEffect()
    {
        //Player looses
        GameManager.GameOver();
        Debug.Log("Player is standing on active spikes!");
    }

    // Start producing Dust Particles
    void CreateDust()
    {
        Dust.Play();
    }
}


