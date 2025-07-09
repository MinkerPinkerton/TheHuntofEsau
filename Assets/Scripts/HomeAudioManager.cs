using UnityEngine;

public class HomeAudioManager : MonoBehaviour
{

    public static HomeAudioManager instance;

    public AudioSource audioSource;

    public AudioClip cookingMeatSound;


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

    public void CookMeatOutside()
    {
        audioSource.PlayOneShot(cookingMeatSound);
    }


}
