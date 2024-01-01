using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private InputManager _input;
    
    [SerializeReference] public new Camera camera;
    [SerializeField, Range(0, 50)] private float cameraFollowSpeed;
    [SerializeField] public Vector3 cameraTargetOffset;
    
    [SerializeField, Range(0, 15)] private float movementSpeed;
    [SerializeField, Range(0, 10)] private float movementRampSpeed;
    
    [SerializeField] private ContactFilter2D groundFilter;
    [SerializeField, Range(0.1f, 1f)] public float groundCheckDistance = 0.1f;
    [SerializeField, Range(1, 10)] public int jumpForce = 1;
    
    public bool IsGrounded { get; private set; }
    private Vector2 _movementVector = new Vector2(0, 0);
    
    private void Awake()
    {
        _input = GetComponent<InputManager>();
        _rigidBody = GetComponent<Rigidbody2D>();;
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void FixedUpdate()
    {  
        
        // Handle Movement vector smoothing
        _movementVector = Vector2.Lerp(_movementVector, _input.MovementVector, movementRampSpeed * Time.fixedDeltaTime);
        
        GroundCheck();
        CameraFollow(Time.fixedDeltaTime, _rigidBody.transform.position);
        Movement(Time.fixedDeltaTime, _movementVector);
    }

    private void GroundCheck()
    {
        var raycastHits = new RaycastHit2D[1];
        var cast = _rigidBody.Cast(Vector2.down, groundFilter, raycastHits, groundCheckDistance);
        IsGrounded = cast == 1;
    }
    
    /// <summary>
    /// Should run in fixed update, updates the velocity of the player physics location.
    /// </summary>
    /// <param name="deltaTime">The time since the last update.</param>
    /// <param name="movementVector">Represents he direction in which to move</param>
    private void Movement(float deltaTime, Vector2 movementVector)
    {   
        // Handle X movement 
        _rigidBody.velocity = new Vector2(movementVector.x * (25 * movementSpeed * deltaTime), _rigidBody.velocity.y);
        
        if (_input.JumpDown && IsGrounded)
            _rigidBody.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Should run on fixed update if following a physics object, used to update the camera's position to center on the target.
    /// </summary>
    /// <param name="deltaTime">The amount of time since the last update.</param>
    /// <param name="target">The position of the target for the camera to follow. </param>
    private void CameraFollow(float deltaTime, Vector3 target)
    {
        
        var targetTransformPosition = target + cameraTargetOffset;
        var currentTransformPosition = camera.transform.position;
        var targetLocation = new Vector3(targetTransformPosition.x, targetTransformPosition.y, currentTransformPosition.z);
        
        camera.transform.position = Vector3.Lerp(currentTransformPosition, targetLocation, cameraFollowSpeed*Time.deltaTime);
        
    }
    
}
