using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Modules.Card;
using Modules.Infastructure;
using nanoid;
using UnityEngine;

namespace Modules.Gameplay
{
    public class IntermediateAiPlayer : MonoBehaviour, IPlayer
    {
        public string id { get; private set; }
        public bool commit { get; private set; }
        public int cardCount => _cards.Count;
        public ILifePoint lifePoint => _lifePoint;

        [SerializeField] private int _startHandCard = 3;
        [SerializeField] private Deck _deck;
        [SerializeField] private GameObject _aiBrainObject;
        [SerializeField] private GameObject _lifePointObject;

        private IAiBrain _aiBrain;
        private ILifePoint _lifePoint;
        private List<CardData> _cards = new List<CardData>();
        private void Awake()
        {
            id = $"Ai{NanoId.Generate(4)}";
            _aiBrain ??= _aiBrainObject.GetComponent<IAiBrain>();
            _lifePoint ??= _lifePointObject.GetComponent<ILifePoint>();
        }
        public void AddCard()
        {
            if (_cards.Count >= 7)
                return;
            
            _cards.Add(_deck.TryDraw(_lifePoint));
        }
        public void OnGameStart()
        {
            for (int i = 0; i < _startHandCard; i++)
                _cards.Add(_deck.TryDraw());
        }
        public void OnRoundStart()
        {
            commit = false;
            _cards.Add(_deck.TryDraw());
            _cards.Add(_deck.TryDraw());
        }
        public async UniTask<CardData[]> Process()
        {
            while (_cards.Count < 5)
            {
                AddCard();
                Debug.Log($"Ai {id} draw a card");
                await UniTask.Delay(100);
            }
            
            var bestPair = _aiBrain.FindPair(_cards.ToArray());
            Debug.Log($"Ai {id} commited {bestPair[0].rank}{bestPair[0].color}{bestPair[0].element} & {bestPair[1].rank}{bestPair[1].color}{bestPair[1].element}");
            commit = true;
            return bestPair;
        }
    }
}