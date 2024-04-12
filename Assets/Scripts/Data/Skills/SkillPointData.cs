using System;
using UnityEngine;

namespace MyGame.Data.Skills
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