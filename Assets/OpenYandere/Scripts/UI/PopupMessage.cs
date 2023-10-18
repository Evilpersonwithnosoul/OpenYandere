using OpenYandere.Managers.Traits;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

internal class PopupMessage : Singleton<PopupMessage>
{
    public UnityEvent<string> onDisplayMessage = new UnityEvent<string>();
    public CanvasGroup canvasGroup;
    public static TextMeshProUGUI messageText;
    public Image messageIcon;

    public float displayTime = 2.3f;
    private float timer;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private bool isDisplaying = false;

    void Awake()
    {
        onDisplayMessage ??= new UnityEvent<string>();
        onDisplayMessage.AddListener(DisplayMessage);
        _rectTransform = GetComponent<RectTransform>();
        if (!TryGetComponent<CanvasGroup>(out _canvasGroup))
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        _canvasGroup.alpha = 0;
    }

    void FixedUpdate(){

        if (isDisplaying)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                HideMessage();
            }
        }
    }

    public void DisplayMessage(string message)
    {
        _rectTransform.anchoredPosition = new Vector2(0, 200);
        _rectTransform.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutQuad);
        _canvasGroup.DOFade(1, 0.5f);
        messageText.text = message;
        timer = displayTime;
        messageText.gameObject.SetActive(true);
        isDisplaying = true;
    }

    private void HideMessage()
    {
        _canvasGroup.DOFade(0, 1.5f).OnComplete(() => {
            messageText.gameObject.SetActive(false);
            isDisplaying = false;
        });
    }
}
