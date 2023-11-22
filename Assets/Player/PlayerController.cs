using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRotation = GetComponent<PlayerRotation>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            playerRotation.Rotate(context.ReadValue<Vector2>().x);
        }*/
        playerRotation.SetRotation(context.ReadValue<Vector2>().x);
    }
}
