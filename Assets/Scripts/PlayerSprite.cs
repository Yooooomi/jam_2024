using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private Juggle juggle;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update() {
        animator.SetBool("juggling", juggle.isJuggling);

        float rotation = movement.lookingDirection.eulerAngles.z;

        int side = (int) (rotation / 90.0f);

        int currentAnimationDirection = animator.GetInteger("direction");
        if (side != currentAnimationDirection) {
            animator.SetInteger("direction", side);
        }
    }
}
