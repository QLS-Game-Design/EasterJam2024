using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!particleSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
