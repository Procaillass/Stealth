using UnityEngine;
using Cinemachine;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;

    public CinemachineFreeLook freeLookCam;
    private CharacterController characterController;
    private PlayerStateController playerStateController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerStateController = GetComponent<PlayerStateController>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");

        Vector3 cameraRight = freeLookCam.State.RawOrientation * Vector3.right;
        Vector3 flattenedCameraRight = Vector3.ProjectOnPlane(cameraRight, Vector3.up);
        Vector3 cameraForward = freeLookCam.State.RawOrientation * Vector3.forward;
        Vector3 flattenedCameraForward = Vector3.ProjectOnPlane(cameraForward, Vector3.up);

        Vector3 move = flattenedCameraRight * deltaX + flattenedCameraForward * deltaZ;

        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * speed);
        }

        characterController.SimpleMove(move * speed);


        // Update player state
        if (Input.GetButtonDown("Fire1"))
        {
            playerStateController.ChangeState(PlayerStateController.PlayerState.isRunningSlide);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            playerStateController.ChangeState(PlayerStateController.PlayerState.isJumping);
        }
        else { 
        
        if (move.magnitude > 0)        
            {            
                playerStateController.ChangeState(PlayerStateController.PlayerState.isRunning);
            }
            else
            {
                playerStateController.ChangeState(PlayerStateController.PlayerState.Idle);
            }
        
        } 
        }


}
