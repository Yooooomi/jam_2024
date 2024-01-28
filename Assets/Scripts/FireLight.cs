using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLight : MonoBehaviour
{
    [SerializeField]
    private Light2D light2D;
    [SerializeField]
    private float minIntensity;
    [SerializeField]
    private float maxIntensity;
    
    private float startedIntensity;
    private float targetIntensity;
    private float startedAt;
    private float finishAt = -1f;

    [SerializeField]
    private float minTransitionTime;
    [SerializeField]
    private float maxTransitionTime;


    private void Update() {
        if (Time.time > finishAt) {
            startedAt = Time.time;
            finishAt = startedAt + minTransitionTime + Random.Range(0, maxTransitionTime - minTransitionTime);
            targetIntensity = minIntensity + Random.Range(0, maxIntensity - minIntensity);
            startedIntensity = light2D.intensity;
        }

        float duration = finishAt - startedAt;
        float ratio = (Time.time - startedAt) / duration;

        light2D.intensity = Mathf.Lerp(startedIntensity, targetIntensity, ratio);
    }
}
