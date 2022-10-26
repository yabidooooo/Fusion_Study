using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CharacterMovementHandler : NetworkBehaviour
{
    // Other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    Camera localCamera;

    void Awake()
    {
        networkCharacterControllerPrototypeCustom = GetComponent<NetworkCharacterControllerPrototypeCustom>();
        localCamera = GetComponentInChildren<Camera>();
    }

    void Start()
    {
        
    }

    void Update()
    {

    }

    public override void FixedUpdateNetwork()
    {
        // Get the input from the network
        if (GetInput(out NetworkInputData networkInputData))
        {
            // Rotate the transform according to the client aim vector
            transform.forward = networkInputData.aimForwardVector;

            // Cancel out rotation on X axis as we don't want our character to tilt
            Quaternion rotation = transform.rotation;
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z);
            transform.rotation = rotation;

            // Move
            Vector3 moveDirection = transform.forward * networkInputData.movementInput.y + transform.right * networkInputData.movementInput.x;
            moveDirection.Normalize();

            networkCharacterControllerPrototypeCustom.Move(moveDirection);

            // Jump
            if (networkInputData.isJumpPressed)
            {
                networkCharacterControllerPrototypeCustom.Jump();
            }

            // Check if we've fallen off the world.
            CheckFallRespawn();
        }
    }

    void CheckFallRespawn()
    {
        if (transform.position.y < -12)
        {
            transform.position = Utils.GetRandomSpawnPoint();
        }
    }
}
