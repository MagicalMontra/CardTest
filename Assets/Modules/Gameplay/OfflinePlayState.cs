using System;
using Cysharp.Threading.Tasks;
using Modules.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public class OfflinePlayState : MonoBehaviour, IGameState
    {
        public string stateName => "TurnState";

        [SerializeField] private int _aiCount;

        public bool Validate()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IPlayBehaviour
    {
        bool commit { get; }
        UniTaskVoid Play();
    }
    
    public class OfflinePlayerPlayBehaviour : MonoBehaviour, IPlayBehaviour
    {
        public bool commit { get; }

        [SerializeField] private Button _playButton;
        [SerializeField] private BasePlayer _basePlayer;

        private void Awake()
        {
            
        }
        public async UniTaskVoid Play()
        {
            await UniTask.WaitUntil(() => commit);
        }
    }
}