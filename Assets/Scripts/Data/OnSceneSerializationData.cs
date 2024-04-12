using System.Collections.Generic;
using MyGame.General.Data;
using UnityEngine;

namespace MyGame.Data
{
    public sealed class OnSceneSerializationData : MonoBehaviour, ISceneSerializedData
    {
        [field: SerializeField] public List<GameObject> OnSceneTriggers { get; private set; }
        
        public void Initialize(List<GameObject> onSceneTriggers)
        {
            OnSceneTriggers = onSceneTriggers;
        }
    }
}