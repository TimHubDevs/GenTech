using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _imageEffect;
    [SerializeField] private Material _effectMaterial;
    [SerializeField] public Image cardImage;
    [SerializeField] private RectTransform _rect;

    public void PlayTouchEffect()
    {
        _effectMaterial.mainTexture = cardImage.sprite.texture;
        var effect = Instantiate(_imageEffect, transform.localPosition, Quaternion.identity, transform);
        effect.transform.localScale = new Vector3(0.05f, 0.05f, 1);
        effect.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -17280);
        effect.Play();
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(DOTween.To(() => _rect.sizeDelta, x => _rect.sizeDelta = x, new Vector2(_rect.sizeDelta.x, 1), 2))
            .SetEase(Ease.Flash)
            .AppendCallback(() =>
            {
                DestroyImmediate(effect.gameObject);
                DestroyImmediate(gameObject);
            });
    }
}
