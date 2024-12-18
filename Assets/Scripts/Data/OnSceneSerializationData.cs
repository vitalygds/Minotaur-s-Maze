using System.Collections.Generic;
using General;
using UnityEngine;

namespace Data
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