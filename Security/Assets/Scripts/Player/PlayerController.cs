using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.PlasticSCM.Editor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SearchService;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// �÷��̾� ���� ����
    /// </summary>
    enum PlayerState
    {
        Idle = 0,
        Walk,
        Sprint,
        Jump,
        Crouch
    }

    /// <summary>
    /// ���� �÷��̾� ����
    /// </summary>
    PlayerState state = PlayerState.Idle;

    PlayerState State
    {
        get => state;
        set
        {
            if(state != value)
            {
                state = value;
                switch(state)
                {
                    case PlayerState.Idle:

                        break;
                    case PlayerState.Walk:

                        break;
                    case PlayerState.Sprint:

                        break;
                    case PlayerState.Jump:

                        break;
                    case PlayerState.Crouch:

                        break;
                }
            }
        }
    }

    PlayerInputAction InputAction;
    public PlayerInputAction playerInputAction => InputAction;
    CharacterController controller;
    public CharacterController controllerController => controller;
    CinemachineVirtualCamera cinemachine;
    public CinemachineVirtualCamera Cinemachine => cinemachine;

    /// <summary>
    /// ī�޶� ��ġ
    /// </summary>
    public Transform cameraRoot;

    /// <summary> 
    /// ��ġ����
    /// </summary>
    Vector3 moveDir = Vector3.zero;

    /// <summary>
    /// ���� �̵��ӵ�
    /// </summary>
    float currentSpeed = 0.0f;

    /// <summary>
    /// �ȱ� �ӵ�
    /// </summary>
    float walkingSpeed = 3.0f;

    /// <summary>
    /// �޸��� �ӵ�
    /// </summary>
    float sprintingSpeed = 4.7f;

    /// <summary>
    /// �޸��� üũ
    /// </summary>
    bool sprintChecking = false;

    /// <summary>
    /// ���� ����
    /// </summary>
    float jumpHeight = 4.0f;

    /// <summary>
    /// ���� üũ
    /// </summary>
    bool jumpChecking = false;

    /// <summary>
    /// ���� ���� Ȯ��
    /// </summary>
    float jumpCheckHeight = 0.0f;

    /// <summary>
    /// �߷� ũ��
    /// </summary>
    float gravity = 9.8f;

    /// <summary>
    /// ���� ī��Ʈ(1������ ����)
    /// </summary>
    int jumpCount = 0;

    /// <summary>
    /// ��ũ���� ���ҷ�
    /// </summary>
    float crouchDecrease = 1.0f;

    /// <summary>
    /// ��ũ���� üũ
    /// </summary>
    bool crouchChecking = false;

    /// <summary>
    /// x���� ��ȯ �ΰ���
    /// </summary>
    float rotateSensitiveX = 7.5f;

    /// <summary>
    /// y���� ��ȯ �ΰ���
    /// </summary>
    float rotateSensitiveY = 10.0f;

    /// <summary>
    /// ���� �̵��� y���� ��ȯ
    /// </summary>
    float curRotateY = 0.0f;

    float Temp = 0.0f;
    Vector3 boxsize = new Vector3(0.25f, 0.125f, 0.25f);
    Vector3 groundCheckPostion;

    public Action onSprinting;
    public Action offSprinting;

    private void Start()
    {
        currentSpeed = walkingSpeed;
        Temp = currentSpeed;
    }

    private void Awake()
    {
        InputAction = new PlayerInputAction();

        controller = GetComponent<CharacterController>();
        cameraRoot = transform.GetChild(0);
        cinemachine = GetComponentInChildren<CinemachineVirtualCamera>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        InputAction.Player.Enable();
        InputAction.Player.Move.performed += OnMove;
        InputAction.Player.Move.canceled += OnMove;
        InputAction.Player.Sprint.performed += OnSprint;
        InputAction.Player.Sprint.canceled += OnSprint;
        InputAction.Player.Jump.performed += OnJump;
        InputAction.Player.Crouch.performed += OnCrouch;
        InputAction.Player.Crouch.canceled += OnCrouch;
        InputAction.Mouse.Enable();
        InputAction.Mouse.MouseVector2.performed += OnMouseDelta;
    }

    private void OnDisable()
    {
        InputAction.Mouse.MouseVector2.performed -= OnMouseDelta;
        InputAction.Mouse.Disable();
        InputAction.Player.Crouch.canceled -= OnCrouch;
        InputAction.Player.Crouch.performed -= OnCrouch;
        InputAction.Player.Jump.performed -= OnJump;
        InputAction.Player.Sprint.canceled -= OnSprint;
        InputAction.Player.Sprint.performed -= OnSprint;
        InputAction.Player.Move.canceled -= OnMove;
        InputAction.Player.Move.performed -= OnMove;
        InputAction.Player.Disable();
    }

    private void Update()
    {
        if (!IsGrounded())
        {
            moveDir.y -= gravity * Time.deltaTime;
        }
        //�÷��̾� x, z��ǥ �̵�
        controller.Move(Time.deltaTime * currentSpeed * transform.TransformDirection(new Vector3(moveDir.x, 0.0f, moveDir.z)));
        //�÷��̾� y��ǥ �̵�
        controller.Move(Time.deltaTime * new Vector3(0.0f, moveDir.y, 0.0f));
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();

        //�Է¹��� W, A, S, D(Vector2)��ǥ�� x, z��ǥ�� ����
        moveDir.x = dir.x; moveDir.z = dir.y;
    }

    private void OnSprint(InputAction.CallbackContext context)
    {
        //��ũ���� ���°� �ƴ� ��
        if (!crouchChecking)
        {
            if (context.performed)
            {
                //���� �̵��ӵ� = �޸��� �ӵ�
                currentSpeed = sprintingSpeed * crouchDecrease;
                onSprinting?.Invoke();
                sprintChecking = true;
            }
            else
            {
                //������ �ڷ�ƾ ����
                EndSequence();
            }
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        //��ũ���� ���°� �ƴ� ��
        if (!crouchChecking)
        {
            if(jumpCount <1)
            {
                //y�̵� ���� ���� ���̷� �Ҵ�
                moveDir.y = jumpHeight;

                if(jumpCount == 0)
                {
                    //��ǥ ������ ���� ����
                    jumpCheckHeight = transform.position.y + controller.radius * 0.3f;
                }
                //���� ���� = true
                jumpChecking = true;
                jumpCount++;
            }
        }
    }

    private void OnCrouch(InputAction.CallbackContext context)
    {
        if (!sprintChecking&&IsGrounded())
        {
            if(context.performed)
            {
                //���ҷ� 0.5��
                crouchDecrease = 0.5f;
                ////���� �ӵ� = �ӽú���(���� �ӵ�) * ���ҷ�(0.5)
                currentSpeed = Temp * crouchDecrease;
                crouchChecking = true;

                //���� ���߱� #��ũ���� O
                cameraRoot.transform.position += new Vector3(0f, -0.5f, 0f);
            }
            else
            {
                //���ҷ� X
                crouchDecrease = 1.0f;
                //���� �ӵ� = �ȱ� �ӵ�(���� �ӵ�) * ���ҷ�X
                currentSpeed = Temp * crouchDecrease;
                crouchChecking = false;

                //���� �ø��� #��ũ���� X
                cameraRoot.transform.position += new Vector3(0f, 0.5f, 0f);
            }
        }
    }

    private void OnMouseDelta(InputAction.CallbackContext context)
    {
        //�Է¹��� ���콺 ��ǥ�� ����
        Vector2 temp = context.ReadValue<Vector2>();
        //�Է¹��� ��ǥ�� x���� ��ȯ �ΰ��� ��ŭ õõ�� �̵�
        float rotateX = temp.x * rotateSensitiveX * Time.fixedDeltaTime;
        //transform�� ����
        transform.Rotate(Vector3.up, rotateX);

        //�Է¹��� ��ǥ�� y���� ��ȯ �ΰ��� ��ŭ õõġ �̵�
        float rotateY = temp.y * rotateSensitiveY * Time.fixedDeltaTime;
        //���� y���� ��ȯ �̵����� ���� �̵��� y���� ��ȯ�� ����
        curRotateY -= rotateY;
        //y���� ��ȯ�� �ּ� �� �ִ뷮�� ����
        curRotateY = Mathf.Clamp(curRotateY, -60.0f, 60.0f);
        //�̵��� ���⸸ŭ ī�޶� �̵�
        cameraRoot.rotation = Quaternion.Euler(curRotateY, cameraRoot.eulerAngles.y, cameraRoot.eulerAngles.z);
    }

    private bool IsGrounded()
    {
        //���� ���°� �ƴϰ�, ���� y���̰� ��ǥ y���� ���� ��
        if(jumpChecking && transform.position.y > jumpCheckHeight)
        {
            jumpChecking = false;
        }

        //ĳ���� ������ �ٴ��� üũ�ϴ� ���簢���� ����
        groundCheckPostion = new Vector3(transform.position.x, transform.position.y + controller.radius * -3.0f, transform.position.z);

        //���簢���� ���̾� "Ground"�� ���� ���
        if(Physics.CheckBox(groundCheckPostion, boxsize, Quaternion.identity, LayerMask.GetMask("Ground")))
        {
            if (!jumpChecking)
            {
                //���� ���̰� ���� �ִ� ���̺��� ���� ��
                if(moveDir.y < jumpHeight)
                {
                    //���� ���̸� -0.01f ����
                    moveDir.y = -0.01f;
                }
                jumpChecking = false;
                jumpCount = 0;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �޸��� ���� �ڷ�ƾ
    /// </summary>
    public void EndSequence()
    {
        //���� �ӵ� = �ȱ� �ӵ�
        currentSpeed = walkingSpeed * crouchDecrease;
        offSprinting?.Invoke();
        sprintChecking = false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(groundCheckPostion, boxsize);
    }
#endif
}
