using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct SkillPointData
    {
        public SkillType SkillType;
        public Vector2 Position;
    }

    public enum SkillType
    {
        DoubleJump,
        Slide,
    }
}