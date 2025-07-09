using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    public AudioSource audioSource;

    public AudioClip playerHitSound;
    public AudioClip enemyHitSound;
    public AudioClip deathSound;
    public AudioClip victorySound;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep it between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerHit()
    {
        audioSource.PlayOneShot(playerHitSound);
    }

    public void PlayEnemyHit()
    {
        audioSource.PlayOneShot(enemyHitSound);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayVictorySound()
    {
        audioSource.PlayOneShot(victorySound);
    }


}
