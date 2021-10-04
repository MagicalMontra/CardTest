using System;
using Modules.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _colorText;
        [SerializeField] private TextMeshProUGUI _elementText;

        private CardData _cardData;
        
        public void Initialize(CardData cardData, Action<CardData> selectCardAction)
        {
            _cardData = cardData;
            _rankText.text = _cardData.rank;
            _colorText.text = _cardData.color;
            _elementText.text = _cardData.element;
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => selectCardAction(cardData));
            
            gameObject.SetActive(true);
        }
        public void Dispose()
        {
            gameObject.SetActive(false);
        }
    }
}