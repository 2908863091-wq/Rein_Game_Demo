using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowTarget : MonoBehaviour
{
    private Transform target;
    private Vector3 my_position;

    void Start()
    {
        my_position = transform.position;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 position = transform.position;
            position.x = target.position.x;
            position.x = Mathf.Clamp(position.x, 0, 20);
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * 5);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, my_position, Time.deltaTime * 5);
        }
    }

    public void SetTarget(Transform transform)
    {
        this.target = transform;
    }
}
