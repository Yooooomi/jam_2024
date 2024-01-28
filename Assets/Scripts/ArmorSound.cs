using UnityEngine;

public class ArmorSound : MonoBehaviour
{
    private Armor armor;

    private void Start() {
        armor = GetComponent<Armor>();
        armor.onTrapPlayer.AddListener(GetComponent<RandomSound>().PlayRandom);
    }
}
