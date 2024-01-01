
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public Vector2 MovementVector { get; private set; } = new Vector2(0, 0);
    public bool JumpDown { get; private set; } = false;
    public void OnMoveX(InputAction.CallbackContext ctx) =>
        MovementVector = new Vector2(ctx.ReadValue<float>(), MovementVector.y);

    public void OnMoveY(InputAction.CallbackContext ctx) =>
        MovementVector = new Vector2(MovementVector.x, ctx.ReadValue<float>());

    public void OnJump(InputAction.CallbackContext ctx)
    {
        
        var buttonVal = ctx.ReadValue<float>();
        JumpDown = Math.Abs(buttonVal - 1.0f) < 1;
    }
}
