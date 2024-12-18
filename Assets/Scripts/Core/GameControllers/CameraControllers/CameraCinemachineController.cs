using Cinemachine;
using Data;
using UnityEngine;

namespace Core
{
    internal sealed class CameraCinemachineController
    {
        private readonly CinemachineVirtualCamera _camera;
        private readonly CameraData _cameraData;

        public CameraCinemachineController(CinemachineVirtualCamera camera)
        {
            _camera = camera;
            SetUpTransposer();
        }

        public void SwitchTarget(Transform target, float orthographicSize)
        {
            _camera.Follow = target;
            _camera.m_Lens.OrthographicSize = orthographicSize;
        }

        private void SetUpTransposer()
        {
            var transposer = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
            transposer.m_LookaheadTime = 1f;
            transposer.m_LookaheadSmoothing = 30f;
            transposer.m_DeadZoneWidth = 0.15f;
            transposer.m_DeadZoneHeight = 0.2f;
            transposer.m_SoftZoneWidth = 0.8f;
            transposer.m_SoftZoneHeight = 0.6f;
        }
    }
}