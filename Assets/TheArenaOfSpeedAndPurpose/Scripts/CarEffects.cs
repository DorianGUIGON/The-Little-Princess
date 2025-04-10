using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CarEffects : MonoBehaviour
{
    public CarMovement carMovement;
    public Rigidbody rg;

    [Header("Sounds and sfx")]
    public AudioClip engineSfx;
    public float engineSfxVelocityPitchFactor = 0.15f; // Facteur réduit pour éviter des sons trop aigus
    public float engineSfxBasePitch = 0.6f; // Tonalité plus grave
    public AudioClip[] collisionSfxs;

    // Private vars
    private AudioSource audioSource;
    private float divide = 7f; // Augmenté pour réduire le volume des collisions
    private float minus = 0.5f; // Diminué pour des impacts plus doux

    void Awake()
    {
        // Initialisation de l'AudioSource
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = engineSfx;
        audioSource.volume = 0.5f;
        audioSource.loop = true;
        audioSource.spatialBlend = 1;
        audioSource.minDistance = 25;
        audioSource.spread = 360;
        audioSource.Play();
    }

    void Update()
    {
        UpdateEngineSfx();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayCollisionSfx(collision);
    }

    private void PlayCollisionSfx(Collision collision)
    {
        if (!rg || !audioSource || collision == null || collisionSfxs.Length == 0)
        {
            return;
        }

        audioSource.pitch = Random.Range(0.85f, 1f);

        // Calcul du volume en limitant l'intensité max
        float volumeScale = Mathf.Clamp((collision.relativeVelocity.magnitude / divide) - minus, 0.1f, 0.4f);
        audioSource.PlayOneShot(collisionSfxs[Random.Range(0, collisionSfxs.Length)], volumeScale);
    }

    private void UpdateEngineSfx()
    {
        if (!rg || !audioSource)
        {
            return;
        }

        audioSource.pitch = engineSfxBasePitch + rg.velocity.magnitude * engineSfxVelocityPitchFactor;
    }
}
