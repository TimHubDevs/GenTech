using UnityEngine;

public class CardChoiserView : MonoBehaviour
{
    [SerializeField] private CardView prefab;
    [SerializeField] private RectTransform rect;

    public void Init(int countOfCards)
    {
        for (int i = 0; i < countOfCards; i++)
        {
            var cardObject = Instantiate(prefab, rect);
            
            cardObject.GetComponent<CardView>().cardImage.sprite = GameData.SpriteLibrary[];
        }
    }
}
