using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    private Vector3 offset = new Vector3(0, 1.5f, -10);
    private float moveSpeed = 0.1f;
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 sumPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, sumPosition, moveSpeed);

        transform.position = smoothPosition;
    }
}
