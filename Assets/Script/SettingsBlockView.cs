using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBlockView : MonoBehaviour
{
    [SerializeField] private Button _musicButton;
    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _soundOn;
    [SerializeField] private Sprite _soundOff;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _soundAudioSource;

    private bool isPlayMusic;
    private bool isPlaySound;

    private void Start()
    {
        isPlayMusic = true;
        isPlaySound = true;

        if (isPlayMusic)
        {
            _musicAudioSource.Play();
        }
    }

    public void MusicButtonPressed()
    {
        if (isPlayMusic)
        {
            _musicButton.GetComponent<Image>().sprite = _musicOff;
            isPlayMusic = false;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(DOTween.To(() => _musicAudioSource.volume, x => _musicAudioSource.volume = x, 0, 2))
                .SetEase(Ease.Flash)
                .AppendCallback(() => { _musicAudioSource.Pause(); });
            Debug.LogError("Music off");
        }
        else
        {
            _musicButton.GetComponent<Image>().sprite = _musicOn;
            isPlayMusic = true;
            Sequence mySequence = DOTween.Sequence();
            mySequence.PrependCallback(() => _musicAudioSource.Play())
                .Append(DOTween.To(() => _musicAudioSource.volume, x => _musicAudioSource.volume = x, 0.8f, 2))
                .SetEase(Ease.Flash);
            Debug.LogError("Music on");
        }
    }

    public void SoundButtonPressed()
    {
        if (isPlaySound)
        {
            _soundButton.GetComponent<Image>().sprite = _soundOff;
            isPlaySound = false;
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(DOTween.To(() => _soundAudioSource.volume, x => _soundAudioSource.volume = x, 0, 2))
                .SetEase(Ease.Flash)
                .AppendCallback(() => { _soundAudioSource.Stop(); });
            Debug.LogError("Sound off");
        }
        else
        {
            _soundButton.GetComponent<Image>().sprite = _soundOn;
            Sequence mySequence = DOTween.Sequence();
            mySequence.PrependCallback(() => _soundAudioSource.Play())
                .Append(DOTween.To(() => _soundAudioSource.volume, x => _soundAudioSource.volume = x, 1f, 2))
                .SetEase(Ease.Flash);
            isPlaySound = true;
            Debug.LogError("Sound on");
        }
    }
}