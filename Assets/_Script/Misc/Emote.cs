using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Emote : MonoBehaviour
{
    [SerializeField] private Image EmoteImg;
    [SerializeField] private Button Btn;

    private int EmoteId;

    private void OnEnable()
    {
        Btn.onClick.AddListener(OnBtnClicked);
    }

    private void OnBtnClicked()
    {
        //TODO
    }

    public void Initialize(int emoteId, Sprite sprite)
    {
        EmoteId = emoteId;
        EmoteImg.sprite = sprite;
    }
}
