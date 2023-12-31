
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public Vector2 MovementVector { get;} = new Vector2(0, 0);
    public Action JumpEvent { get; set; }

    public void OnMoveX(InputAction.CallbackContext ctx) =>
        MovementVector.Set(ctx.ReadValue<float>(), MovementVector.y);

    public void OnMoveY(InputAction.CallbackContext ctx) =>
        MovementVector.Set(MovementVector.x, ctx.ReadValue<float>());

    public void OnJump(InputAction.CallbackContext ctx)
        => JumpEvent.Invoke();
}
