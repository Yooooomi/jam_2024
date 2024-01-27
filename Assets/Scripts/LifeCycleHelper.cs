using UnityEngine;

public static class LifeCycleHelper
{
    public static bool IsDestroyed(Object target) {
        return target == null || target.Equals(null);
    }
}
