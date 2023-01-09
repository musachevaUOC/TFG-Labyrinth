using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    private Player player;

    public Transform cam; // First person camera of the player
    public GameObject projectile;
    public float enemyLockRayDist = 20f;

    private float LookSensitivity;

    private CharacterController characterController;

    private Vector3 movement_direction;
    private Vector2 look_rotation_controller;

    private float vertAngle;

    private bool action_move_active;
    private bool action_look_active;
    private bool locked_on_enemy;

    private Transform enemyPos;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.inst;
        characterController = gameObject.GetComponent<CharacterController>();

        LookSensitivity = player.lookSensitivity;

        action_move_active = false;
        action_look_active = false;
        locked_on_enemy = false;

        movement_direction = Vector3.zero;
        look_rotation_controller = Vector2.zero;

        enemyPos = null;

        vertAngle = 0f;

        gameObject.GetComponent<PlayerInput>().enabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (action_move_active)
        {
            characterController.SimpleMove((transform.forward * movement_direction.y + transform.right * movement_direction.x) * player.speed);
        }
        if (action_look_active)
        {
            vertAngle = Mathf.Clamp(vertAngle - look_rotation_controller.y * Time.deltaTime * (LookSensitivity/2), -30f, 20f);
            cam.localRotation = Quaternion.Euler(vertAngle, 0,0);
            transform.Rotate(0 , look_rotation_controller.x * Time.deltaTime * LookSensitivity , 0);
        }else if (locked_on_enemy && enemyPos != null)
        {
            vertAngle = 0;
            transform.LookAt(new Vector3(enemyPos.position.x, transform.position.y, enemyPos.position.z));
        }
    }

    public void move(InputAction.CallbackContext context)
    {
        if(context.phase != InputActionPhase.Canceled)
        {
            movement_direction = context.action.ReadValue<Vector2>();
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
            locked_on_enemy = false;
            enemyPos = null;
        }
        else
        {
            look_rotation_controller = Vector2.zero;
            action_look_active = false;
            locked_on_enemy = lockedOnEnemy();
        }
    }

    public bool lockedOnEnemy()
    {
        RaycastHit rch;
        if(Physics.SphereCast(transform.position, 7f ,transform.forward,out rch ,enemyLockRayDist,  
            LayerMask.GetMask("hitable"), QueryTriggerInteraction.Collide))
        {
            if(rch.transform.gameObject.tag == "Enemy")
            {
                enemyPos = rch.transform;
                return true;
            }
        }
        return false;
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
        return this.movement_direction* player.speed;
    }
}
