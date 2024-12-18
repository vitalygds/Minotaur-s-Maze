using System.Collections.Generic;
using UnityEngine;

namespace General
{
    public interface ISceneSerializedData
    {
        List<GameObject> OnSceneTriggers { get; }
    }
}