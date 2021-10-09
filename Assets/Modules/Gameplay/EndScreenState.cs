using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Modules.Gameplay
{
    public class EndScreenState : MonoBehaviour, IGameState
    {
        public string stateName => "EndScreenState";

        [SerializeField] private GameObject _endCanvas;
        [SerializeField] private Button _restartButton;
        [SerializeField] private GameObject _gameModeObject;
        
        private IGameMode _gameMode;
        public async UniTaskVoid Process()
        {
            _endCanvas.SetActive(true);
            await UniTask.Yield();
        }
        private void Awake()
        {
            _gameMode ??= _gameModeObject.GetComponent<IGameMode>();
            _restartButton.onClick.AddListener(() =>
            {
                _endCanvas.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            });
        }
    }
}