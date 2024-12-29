using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatFoodAnimation : MonoBehaviour
{
    public void Show()
    {
        transform.DOScale(1.3f, 1f).SetEase(Ease.OutBounce).OnComplete(() => 
        {
            transform.DOScale(1f, 1f).SetEase(Ease.OutBounce);
        });
    }
}
