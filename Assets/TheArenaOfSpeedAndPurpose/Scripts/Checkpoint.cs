using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent<CarIdentity, Checkpoint> onCheckpointEnter;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<CarIdentity>() != null)
        {
            onCheckpointEnter.Invoke(collider.GetComponent<CarIdentity>(), this);
        }
    }
}