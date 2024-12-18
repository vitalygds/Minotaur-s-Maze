using General;

namespace Core
{
    internal sealed class BootstrapState : ILoadingState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private string _sceneKey;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(string sceneKey)
        {
            _sceneKey = sceneKey;
            _sceneLoader.Load(SceneNames.Loading, EnterSceneLoadingState);
        }

        private void EnterSceneLoadingState() => _stateMachine.LoadScene(_sceneKey);

        public void Exit() {}
    }
}