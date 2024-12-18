using General;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = nameof(CameraData), menuName = "ProjectData/" + nameof(CameraData))]
    public sealed class CameraData: ScriptableObject, ICameraData
    {
        [SerializeField] private float _cameraFarSize = 8f;
        [SerializeField] private float _cameraNearSize = 2f;
        public float CameraFarSize => _cameraFarSize;
        public float CameraNearSize => _cameraNearSize;
    }
}