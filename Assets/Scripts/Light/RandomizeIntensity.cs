using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RandomizeIntensity : MonoBehaviour
{
    [SerializeField]
    private Vector2 intensityInterval;
    [SerializeField]
    private Vector2 randomizeEveryInterval;

    private Light2D lightToRandomize;

    private float nextRandomizeTime = -1f;

    private void Start() {
        lightToRandomize = GetComponent<Light2D>();
    }

    private void Update() {
        if (nextRandomizeTime > Time.time) {
            return;
        }
        lightToRandomize.intensity = Random.Range(intensityInterval.x, intensityInterval.y);
        nextRandomizeTime = Time.time + Random.Range(randomizeEveryInterval.x, randomizeEveryInterval.y);
    }
}
