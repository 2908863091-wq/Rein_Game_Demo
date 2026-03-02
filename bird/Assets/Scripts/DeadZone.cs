using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bird bird = collision.GetComponent<Bird>();
        if (bird != null)
        {
            bird.Fade();
        }

    }
}
