using UnityEngine;

public class CakeSound : MonoBehaviour
{
    private Cake cake;

    private void Start() {
        cake = GetComponent<Cake>();
        cake.onHit.AddListener(GetComponent<RandomSound>().PlayRandom);
    }
}
