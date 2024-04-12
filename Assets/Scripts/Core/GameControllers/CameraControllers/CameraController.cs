using Cinemachine;
using MyGame.General.Controller;
using MyGame.General.Data;
using MyGame.General.Extensions;
using MyGame.General.View;
using UnityEngine;

namespace MyGame.Core.GameControllers.CameraControllers
{
    internal sealed class CameraController : ICameraController
    {
        private readonly CameraCinemachineController _stateMachine;
        private readonly ICameraData _cameraData;

        public CameraController(Camera camera, ICameraData cameraData, IControllersMediator mediator)
        {
            _cameraData = cameraData;
            mediator.OnHeroCreateEvent += SetFollowTarget;
            camera.gameObject.GetOrAddComponent<CinemachineBrain>();
            CinemachineVirtualCamera virtualCamera = camera.gameObject.GetOrAddComponent<CinemachineVirtualCamera>();
            _stateMachine = new CameraCinemachineController(virtualCamera);
        }
        
        public void SetFollowTarget(Transform target) => _stateMachine.SwitchTarget(target, _cameraData.CameraFarSize);

        public void SetObserveTarget(Transform target) => _stateMachine.SwitchTarget(target, _cameraData.CameraNearSize);
    }
}