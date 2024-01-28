using System.Linq;
using UnityEngine;

[System.Serializable]
public class JuggleDot : Dot
{
  public readonly float value;
  public JuggleDot(float duration) {
    this.duration = duration;
  }
}

public class JuggleModifier : DotManager<JuggleDot> {
  public bool CanJuggle() {
    return dots.Count == 0;
  }
}
