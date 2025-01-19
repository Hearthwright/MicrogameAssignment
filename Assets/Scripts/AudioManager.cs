using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip countdown;
    public AudioClip move;
    public AudioClip win;
    public AudioClip lose;

    public GameManager gameManager;
    public TitleScreenManager titleScreenManager;

    private void Start()
    {
        SFXSource.volume = 0.5f;

        musicSource.clip = background;
        musicSource.loop = true;
        musicSource.volume = 0.5f;
        musicSource.Play();
    }

    private void Update()
    {
        // If Game has ended, stop background music
        if(GameManager.isGameOver || GameManager.hasWon)
        {
            musicSource.Stop();
        }
    }

    // Plays a sound effect
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}