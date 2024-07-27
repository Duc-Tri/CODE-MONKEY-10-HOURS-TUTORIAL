using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private Animator animator;
    private Player player;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking);
    }

}
