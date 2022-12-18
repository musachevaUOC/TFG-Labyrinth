using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class movement : MonoBehaviour
{

    private Player player;

    public Transform cam; // First person camera of the player
    public GameObject projectile;

    private float vel; // velocity of the character
    private float LookSensitivity;

    private CharacterController characterController;

    private Vector3 movement_direction;
    private Vector2 look_rotation_controller;

    private float vertAngle;

    private bool action_move_active;
    private bool action_look_active;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        characterController = gameObject.GetComponent<CharacterController>();

        vel = player.speed; 
        LookSensitivity = player.lookSensitivity;

        action_move_active = false;
        action_look_active = false;

        movement_direction = Vector3.zero;
        look_rotation_controller = Vector2.zero;

        vertAngle = 0f;

        gameObject.GetComponent<PlayerInput>().enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (action_move_active)
        {
            characterController.SimpleMove( movement_direction * vel);
        }
        if (action_look_active)
        {
            vertAngle = Mathf.Clamp(vertAngle - look_rotation_controller.y * Time.deltaTime * (LookSensitivity/2), -90f, 90f);
            cam.localRotation = Quaternion.Euler(vertAngle, 0,0);
            transform.Rotate(0 , look_rotation_controller.x * Time.deltaTime * LookSensitivity , 0);
        }

    }

    public void move(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Canceled)
        {
            Vector2 input = context.action.ReadValue<Vector2>();
            movement_direction = transform.forward * input.y + transform.right * input.x;
            action_move_active = true;

        }
        else{
            movement_direction = Vector3.zero;
            action_move_active = false;
        }
        
    }

    public void look(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Canceled)
        {
            look_rotation_controller = context.action.ReadValue<Vector2>();
            action_look_active = true;

        }
        else
        {
            look_rotation_controller = Vector2.zero;
            action_look_active = false;
        }
    }

    public void fire(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {

            Instantiate(projectile, transform.position + transform.forward * 0.8f, transform.rotation).SetActive(true);
            
        }
        
    }

    public Vector3 getMovementVel()
    {
        return this.movement_direction*vel;
    }
}
