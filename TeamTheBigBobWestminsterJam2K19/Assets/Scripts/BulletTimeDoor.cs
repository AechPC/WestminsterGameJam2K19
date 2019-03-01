using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeDoor : MonoBehaviour
{
    private Transform exorcistTransform;

    private Vector3 startPos;

    [SerializeField] private float closePlayerDis, closeSpeed, closeDis;
    private float closedAmount, closePlayerDisSqr;

    private void Awake()
    {
        startPos = transform.position;
        exorcistTransform = GameObject.FindWithTag("Exorcist").transform;
        closePlayerDisSqr = Mathf.Pow(closePlayerDis, 2);
    }

    private void FixedUpdate()
    {
        closedAmount = Mathf.Clamp(closedAmount + ((transform.position - exorcistTransform.position).sqrMagnitude < closePlayerDisSqr ? closeSpeed : -closeSpeed), 0, 1);
        transform.position = new Vector3(startPos.x, startPos.y - closeDis * closedAmount);
    }
}
