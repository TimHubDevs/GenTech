using System;
using DG.Tweening;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _infoBlock;
    [SerializeField] private CanvasGroup _settingsBlock;
    
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;
    [SerializeField] private AudioClip _soundButtonClick;

    private void Awake()
    {
        DOTween.Init();
    }

    private void Start()
    {
        ChangeVisibilityInfoBlock();
    }

    public void ChangeVisibilityInfoBlock()
    {
        ChangeState(_infoBlock);
    }
    
    public void ChangeVisibilitySettingsBlock()
    {
        ChangeState(_settingsBlock);
    }

    private void ChangeState(CanvasGroup canvasGroup)
    {
        bool isActive = canvasGroup.alpha != 0f;
        if (isActive)
        {
            ChangeVisibility(canvasGroup, 0, false);
            Debug.Log("Hide canvas group");
        }
        else
        {
            ChangeVisibility(canvasGroup, 1, true);
            Debug.Log("Show canvas group");
        }
    }

    private void ChangeVisibility(CanvasGroup canvasGroup, float endAlpha, bool activateObject)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.PrependCallback(() => { canvasGroup.gameObject.SetActive(true); })
            .Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, endAlpha, 2)).SetEase(Ease.Flash)
            .AppendCallback(() => { canvasGroup.gameObject.SetActive(activateObject); });
    }

    public void PlayClickSound()
    {
        _soundAudioSource.clip = _soundButtonClick;
        _soundAudioSource.Play();
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }
}