using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftWallCheck : MonoBehaviour
{
    private PlayerMovement parentPlayerMovement;
    void Start()
    {
        parentPlayerMovement = transform.parent.GetComponent<PlayerMovement>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            parentPlayerMovement.SetWallLeft(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Wall")
        {
            parentPlayerMovement.SetWallLeft(false);
        }
    }
}
