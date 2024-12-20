using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextFlyEffect : MonoBehaviour
{
    public TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    public void Fly(string msg, Vector3 startPos)
    {
        this.text.text = msg;
        transform.position = startPos;
        text.DOFade(1f, 0f);
        transform.DOMove(transform.position + new Vector3(0, 1f, 0f), 1f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            text.DOFade(0f, 1f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        });
    }
}