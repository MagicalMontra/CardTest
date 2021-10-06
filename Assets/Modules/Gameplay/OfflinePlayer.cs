using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Card;
using Modules.Infastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public class OfflinePlayer : MonoBehaviour, IPlayer
    {
        public string id { get; private set; }
        public bool commit { get; private set; }
        
        public ILifePoint lifePoint => _lifePoint;
        
        [SerializeField] private int _handCardCount = 7;
        [SerializeField] private int _startHandCard = 5;
        
        [SerializeField] private Deck _deck;
        [SerializeField] private Button _playButton;
        [SerializeField] private Transform _cardCanvasGroup;
        [SerializeField] private GameObject _lifePointObject;
        [SerializeField] private CardVisual _cardVisualPrefab;

        
        private ILifePoint _lifePoint;
        private CardVisual[] _handCards;
        private CardData[] _selectedPairs;
        private List<CardData> _cards = new List<CardData>();
        
        public void AddCard()
        {
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
            for (int i = 0; i < _startHandCard; i++)
                _cards.Add(_deck.TryDraw());
            
            for (int i = 0; i < _cards.Count; i++)
                _handCards[i].Initialize(_cards[i], SelectCard);
        }
        public void OnRoundStart()
        {
            commit = false;
            _cards.Add(_deck.TryDraw());
            _cards.Add(_deck.TryDraw());
            
            for (int i = 0; i < _handCards.Length; i++)
                _handCards[i].Dispose();
            
            for (int i = 0; i < _cards.Count; i++)
                _handCards[i].Initialize(_cards[i], SelectCard);
        }
        private void Awake()
        {
            id = "Player";
            
            _lifePoint ??= _lifePointObject.GetComponent<ILifePoint>();
            _handCards = new CardVisual[_handCardCount];

            for (int i = 0; i < _handCardCount; i++)
                _handCards[i] = Instantiate(_cardVisualPrefab, _cardCanvasGroup);
            
            _playButton.onClick.RemoveAllListeners();
            _playButton.onClick.AddListener(CommitCard);
        }

        private void CommitCard()
        {
            var bothSelected = _selectedPairs[0] != null && _selectedPairs[1] != null;
            commit = bothSelected;
        }
        private void SelectCard(CardData cardData)
        {
            var index = _cards.FindIndex(card => card == cardData);
            
            if (index > _cards.Count)
                return;

            var slotOneEmpty = _cards[0] == null;
            
            if (slotOneEmpty)
            {
                _selectedPairs[0] = _cards[index];
                return;
            }

            var slotTwoEmpty = _selectedPairs[1] == null;

            if (slotTwoEmpty)
            {
                _selectedPairs[1] = _cards[index];
                return;
            }

            var bothSlotSelected = _selectedPairs[0] != null && _selectedPairs[1] != null;

            if (!bothSlotSelected) 
                return;
            
            _selectedPairs[0] = null;
            _selectedPairs[1] = null;
            SelectCard(cardData);
        }
        public async UniTask<CardData[]> Process()
        {
            await UniTask.WaitUntil(() => commit);
            return _selectedPairs;
        }
    }
}