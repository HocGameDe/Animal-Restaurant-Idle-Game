using DG.Tweening;
using UnityEngine;

public class Table : VisitablePlace
{
    public const float ANIMATION_TIME = 1f;

    [SerializeField] private SpriteRenderer food;
    private void Start()
    {
        food.gameObject.SetActive(false);
    }
    public void ShowFood(CandyData candyData)
    {
        food.color = new Color(1, 1, 1, 0);
        food.gameObject.SetActive(true);
        food.DOFade(1f, ANIMATION_TIME);
        food.sprite = candyData.sprite;
    }
    public void HideFood()
    {
        food.DOFade(0f, ANIMATION_TIME).OnComplete(() =>
        {
            food.gameObject.SetActive(false);
        });
    }
}