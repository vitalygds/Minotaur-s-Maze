using UnityEngine;

namespace General
{
    public interface ICameraController : IController
    {
        void SetFollowTarget(Transform target);
        void SetObserveTarget(Transform target);
    }
}