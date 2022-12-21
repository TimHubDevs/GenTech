using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LinkMessage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private string _url;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL(_url);
    }
}
