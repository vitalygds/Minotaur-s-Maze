using MyGame.General.Controller;
using MyGame.General.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace MyGame.Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        private SceneContext _sceneContext;
        private IControllersMediator _mediator;
        private string _startSceneName;
        private bool _isJoysticksUse;
        private Game _game;

        public void Construct(string sceneName, bool isJoysticksUse)
        {
            DontDestroyOnLoad(this);
            _sceneContext = GetComponent<SceneContext>();
            _startSceneName = sceneName;
            _isJoysticksUse = isJoysticksUse;
            RunSceneContext();
        }

        private void RunSceneContext() => _sceneContext.Run();

        [Inject]
        private void OnContextRun(IGameFactory gameFactory, IControllersMediator mediator)
        {
            _mediator = mediator;
            _mediator.IsJoysticksUse = _isJoysticksUse;
            _game = new Game(Instantiate(_loadingScreenPrefab), gameFactory, mediator);
            _game.Start(_startSceneName);
        }

        public void TryStartGame(string sceneName, bool isJoystickUse)
        {
            if(SceneManager.GetActiveScene().name == sceneName) return;
            _mediator.IsJoysticksUse = isJoystickUse;
            _game.Start(sceneName);
        }
    }
}