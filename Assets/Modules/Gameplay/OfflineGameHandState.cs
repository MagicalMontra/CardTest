using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Modules.Card;
using UnityEngine;

namespace Modules.Gameplay
{
    public class OfflineGameHandState : MonoBehaviour, IGameState
    {
        public string stateName => "HandState";

        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject[] _aiPlayerObjects;

        private IPlayer _player;
        private IPlayer[] _aiPlayers;
        
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
        public async void Process()
        {
            if (!_player.commit)
                await _player.Process();

            for (int i = 0; i < _aiPlayers.Length; i++)
            {
                await _aiPlayers[i].Process();
            }
        }
        private void Awake()
        {
            Initialize();
        }
        private void Initialize()
        {
            _player ??= _playerObject.GetComponent<IPlayer>();
            
            if (_aiPlayerObjects == null || _aiPlayerObjects.Length <= 0)
                return;
            
            _aiPlayers = new IPlayer[_aiPlayerObjects.Length];
            
            for (int i = 0; i < _aiPlayerObjects.Length; i++)
            {
                _aiPlayers[i] ??= _aiPlayerObjects[i].GetComponent<IPlayer>();
            }
        }
    }

    public interface IPlayer
    {
        bool commit { get; }
        void AddCard();
        void OnGameStart();
        void OnRoundStart();
        UniTask<CardData[]> Process();
    }
}