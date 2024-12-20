using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ThinkingBox : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.7f;

    private CanvasGroup canvasGroup;
    [SerializeField] private Image imgDemo;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        canvasGroup.alpha = 0f;
    }
    public void Show(CandyData candy)
    {
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        imgDemo.sprite = candy.sprite;
    }

    public void Hide()
    {
        canvasGroup.DOFade(0f, ANIMATION_TIME);
    }
}