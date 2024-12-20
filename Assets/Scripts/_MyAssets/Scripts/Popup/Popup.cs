using UnityEngine;
using DG.Tweening;

public abstract class Popup : MonoBehaviour
{
    public const float POPUP_ANIMATION_TIME = 0.5f;

    public Transform body;
    public virtual void OnEnable()
    {
        Open();
    }
    public virtual void Open()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1f, POPUP_ANIMATION_TIME);
        if (body == null)
        {
            Debug.LogError($"body is not define in {gameObject.name}");
            body = transform.GetChild(0);
        }
        body.transform.localScale = Vector3.one * 0.5f;
        body.DOScale(1f, POPUP_ANIMATION_TIME).SetEase(Ease.OutBack);
    }
    public virtual void Close()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, POPUP_ANIMATION_TIME);
        if (body == null)
        {
            Debug.LogError($"body is not define in {gameObject.name}");
            body = transform.GetChild(0);
        }
        body.DOScale(0f, POPUP_ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}