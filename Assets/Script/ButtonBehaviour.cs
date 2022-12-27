using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private void Start()
    {
        _button.onClick.AddListener(AnimateClick);
    }

    protected virtual void AnimateClick()
    {
        Sequence buttonSequence = DOTween.Sequence();
        buttonSequence.Append(gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.5f))
            .Append(gameObject.transform.DOScale(new Vector3(1f, 1f, 1), 0.5f));
    }
}
