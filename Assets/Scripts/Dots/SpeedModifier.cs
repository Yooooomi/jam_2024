using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpeedDot : Dot
{
  public float value;
  public SpeedDot(float duration, float value) {
    this.value = value;
    this.duration = duration;
  }
}

public class SpeedModifier : DotManager<SpeedDot> {
  public float getValue() {
    if (dots.Count == 0) {
      return 1.0f;
    }
    return dots.OrderBy((e) => e.value).First().value;
  }
}
