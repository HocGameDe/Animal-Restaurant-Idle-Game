public class TextLevel : TextUpdater
{
    public override float GetValue() => GameSystem.userdata.level;
    public override void UpdateText(float newValue)
    {
        txt.text = $"Level {newValue + 1}";
    }
}