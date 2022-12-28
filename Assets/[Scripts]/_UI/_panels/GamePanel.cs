﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace EKTemplate
{
    public class GamePanel : Panel
    {
        public RectTransform coinPanelRect;
        public RectTransform restartButtonRect;

        [HideInInspector] int inGameCurrency;
        public GameObject firebar;
        private Tween tween;
        public TextMeshProUGUI textChar;
        public Image barColor;
        public GameObject img;
        private Text moneyText { get { return coinPanelRect.GetChild(1).GetChild(0).GetComponent<Text>(); } }
        private Button restartButton { get { return restartButtonRect.GetComponent<Button>(); } }
        public RectTransform ibre;
        private void Update()
        {
            moneyText.text = GameManager.instance.money.ToString();
        }
        private void Start()
        {
            restartButton.onClick.AddListener(OnClickRestartButton);
            AddMoney(0);
        }

        public void SetMoney(float to, float duration = 0.3f)
        {
            if (tween != null) tween.Kill();

            coinPanelRect
            .DOScale(1.2f, duration * 0.5f)
            .SetEase(Ease.Linear)
            .SetLoops(2, LoopType.Yoyo);

            float startFrom = int.Parse(moneyText.text);
            tween = DOTween.To((x) => startFrom = x, startFrom, to, duration)
            .OnUpdate(() =>
            {
                moneyText.text = ((int)startFrom).ToString();
            })
            .OnComplete(() => moneyText.text = ((int)to).ToString());
        }

        public void AddMoney(int amount)
        {
            moneyText.text = GameManager.instance.money.ToString();
            //float startFrom = int.Parse(moneyText.text);
            //SetMoney(inGameCurrency + amount);
        }

        private void OnClickRestartButton()
        {
            GameManager.instance.RestartScene();
        }
    }
}