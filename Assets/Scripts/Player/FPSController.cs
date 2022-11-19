using System;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LostOnTenebris
{
    public class FPSController : MonoBehaviour
    {
        // Editor parameters
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 6.0f;
        [SerializeField] private float runSpeed = 12.0f;
        [SerializeField] private float jumpVelocity = 12.0f;
        [SerializeField] private float maxStamina = 1f;
        [SerializeField] private float staminaRunDecrease = 0.2f;
        [SerializeField] private float staminaIdleIncrease = 0.3f;
        
        [Header("Ground detection")]
        [SerializeField] private LayerMask whatIsGround;
        [SerializeField] private Vector3 groundCheckCenter;
        [SerializeField] private float groundCheckDistance;
        
        [Header("Camera")]
        [SerializeField] private float lookSensitivity = 0.6f;
        [SerializeField] private bool invertY = false;
        [SerializeField] private float maxAngleY = 80.0f;

        [Header("Components")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform camera;
        [SerializeField] private PlayerInput playerInput;

        [Header("Sound")]
        [SerializeField] private EventReference outOfBreath;
        
        public Vector3 LookDirection => camera.forward;
        public Vector3 Position => camera.position;
        public bool Running => running;
        public bool Moving => moveDirection != Vector3.zero;
        public bool Grounded => characterController.isGrounded;
        
        // Player state management
        private bool onGround;
        private bool onSlope;
        private bool crouching;
        private bool running;
        private float stamina;
        
        private Vector3 lookRotation;
        private Vector3 lookDirectionGamepad;
        private Vector3 moveDirection;

        private Vector3 groundNormal;

        private Vector3 MoveDirection => AdjustMoveDirectionToCamera(moveDirection);
        
        private bool pause = false;

        private void Start()
        {
            this.stamina = maxStamina;
            this.lookRotation = transform.eulerAngles;
        }

        public void Pause(bool state)
        {
            pause = state;
            this.moveDirection = Vector3.zero;
        }
        
        // MonoBehaviour messages
        private void Update()
        {
            if (pause) return;
            LookGamepad();
            Look();
        }

        private void FixedUpdate()
        {
            if (pause) return;

            if (running && stamina <= 0.0f)
            {
                RuntimeManager.PlayOneShot(outOfBreath);
                running = false;
            }
            
            this.stamina = running ? Mathf.Clamp(stamina - Time.deltaTime * staminaRunDecrease, 0, maxStamina)
                : Mathf.Clamp(stamina + Time.deltaTime * staminaIdleIncrease, 0, maxStamina);
            
            Move();
        }

        private void OnDrawGizmos()
        {
            if (characterController is null) return;
            // Move direction
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + groundCheckCenter, characterController.radius + groundCheckDistance);
        }

        // Controller functions
        private void Look()
        {
            camera.rotation = Quaternion.Euler(lookRotation);
        }

        private void LookGamepad()
        {
            if (lookDirectionGamepad == Vector3.zero) return;
            lookRotation += lookDirectionGamepad;
            lookRotation.x = ClampEulerAngle(lookRotation.x, -maxAngleY, maxAngleY);
            lookRotation.y = ClampEulerAngle(lookRotation.y);
        }

        private void Move()
        {
            Vector3 direction = AdjustMoveDirectionToCamera(moveDirection);
            characterController.SimpleMove(direction * (running ? runSpeed : walkSpeed));
        }
        
        // Utility functions
        private void CheckGround()
        {
            Vector3 worldCenter = transform.position + groundCheckCenter;
            float radius = characterController.radius + groundCheckDistance;

            RaycastHit[] results = new RaycastHit[1];
            Physics.SphereCastNonAlloc(worldCenter, radius, Vector3.down, results, groundCheckDistance, whatIsGround);
        }
        
        private Vector3 AdjustMoveDirectionToCamera(Vector3 direction)
        {
            direction = camera.rotation * direction;
            direction = new Vector3(direction.x, 0.0f, direction.z).normalized;
            return direction;
        }
        
        private float ClampEulerAngle(float angle, float min = -360.0f, float max = 360.0f)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;

            return Mathf.Clamp(angle, min, max);
        }
        
        // Input Handling
        private void OnMove(InputValue value)
        {
            if (pause) return;
            
            Vector2 direction = value.Get<Vector2>();
            Vector3 localMoveDirection = new Vector3(direction.x, 0.0f, direction.y);
            moveDirection = localMoveDirection;
        }

        private void OnLook(InputValue value)
        {
            if (pause) return;

            Vector2 deltaLook = value.Get<Vector2>();

            Vector3 deltaRotation = new Vector3(deltaLook.y, deltaLook.x, 0.0f);
            deltaRotation *= lookSensitivity;
            deltaRotation.x *= invertY ? 1 : -1;

            lookRotation += deltaRotation;
            lookRotation.x = ClampEulerAngle(lookRotation.x, -maxAngleY, maxAngleY);
            lookRotation.y = ClampEulerAngle(lookRotation.y);
        }

        private void OnLookGamepad(InputValue value)
        {
            if (pause) return;

            Vector2 deltaLook = value.Get<Vector2>();

            lookDirectionGamepad = new Vector3(deltaLook.y, deltaLook.x, 0.0f);
            lookDirectionGamepad *= lookSensitivity;
            lookDirectionGamepad.x *= invertY ? 1 : -1;
        }
        
        private void OnRun(InputValue value)
        {
            if (pause) return;
            
            float runningValue = value.Get<float>();
            this.running = runningValue > 0.0f;
        }

        private void OnCrouch(InputValue value)
        {
            float crouchValue = value.Get<float>();
            this.crouching = crouchValue > 0.0f;
        }

        private void OnPause()
        {
           Pause(true); 
        }

        private void OnUnpause()
        {
            Pause(false);
        }
    }
}
