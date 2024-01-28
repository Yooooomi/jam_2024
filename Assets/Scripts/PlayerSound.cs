using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private Picker picker;


    private void Start() {
        picker = GetComponent<Picker>();
        picker.onThrow.AddListener(GetComponent<RandomSound>().PlayRandom);
    }
}
