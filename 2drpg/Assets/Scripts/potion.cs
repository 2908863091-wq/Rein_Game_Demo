using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LunaController controller = collision.GetComponent<LunaController>();
        if (controller.Ima_health < controller.Max_health)
        {
            controller.ChangeHealth(1);
            Destroy(gameObject);
        }

    }
}
