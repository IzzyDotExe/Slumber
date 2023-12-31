using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;
    private InputActions _inputActions;
    
    private void Awake()
    {
        
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _inputActions = new InputActions();
        
    }
}
