using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    public ParticleSystem particleSystem;

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
