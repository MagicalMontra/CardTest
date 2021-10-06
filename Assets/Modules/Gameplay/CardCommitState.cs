using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Infastructure;
using UnityEngine;

namespace Modules.Gameplay
{
    public class CardCommitState : MonoBehaviour, IGameState
    {
        public string stateName => "CardCommitState";
        
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject _pairRuleObject;
        [SerializeField] private GameObject[] _gameRuleObjects;
        [SerializeField] private GameObject[] _aiPlayerObjects;
        
        private IPlayer _player;
        private IPairRule _pairRule;
        private IPlayer[] _aiPlayers;
        private IGameRule[] _gameRules;
        
        public void Reset()
        {
            Initialize();
        }
        public bool Validate()
        {
            var isPlayerFinished = _player.commit;
            var isAisFinished = _aiPlayers.All(ai => ai.commit);
            return isPlayerFinished && isAisFinished;
        }
        public async UniTask<bool> Process()
        {
            if (_player.lifePoint.value <= 0)
            {
                return true;
            }
            
            await UniTask.WaitUntil(() => _player.commit);
            var playerPair = await _player.Process();
            var playerValue = _pairRule.CalculatePair(playerPair);

            Dictionary<IPlayer, float> playersValue = new Dictionary<IPlayer, float>();
            playersValue.Add(_player, playerValue);

            for (int i = 0; i < _aiPlayers.Length; i++)
            {
                if (_aiPlayers[i].lifePoint.value <= 0)
                    continue;

                var aiPair = await _aiPlayers[i].Process();
                var aiValue = _pairRule.CalculatePair(aiPair);
                playersValue.Add(_aiPlayers[i], aiValue);
            }

            Dictionary<IPlayer, float> winners = new Dictionary<IPlayer, float>();

            for (int i = 0; i < _gameRules.Length; i++)
            {
                winners = _gameRules[i].DecideWinner(playersValue);
            }
            
            var loserCount = playersValue.Count - winners.Count;

            foreach (var winner in winners)
            {
                winner.Key.lifePoint.Modify(loserCount / winners.Count);
            }

            return true;
        }
        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            _player ??= _playerObject.GetComponent<IPlayer>();
            _pairRule ??= _pairRuleObject.GetComponent<IPairRule>();

            if (_aiPlayerObjects == null || _aiPlayerObjects.Length <= 0)
                return;
            
            _aiPlayers = new IPlayer[_aiPlayerObjects.Length];
            
            for (int i = 0; i < _aiPlayerObjects.Length; i++)
            {
                _aiPlayers[i] ??= _aiPlayerObjects[i].GetComponent<IPlayer>();
            }
            
            if (_gameRuleObjects == null || _gameRuleObjects.Length <= 0)
                return;
                
            _gameRules ??= new IGameRule[_gameRuleObjects.Length];
            
            for (int i = 0; i < _gameRuleObjects.Length; i++)
            {
                _gameRules[i] ??= _gameRuleObjects[i].GetComponent<IGameRule>();
            }
        }
    }

    public interface IPlayer
    {
        string id { get; }
        bool commit { get; }
        ILifePoint lifePoint { get; }
        void AddCard();
        void OnGameStart();
        void OnRoundStart();
        UniTask<CardData[]> Process();
    }
}