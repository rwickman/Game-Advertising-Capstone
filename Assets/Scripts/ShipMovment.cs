using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovment : MonoBehaviour
{
    public Vector3[] targetPos;
    public float minDistanceToPos = 0.5f;
    public float speed = 5f;
    public float rotSpeed = 5f;
    
    
    private int curPos = 0;

    private Vector3 lookDir;
    private Quaternion lookRot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, targetPos[curPos]) <= minDistanceToPos) {
            curPos = (curPos + 1) % targetPos.Length;
        }

        // Update position
        transform.position = Vector3.MoveTowards(transform.position, targetPos[curPos], speed * Time.deltaTime);
        
        // Update  rotation
        Vector3 lookDir = (targetPos[curPos] - transform.position).normalized;
        lookRot = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);
    }
}
