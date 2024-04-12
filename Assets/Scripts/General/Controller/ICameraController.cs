using UnityEngine;

namespace MyGame.General.Controller
{
    public interface ICameraController : IController
    {
        void SetFollowTarget(Transform target);
        void SetObserveTarget(Transform target);
    }
}