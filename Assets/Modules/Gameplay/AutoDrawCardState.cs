using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Modules.Gameplay
{

    public class AutoDrawCardState : MonoBehaviour, IGameState
    {
        public string stateName => "DrawCardState";
        
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject _gameModeObject;
        [SerializeField] private GameObject[] _aiPlayerObjects;
        [SerializeField] private GameObject _winningStateObject;
        [SerializeField] private GameObject _playerDeadStateObject;
        [SerializeField] private GameObject _defaultNextStateObject;

        private IPlayer _player;
        private IGameMode _gameMode;
        private IPlayer[] _aiPlayers;
        private IGameState _winningState;
        private IGameState _playerDeadState;
        private IGameState _defaultNextState;

        public async UniTaskVoid Process()
        {
            if (_player.lifePoint.value <= 0)
            {
                _gameMode.AssignState(_playerDeadState);
                await UniTask.Yield();
                return;
            }

            if (_player.lifePoint.value > 0 && _aiPlayers.All(ai => ai.lifePoint.value <= 0))
            {
                _gameMode.AssignState(_winningState);
                await UniTask.Yield();
                return;
            }
            
            if (_gameMode.turnCount <= 0)
            {
                _player.OnGameStart();
                
                for (int i = 0; i < _aiPlayers.Length; i++)
                {
                    _aiPlayers[i].OnGameStart();
                }    
                
                _gameMode.AssignState(_defaultNextState);
                await UniTask.Yield();
                return;
            }

            
            if (_player.cardCount < 4)
                _player.OnRoundStart();

            for (int i = 0; i < _aiPlayers.Length; i++)
            {
                if (_aiPlayers[i].cardCount < 4)
                    _aiPlayers[i].OnRoundStart();
            }
            
            _gameMode.AssignState(_defaultNextState);
            await UniTask.Yield();

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
            _winningState ??= _winningStateObject.GetComponent<IGameState>();
            _playerDeadState ??= _playerDeadStateObject.GetComponent<IGameState>();
            _defaultNextState ??= _defaultNextStateObject.GetComponent<IGameState>();
            
            if (_aiPlayerObjects == null || _aiPlayerObjects.Length <= 0)
                return;
            
            _aiPlayers = new IPlayer[_aiPlayerObjects.Length];
            
            for (int i = 0; i < _aiPlayerObjects.Length; i++)
            {
                _aiPlayers[i] ??= _aiPlayerObjects[i].GetComponent<IPlayer>();
            }
        }
    }
}