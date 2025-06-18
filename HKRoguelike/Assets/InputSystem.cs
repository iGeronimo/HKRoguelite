using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem instance
    {
        get; private set;
    }

    void OnEnable()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private const KeyCode LEFT = KeyCode.LeftArrow;
    private const KeyCode RIGHT = KeyCode.RightArrow;
}