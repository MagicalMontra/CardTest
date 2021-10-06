using System;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using Modules.Card;
using Modules.Player;
using UnityEngine;

namespace Modules.Gameplay
{
    public class IntermediateAiPlayer : MonoBehaviour, IPlayer
    {
        public bool commit { get; private set; }
        
        [SerializeField] private int _startHandCard = 3;
        [SerializeField] private Deck _deck;
        [SerializeField] private GameObject _aiBrainObject;
        [SerializeField] private GameObject _lifePointObject;

        private IAiBrain _aiBrain;
        private ILifePoint _lifePoint;
        private List<CardData> _cards = new List<CardData>();
        private void Awake()
        {
            _aiBrain ??= _aiBrainObject.GetComponent<IAiBrain>();
            _lifePoint ??= _lifePointObject.GetComponent<ILifePoint>();
        }
        public void AddCard()
        {
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
            while (_cards.Count < 3)
            {
                AddCard();
                await UniTask.Delay(100);
            }
            
            var bestPair = _aiBrain.FindPair(_cards.ToArray());
            commit = true;
            return bestPair;
        }
    }
}