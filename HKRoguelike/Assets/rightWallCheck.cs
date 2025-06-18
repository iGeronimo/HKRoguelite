using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightWallCheck : MonoBehaviour
{
    public PlayerMovement parentPlayerMovement;
    void Start()
    {
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            parentPlayerMovement.SetWallRight(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            parentPlayerMovement.SetWallRight(false);
        }
    }
}
