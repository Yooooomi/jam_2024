using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Transform root;
    [Serializable]
    public struct AnimationInterval {
        public string name;
        public float interval;
    }

    [SerializeField]
    private SpriteLibraryAsset[] colors;

    [SerializeField]
    private AnimationInterval[] intervalsArray;

    private SpriteResolver resolver;
    [SerializeField]
    private Dictionary<string, float> intervals = new Dictionary<string, float>();
    private Dictionary<string, string[]> counts = new Dictionary<string, string[]>();
    
    private string category = "idle_down";
    private int index = 0;

    private float nextChange;

    private void Awake() {
        foreach (var item in intervalsArray)
        {
            intervals[item.name] = item.interval;
        }
        resolver = GetComponent<SpriteResolver>();
        SpriteLibraryAsset library = resolver.spriteLibrary.spriteLibraryAsset;
        foreach (var i in library.GetCategoryNames()) {
            counts[i] = library.GetCategoryLabelNames(i).ToArray();
        }
    }

    private void Start() {
        int myIndex = GameState.instance.GetPlayerIndex(root.gameObject.GetInstanceID());
        resolver.spriteLibrary.spriteLibraryAsset = colors[myIndex];
        SetAnimationName(category);
    }

    private bool CompareCurrentAnimation(string anim) {
        return category == anim;
    }

    public void SetAnimationName(string animationName) {
        if (CompareCurrentAnimation(animationName)) {
            return;
        }

        category = animationName;
        index = 0;

        string[] labels = counts[category];
        string label = labels[index];
        resolver.SetCategoryAndLabel(category, label);
        float interval = intervals[category];
        nextChange = Time.time + interval;
    }

    private void Update() {
        if (Time.time < nextChange) {
            return;
        }
        string[] labels = counts[category];
        index = (index + 1) % labels.Length;
        string label = labels[index];
        resolver.SetCategoryAndLabel(category, label);
        float interval = intervals[category];
        nextChange = Time.time + interval - (nextChange - Time.time);
    }
}
