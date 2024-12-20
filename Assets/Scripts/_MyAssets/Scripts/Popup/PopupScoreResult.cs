using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupScoreResult : Popup
{
    public TextMeshProUGUI txtScore;
    public Transform goalParent;
    public Transform goal;
    public Transform[] stars;

    public void Show(int score)
    {
        //Instantiate(goal.gameObject, goalParent.transform);
        txtScore.text = "0";
        DOVirtual.Float(0, score, 2.5f, (float f) =>
        {
            txtScore.text = f.ToString("F0");
        }).OnComplete(() =>
        {
            StartCoroutine(IEShowStar(3));
        });
    }

    private IEnumerator IEShowStar(int star)
    {
        var wait = new WaitForSeconds(0.2f);
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < stars.Length; i++)
        {
            if (i < star)
            {
                stars[i].transform.localScale = Vector3.zero;
                stars[i].transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            }
            yield return wait; 
        }
    }

    [ContextMenu("Test")]
    public void Test()
    {
        Show(9999);
    }
}