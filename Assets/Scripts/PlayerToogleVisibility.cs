using UnityEngine;

public class PlayerToogleVisibility : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public void SetPlayerVisibility(bool visibility)
    {
        spriteRenderer.enabled = visibility;
    }
}
