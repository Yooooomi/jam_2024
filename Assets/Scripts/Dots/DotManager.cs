using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dot
{
  public static float INFINITE_DURATION = -1f;

  public float duration;
}

public abstract class DotManager<D> : MonoBehaviour where D : Dot 
{
  protected List<D> dots = new List<D>();

  public void ApplyDot(D newDOt)
  {
    dots.Add(newDOt);
  }

  public void CancelDot(D newDot) {
    dots.Remove(newDot);
  }

  public void Update()
  {
    foreach (D dot in dots) {
      if (dot.duration < 0) {
        continue;
      }
      dot.duration -= Time.deltaTime;
      if (dot.duration < 0) {
        dot.duration = 0;
      }
    }
    dots.RemoveAll(d => d.duration == 0);
  }
}
