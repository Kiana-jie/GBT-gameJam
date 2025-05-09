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
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        inputControl = new PlayerInputControl();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
    }
    public void OnEnable()
    {
        inputControl.Enable();
    }

    public void OnDisable()
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
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMove();
        
    }

    public void HandleJump()
    {
        bool status = inputControl.GamePlay2.Jump.triggered;
        if (status)
        {
            Debug.Log("��Ծ�����1");
            FusionController.Instance.StartFusion(gameObject, GameObject.Find("Player1"));

        }
    }

    public void HandleMove()//˫�ˣ�ʹ��inputSystem
    {

        if (InputDirection.x != 0 || InputDirection.y != 0 ) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);


        rb.velocity = new Vector2(InputDirection.x * status.speed * Time.deltaTime, InputDirection.y * status.speed * Time.deltaTime);


        float originalScale = Mathf.Abs(transform.localScale.x); // ��ȡԭʼ���Ŵ�С(����ֵ)
        int faceDir = InputDirection.x > 0 ? 1 : (InputDirection.x < 0 ? -1 : (int)Mathf.Sign(transform.localScale.x));

        gameObject.transform.localScale = new Vector3(originalScale * faceDir, transform.localScale.y, transform.localScale.z);
    }
}
