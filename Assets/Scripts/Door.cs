using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject bringsTo;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(Tags.PLAYER))
        {
            Debug.Log("Door player");
            Vector2 goToPos = bringsTo.transform.TransformPoint(Vector3.zero);
            Debug.Log(goToPos);

            float offsetDir = transform.position.x - bringsTo.transform.position.x > 0 ? 1 : -1;
            float offsetX = offsetDir * 0.5f;
            // Offset half size of the goto object and half size of the player
            // To prevent collision
            offsetX += offsetDir * (bringsTo.transform.GetComponent<Collider2D>().bounds.size.x / 2 +
                collider.bounds.size.x / 2);
            goToPos.x += offsetX;
            collider.GetComponent<PlayerMovement>().SetPos(goToPos);
        }
    }
}
