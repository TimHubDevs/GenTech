using UnityEngine;
using UnityEngine.UI;

public class IconsManager : MonoBehaviour
{
    [SerializeField] private Image _settingImage;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Image _soundImage;
    [SerializeField] private Image _infoImage;
    [SerializeField] private Image _exitSettingsImage;
    [SerializeField] private Image _exitInfoImage;
    
    private void Awake()
    {
        _settingImage.sprite = GameData.SpriteLibrary[IconsType.SettingsWhite];
        _musicImage.sprite = GameData.SpriteLibrary[IconsType.MusicOn];
        _soundImage.sprite = GameData.SpriteLibrary[IconsType.SoundOn];
        _infoImage.sprite = GameData.SpriteLibrary[IconsType.InfoGreen];
        _exitSettingsImage.sprite = GameData.SpriteLibrary[IconsType.ExitRed];
        _exitInfoImage.sprite = GameData.SpriteLibrary[IconsType.ExitRed];
        Debug.Log("Assigned image");
    }
}
