public class TextLevelUnlockPowerup : TextUpdater
{
    public override float GetValue() => 0;
    public override void UpdateText(float newValue)
    {
        int val = (int)(newValue + 1);
        if (val < 10)
        {
            txt.text = $"Lv.0{val}";
        }
        else 
        {
            txt.text = $"Lv.{val}";
        }
    }
}