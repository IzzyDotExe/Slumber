using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    public InputManager _input;
    
    [SerializeReference] public new Camera camera;
    [SerializeField, Range(0, 50)] private float cameraFollowSpeed;
    [SerializeField] public Vector3 cameraTargetOffset;
    
    private void Awake()
    {
        _input = GetComponent<InputManager>();
        _input.JumpEvent += () =>
        {
            Debug.Log("Jump");
        };
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {  
        CameraFollow(Time.fixedDeltaTime, _rigidBody.transform.position);
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
