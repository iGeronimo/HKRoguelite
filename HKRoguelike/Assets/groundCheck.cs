using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    private PlayerMovement parentPlayerMovement;
    void Start()
    {
        parentPlayerMovement = transform.parent.GetComponent<PlayerMovement>();
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
