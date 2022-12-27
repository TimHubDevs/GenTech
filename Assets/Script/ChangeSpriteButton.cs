using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpriteButton : ButtonBehaviour
{
    [SerializeField] private Image _buttonImage;
    [SerializeField] private IconsType _iconsTypeOn;
    [SerializeField] private IconsType _iconsTypeOff;
    private IconsType changingType;

    protected override void AnimateClick()
    {
        changingType = _buttonImage.sprite.name == _iconsTypeOn.ToString() ?  _iconsTypeOn : _iconsTypeOff;
        Debug.Log($"{changingType}");

        Sequence buttonSequence = DOTween.Sequence();
        buttonSequence.Append(gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.5f))
            .AppendCallback(() =>
            {
                _buttonImage.sprite = GameData.SpriteLibrary[changingType];
            })
            .Append(gameObject.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f));
    }
}