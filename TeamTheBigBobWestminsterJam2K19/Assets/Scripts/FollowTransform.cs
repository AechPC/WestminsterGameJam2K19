using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform toFollow;

    private Vector3 positionOffset;

    private void Awake()
    {
        positionOffset = transform.position - toFollow.position;
    }
    private void Update()
    {
        transform.position = toFollow.position + positionOffset;
    }
}
