using DG.Tweening;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup InfoBlock;

    private void Start()
    {
        DOTween.Init();
        ChangeState(InfoBlock);
    }

    private void ChangeState(CanvasGroup canvasGroup)
    {
        bool isActive = InfoBlock.alpha != 0f;
        if (isActive)
        {
            ChangeVisibility(canvasGroup, 0, false);
        }
        else
        {
            ChangeVisibility(canvasGroup, 1, true);
        }
    }

    private void ChangeVisibility(CanvasGroup canvasGroup, float endAlpha, bool interactable)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.PrependCallback(() => { canvasGroup.interactable = false; })
            .Append(DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, endAlpha, 3))
            .AppendCallback(() => { canvasGroup.interactable = interactable; });
    }
}