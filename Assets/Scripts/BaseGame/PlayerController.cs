using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IPlayerController
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    Animator _animator;

    public enum playerName
    {
        Blue, Red, Green, Purple
    }

    public playerName thisPlayersName = playerName.Blue;

    private Vector3 move;
    public int score { get; set; }

    public event System.Action<int> OnScoreChanged;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Start()
    {
        if (OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
        _animator = GetComponent<Animator>();
    }
    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }


        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        /*
         * This is using the old input system, refactor for the new one
         * 
        if (Input.GetButtonDown("Fire1"))//left click, South on gamepad
        {
            //slap opponents
        }
        if (Input.GetButtonDown("Fire2"))//Right click, East on gamepad
        {
            //dash with cooldown
        }
        if (Input.GetButtonDown("Fire3"))//middle click, west on gamepad
        {
        }
        */
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        _animator.SetBool("IsGrounded", groundedPlayer);
        _animator.SetFloat("Speed", move.normalized.magnitude);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -gravityValue);
        }
    }


    public void OnSlap(InputAction.CallbackContext context)
    {
        Debug.Log("Slappity slap");
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        Debug.Log("Dashing...");
    }

    public void IncreaseScore(int value)
    {
        score += value;
        if (OnScoreChanged != null)
        {
            OnScoreChanged(score);
        }
        //Debug.Log($"Player score is {score}");
    }
    
}
