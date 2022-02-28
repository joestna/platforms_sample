using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    Vector3 offset;
    public float smoothing = 5f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        // La camara se mueve siguiendo al personaje pero con un poco de retardo dando un efecto mas animado y dinamico
        transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
    }
}
