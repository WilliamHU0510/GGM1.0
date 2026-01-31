using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -100f);
    private float smoothTime = 0.25f;
    private Vector3 velocity= Vector3.zero;
    public Transform farBackground;//, middleBackground;
    private Vector2 lastPos;
    [SerializeField] private Transform target;

    // Update is called once per frame

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position,targetPosition, ref velocity, smoothTime  );

        //transform.position = new Vector3(target.position.x-9.5f,target.position.y+2f, transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);

        farBackground.position  += new Vector3(amountToMove.x,amountToMove.y,0f);
        //middleBackground.position += new Vector3(amountToMove.x*0.5f, amountToMove.y*0.5f,0f);
        lastPos = transform.position;
    }
}
