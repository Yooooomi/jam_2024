using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement movement;
    [SerializeField]
    private Juggle juggle;
    [SerializeField]
    private PlayerPickable playerPickable;
    [SerializeField]
    private PlayerAnimation playerAnimation;

    private void Update() {
        float rotation = movement.lookingDirection.eulerAngles.z;
        int side = (int) ((rotation + 45f) / 90.0f);

        bool inAir = playerPickable.IsBeingPicked() || playerPickable.IsBeingThrown();

        string strSide;

        if (side == 0) {
            strSide = "right";
        } else if (side == 1) {
            strSide = "up";
        } else if (side == 2) {
            strSide = "left";
        } else {
            strSide = "down";
        }

        if (inAir) {
            playerAnimation.SetAnimationName("idle_down");
            return;
        }
        if (juggle.isJuggling) {
            playerAnimation.SetAnimationName("juggle");
            return;
        }
        
        if (movement.currentSpeed == 0) {
            strSide = $"idle_{strSide}";
        }

        playerAnimation.SetAnimationName(strSide);
    }
}
