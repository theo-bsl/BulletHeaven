using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerRotation playerRotation;
    private PlayerAttack PlayerAttack;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerRotation = GetComponent<PlayerRotation>();
        PlayerAttack = GetComponent<PlayerAttack>();

        playerInput = GetComponent<PlayerInput>();

        #if !UNITY_EDITOR
        UpdateBinding(playerInput.currentActionMap, "Pause", "<Keyboard>/escape");
        #endif
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnRotateKeyboard(InputAction.CallbackContext context)
    {
        playerRotation.SetRotation(context.ReadValue<Vector2>().x);
    }

    public void OnRotateGamepad(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector2>() != Vector2.zero)
            playerRotation.RotateGamepad(context.ReadValue<Vector2>());
    }

    public void OnSwitchAttackMode(InputAction.CallbackContext context)
    {
        if (context.started)
            PlayerAttack.SwitchAttackMode();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        PlayerAttack.SetIsAttacking(context.ReadValueAsButton());
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            GameManager.Instance.PausesGame();
    }

    private void UpdateBinding(InputActionMap actionMap, string actionName, string overridePath)
    {
        InputBinding newBinding = new InputBinding();
        int index = 0;
        foreach (InputBinding inputBinding in actionMap.bindings)
        {
            if (inputBinding.action.Equals(actionName))
            {
                newBinding = inputBinding;
                newBinding.overridePath = overridePath;
                break;
            }

            index++;
        }
        actionMap.ApplyBindingOverride(index, newBinding);
    }

}
