using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Card;
using Modules.Infastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public class OfflinePlayer : MonoBehaviour, IPlayer
    {
        public string id { get; private set; }
        public bool commit { get; private set; }
        public int cardCount => _cards.Count;
        
        public ILifePoint lifePoint => _lifePoint;
        
        [SerializeField] private int _handCardCount = 7;
        [SerializeField] private int _startHandCard = 5;
        
        [SerializeField] private Deck _deck;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _drawButton;

        [SerializeField] private Transform _cardCanvasGroup;
        [SerializeField] private GameObject _lifePointObject;
        [SerializeField] private CardVisual _cardVisualPrefab;

        
        private ILifePoint _lifePoint;
        private CardVisual[] _handCards;
        private List<CardData> _cards = new List<CardData>();
        private CardData _slotOne;
        private CardData _slotTwo;
        
        public void AddCard()
        {
            if (lifePoint.value <= 1)
                return;
            
            if (_cards.Count >= 7)
                return;
            
            _cards.Add(_deck.TryDraw(_lifePoint));
            
            for (int i = 0; i < _handCards.Length; i++)
                _handCards[i].Dispose();

            for (int i = 0; i < _cards.Count; i++)
                _handCards[i].Initialize(_cards[i], SelectCard);
        }
        public void OnGameStart()
        {
            commit = false;
            
            for (int i = 0; i < _startHandCard; i++)
                _cards.Add(_deck.TryDraw());
            
            for (int i = 0; i < _handCards.Length; i++)
                _handCards[i].Dispose();
            
            for (int i = 0; i < _cards.Count; i++)
            {
                if (i > _handCards.Length - 1)
                    continue;

                _handCards[i].Initialize(_cards[i], SelectCard);
            }
        }
        public void OnRoundStart()
        {
            commit = false;
            _slotOne = null;
            _slotTwo = null;
            _playButton.interactable = false;
            _cards.Add(_deck.TryDraw());
            _cards.Add(_deck.TryDraw());
            
            for (int i = 0; i < _handCards.Length; i++)
                _handCards[i].Dispose();

            for (int i = 0; i < _cards.Count; i++)
            {
                if (i > _handCards.Length - 1)
                    continue;

                _handCards[i].Initialize(_cards[i], SelectCard);
            }
        }
        private void Awake()
        {
            id = "Player";
            
            _lifePoint ??= _lifePointObject.GetComponent<ILifePoint>();
            _handCards = new CardVisual[_handCardCount];

            for (int i = 0; i < _handCardCount; i++)
                _handCards[i] = Instantiate(_cardVisualPrefab, _cardCanvasGroup);
            
            _drawButton.onClick.RemoveAllListeners();
            _drawButton.onClick.AddListener(AddCard);
            
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(CommitCard);
        }

        private void CommitCard()
        {
            commit = _slotOne != null && _slotTwo != null;
            _cards.Remove(_slotOne);
            _cards.Remove(_slotTwo);
            
            for (int i = 0; i < _handCards.Length; i++)
                _handCards[i].Dispose();
            
            for (int i = 0; i < _cards.Count; i++)
            {
                if (i > _handCards.Length - 1)
                    continue;

                _handCards[i].Initialize(_cards[i], SelectCard);
            }
        }
        private void SelectCard(CardData cardData)
        {
            var index = _cards.FindIndex(card => card == cardData);
            
            if (index > _cards.Count)
                return;

            if (_slotOne == null)
            {
                _slotOne = _cards[index];
                return;
            }

            if (_slotTwo == null && _cards[index] != _slotOne)
            {
                _slotTwo = _cards[index];
                _playButton.interactable = true;
                return;
            }

            if (_slotOne != null && _slotTwo != null)
            {
                _slotOne = null;
                _slotTwo = null;
            }

            SelectCard(cardData);
        }
        public async UniTask<CardData[]> Process()
        {
            await UniTask.WaitUntil(() => commit);
            return new []{ _slotOne, _slotTwo };
        }
    }
}