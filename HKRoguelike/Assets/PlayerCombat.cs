using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject attackLeft;
    public GameObject attackRight;
    public GameObject attackUp;
    public GameObject attackDown;

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        attackInput();
    }

    void attackInput()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (Input.GetKeyDown(KeyCode.W)) swing(Attacks.attackUp);
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (pm.isGrounded()) swing(Attacks.attackDown);
            }
            else if (pm.facingRight) swing(Attacks.attackRight);
            else if (!pm.facingRight) swing(Attacks.attackLeft);
        }
    }
    void swing(Attacks attack)
    {
        switch (attack)
        {
            case Attacks.attackLeft:
                break;
            case Attacks.attackRight:
                break;
            case Attacks.attackUp:
                break;
            case Attacks.attackDown:
                break;
        }
    }

    void applyPushBack(Attacks attack)
    {
        switch (attack)
        {
            case Attacks.attackLeft:
                pm._velocity = new Vector2(2, pm._velocity.y);
                break;

            case Attacks.attackRight:
                pm._velocity = new Vector2(-2, pm._velocity.y);
                break;

            case Attacks.attackUp:
                pm._velocity = new Vector2(pm._velocity.x, 0);
                break;

            case Attacks.attackDown:
                pm._velocity = new Vector2(pm._velocity.x, 10);
                break;
        }
    }
}
enum Attacks
{
    attackLeft,
    attackRight,
    attackUp,
    attackDown
}
