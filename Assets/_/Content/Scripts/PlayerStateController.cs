using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [System.Serializable]
    public enum PlayerState
    {
        Idle,
        isRunning,
        isRunningSlide,
        isJumping
    }

    [SerializeField]
    private PlayerState currentState = PlayerState.Idle;
    private Animator animator;

    public PlayerState CurrentState
    {
        get { return currentState; }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeState(PlayerState newState)
    {

        if (currentState == newState) return;

        currentState = newState;

        switch (currentState)
        {
            case PlayerState.Idle:
                animator.SetBool("Idle", true);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningSlide", false);
                animator.SetBool("isJumping", false);
                break;
            case PlayerState.isRunning:
                animator.SetBool("Idle", false);
                animator.SetBool("isRunning", true);
                animator.SetBool("isRunningSlide", false);
                animator.SetBool("isJumping", false);
                break;
            case PlayerState.isRunningSlide:
                animator.SetBool("Idle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningSlide", true);
                animator.SetBool("isJumping", false);
                break;
            case PlayerState.isJumping:
                animator.SetBool("Idle", false);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningSlide", false);
                animator.SetBool("isJumping", true);
                break;
        }
    }
}
