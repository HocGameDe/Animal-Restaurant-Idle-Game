using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSpawnVisitor : MonoBehaviour
{
    public const float MAX_PRESS_REQUIRE = 10;
    public const float PROGRESSBAR_FILL_TIME = 0.2f;

    public TextFlyEffect textEffect;
    public Image imgFill;
    public int count = 0;

    private void Start()
    {
        UpdateDisplay();
    }

    public void OnClick()
    {
        count++;
        if (count == MAX_PRESS_REQUIRE)
        {
            count = 0;
            CharacterSpawner.Instance.SpawnVisitor();
            var text = MyPoolManager.Instance.Get<TextFlyEffect>(textEffect.gameObject);
            text.Fly("Promoted!", Vector2.zero);
        }
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        float ratio = count / MAX_PRESS_REQUIRE;
        imgFill.DOFillAmount(ratio, 0.2f); 
    }
}