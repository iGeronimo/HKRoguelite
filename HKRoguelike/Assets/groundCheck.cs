using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public PlayerMovement parentPlayerMovement;
    void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            parentPlayerMovement.SetGrounded(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            parentPlayerMovement.SetGrounded(false);
        }
    }
}
