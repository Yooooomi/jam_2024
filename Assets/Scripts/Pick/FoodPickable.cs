public class FoodPickable : ThrowablePickable
{
    protected override bool ReactToRelease(float power)
    {
        bool isLaunched = base.ReactToRelease(power);
        if (!isLaunched) {
            return false;
        }
        GameState.instance.gamePoints.RegisterFoodThrow(oldHolder);
        return isLaunched;
    }
}
