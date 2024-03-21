using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource musicSource;

    void Start()
    {
        
        musicSource.Play();
    }

    void Update()
    {
        // You can add more logic here if needed
    }
}
