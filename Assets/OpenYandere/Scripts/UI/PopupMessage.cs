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
    public UnityEvent<string> onDisplayMessage = new();
    protected RectTransform _rectTransform;
    protected CanvasGroup _canvasGroup;
    protected static TextMeshProUGUI messageText;
    protected static Image messageIcon;

    public float displayTime = 2.3f;
    protected float timer;
    protected bool isDisplaying = false;

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
        messageText = GetComponentInChildren<TextMeshProUGUI>();
        messageIcon = GetComponentInChildren<Image>();
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
        _rectTransform.DOKill();
        _rectTransform.DOAnchorPosY(0, 0.5f).SetEase(Ease.OutQuad);
        _canvasGroup.DOFade(1, 0.5f);
        messageText.SetText(message);
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
