using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player != null)
        {
            player.speed -= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (player != null)
        {
            player.speed = player.GetMaxSpeed();
        }
    }
}
