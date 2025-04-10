using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform car; 
    public Vector3 offset = new Vector3(0, 2, -5); 
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (car == null) return;

        Vector3 desiredPosition = car.position + car.transform.TransformDirection(offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, car.rotation, smoothSpeed * Time.deltaTime);
    }
}
