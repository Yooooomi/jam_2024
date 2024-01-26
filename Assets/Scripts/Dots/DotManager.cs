using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dot
{
  public float duration;
}

public abstract class DotManager<D> : MonoBehaviour where D : Dot 
{
  protected List<D> dots = new List<D>();

  public void ApplyDot(D newDOt)
  {
    dots.Add(newDOt);
  }

  public void Update()
  {
    foreach (D dot in dots) {
      dot.duration -= Time.deltaTime;
    }
    dots.RemoveAll(d => d.duration <= 0);
  }
}
