using General;
using UnityEngine;

namespace Core
{
    internal sealed class GameController : MonoBehaviour, IGameController
    {
        private Controllers _controllers;

        void IGameController.Initialize(int controllersCapacity)
        {
            _controllers = new Controllers(controllersCapacity);
            DontDestroyOnLoad(this);
        }

        void IGameController.AddController<T>(IController controller) =>
            _controllers.Add(controller, typeof(T));

        void IGameController.Disable() => gameObject.SetActive(false);

        void IGameController.Enable() => gameObject.SetActive(true);

        void IGameController.DestroyControllers() => _controllers.Destroy();
        public void DestroyLogic() => _controllers.Remove<ILogicController>();

        private void Start() => _controllers.Start();

        private void Update() => _controllers.Update();

        private void FixedUpdate() => _controllers.FixedUpdate();

        private void LateUpdate() => _controllers.LateUpdate();
    }
}