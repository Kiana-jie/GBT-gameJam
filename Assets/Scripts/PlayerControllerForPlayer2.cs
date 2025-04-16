using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerForPlayer2 : MonoBehaviour
{
    public PlayerInputControl inputControl;
    private Rigidbody2D rb;
    private Vector2 InputDirection;
    private PlayerStatus status;
    
    // Start is called before the first frame update
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
    }
    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputDirection = inputControl.GamePlay2.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    public void HandleMove()//双人：使用inputSystem
    {
        rb.velocity = new Vector2(InputDirection.x * status.speed * Time.deltaTime, InputDirection.y * status.speed * Time.deltaTime);

        //翻转
        int faceDir = (int)transform.localScale.x;
        if (InputDirection.x > 0) faceDir = 1;
        if (InputDirection.x < 0) faceDir = -1;

        gameObject.transform.localScale = new Vector3(faceDir, 1, 1);
    }
}
