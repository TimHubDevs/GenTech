using DG.Tweening;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup _infoBlock;
    [SerializeField] private CanvasGroup _settingsBlock;
    
    [SerializeField] private RectTransform _horizontalRect;
    [SerializeField] private RectTransform _verticalRect;
    
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;
    [SerializeField] private AudioClip _soundButtonClick;
    
    [SerializeField] private GameObject _circularList;
    [SerializeField] private GameObject _verticalScrollList;
    private GameObject _circularListObject;
    private GameObject _verticalScrollListObject;

    private void Awake()
    {
        DOTween.Init();
    }

    private void Start()
    {
        ChangeVisibilityInfoBlock();
        CreateCore();
    }

    private void CreateCore()
    {
        _circularListObject = Instantiate(_circularList, _horizontalRect);
        _verticalScrollListObject = Instantiate(_verticalScrollList, _verticalRect);
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
        }
        else
        {
            ChangeVisibility(canvasGroup, 1, true);
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

    public void ResetMainScreen()
    {
        DOTween.KillAll();
        DestroyImmediate(_circularListObject);
        DestroyImmediate(_verticalScrollListObject);
        CreateCore();
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }
}