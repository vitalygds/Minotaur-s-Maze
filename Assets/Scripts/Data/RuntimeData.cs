using General;
using UnityEngine;

namespace Data
{ 
   [CreateAssetMenu(fileName = nameof(RuntimeData), menuName = "ProjectData/" + nameof(RuntimeData))]
   public sealed class RuntimeData: ScriptableObject, IRuntimeData
   {
       [field: SerializeField] public float HeroMovementSpeed { get; private set; } = 430f;
       [field: SerializeField] public float HeroDashTime { get; private set; } = 0.5f;
       [field: SerializeField] public float HeroDashCoolDownTime { get; private set; } = 2f;
       [field: SerializeField] public float HeroDashSpeedMultiplier { get; private set; } = 2f;
   }
}