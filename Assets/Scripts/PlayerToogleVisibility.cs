using System.Collections;
using System.Collections.Generic;
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
