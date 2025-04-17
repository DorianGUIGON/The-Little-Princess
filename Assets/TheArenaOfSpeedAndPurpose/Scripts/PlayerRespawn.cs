using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private LapManager lapManager;
    private Transform lastCheckpointTransform;

    // Variables pour stocker la position et rotation de départ
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        lapManager = FindObjectOfType<LapManager>();

        // Enregistre la position et la rotation initiales du joueur au lancement du jeu
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R appuyé");
            RespawnAtLastCheckpoint();
        }
    }

    public void UpdateLastCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpointTransform = checkpoint.transform;
        Debug.Log("Checkpoint mis à jour pour le respawn : " + checkpoint.name);
    }

    private void RespawnAtLastCheckpoint()
    {
        if (lastCheckpointTransform != null)
        {
            transform.position = lastCheckpointTransform.position;
            transform.rotation = lastCheckpointTransform.rotation;
            Debug.Log("Respawn au dernier checkpoint !");
        }
        else
        {
            transform.position = startPosition;
            transform.rotation = startRotation;
            Debug.Log("Respawn au start");
        }
    }
}
