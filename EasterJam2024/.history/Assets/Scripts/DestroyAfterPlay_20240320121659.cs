using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        Destroy(particleSystem, 5);
    }


}
