using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{   
    private PlayerInput playerInput; //pull player input file
    private PlayerInput.MoveActions move;

    private PlayerMotor motor;

    void Awake()
    {
        playerInput = new PlayerInput();
        move = playerInput.Move;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movemnt action.
        motor.ProcessMove(move.Move.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }
    
}
