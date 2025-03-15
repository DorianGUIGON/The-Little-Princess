using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 marginFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = player.transform.position + marginFromPlayer;
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
