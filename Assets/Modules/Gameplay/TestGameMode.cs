using System;
using Cysharp.Threading.Tasks;
using Modules.Card;
using UnityEngine;
using UnityEngine.Assertions;

namespace Modules.Gameplay
{
    public class TestGameMode : MonoBehaviour, IGameMode
    {
        public int turnCount => _turnCount;

        [SerializeField] private int _turnCount;
        [SerializeField] private Deck _deck;
        [SerializeField] private GameObject _defaultGameStateObject;

        private IGameState _defaultState;
        private IGameState _currentState;

        private void Awake()
        {
            _defaultState = _defaultGameStateObject.GetComponent<IGameState>();
        }
        public void StartGame()
        {
            _deck.TryCreateDeck();
            Assert.IsNotNull(_defaultState);
            _currentState = _defaultState;
            ProcessState();
        }
        public void AssignState(IGameState nextState)
        {
            if (nextState.stateName == _defaultState.stateName)
                _turnCount++;
            
            _currentState = nextState;
            ProcessState();
        }
        private void ProcessState()
        {
            _currentState.Process().Forget();
        }
    }
}