using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Infastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public interface IBonusRule
    {
        float Calculate(IPlayer player, CardData[] pair);
    }

    public class ElementMatch : MonoBehaviour, IBonusRule
    {
        public float Calculate(IPlayer player, CardData[] pair)
        {
            throw new NotImplementedException();
        }
    }
    public class CardCommitState : MonoBehaviour, IGameState
    {
        public string stateName => "CardCommitState";

        [SerializeField] private GameObject _resultObject;
        [SerializeField] private TextMeshProUGUI _resultText;

        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject _gameModeObject;
        [SerializeField] private GameObject _pairRuleObject;
        [SerializeField] private GameObject[] _gameRuleObjects;
        [SerializeField] private GameObject[] _aiPlayerObjects;
        [SerializeField] private GameObject _winningStateObject;
        [SerializeField] private GameObject _playerDeadStateObject;
        [SerializeField] private GameObject _defaultNextStateObject;
        
        private IPlayer _player;
        private IGameMode _gameMode;
        private IPairRule _pairRule;
        private IPlayer[] _aiPlayers;
        private IGameRule[] _gameRules;
        private IGameState _playerDead;
        private IGameState _winningState;
        private IGameState _defaultState;
        
        public async UniTaskVoid Process()
        {
            if (_player.lifePoint.value <= 0)
            {
                _gameMode.AssignState(_playerDead);
                await UniTask.Yield();
                return;
            }
            
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

            _resultText.text = "";
            _resultText.text += $"{winners.Count} winners\n";
            foreach (var winner in winners)
            {
                _resultText.text += $"{winner.Key.id} won with {winner.Value.ToString("F2")} points\n";
            }
            
            _resultObject.SetActive(true);
            await UniTask.Delay(4000);
            _resultObject.SetActive(false);
            
            var loserCount = playersValue.Count - winners.Count;

            foreach (var winner in winners)
            {
                winner.Key.lifePoint.Modify(loserCount / winners.Count);
                _resultText.text += $"{winner.Key.id} get +{loserCount / winners.Count}lp\n";
            }

            var losers = playersValue.Except(winners);

            foreach (var keyValuePair in losers)
            {
                keyValuePair.Key.lifePoint.Modify(-1);
            }

            _gameMode.AssignState(_defaultState);
            await UniTask.Yield();
        }
        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            _player ??= _playerObject.GetComponent<IPlayer>();
            _gameMode ??= _gameModeObject.GetComponent<IGameMode>();
            _pairRule ??= _pairRuleObject.GetComponent<IPairRule>();
            _playerDead ??= _playerDeadStateObject.GetComponent<IGameState>();
            _defaultState ??= _defaultNextStateObject.GetComponent<IGameState>();
            _winningState ??= _winningStateObject.GetComponent<IGameState>();

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
}