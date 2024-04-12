using System.Collections.Generic;
using UnityEngine;

namespace MyGame.General.Data
{
    public interface ISceneSerializedData
    {
        List<GameObject> OnSceneTriggers { get; }
    }
}