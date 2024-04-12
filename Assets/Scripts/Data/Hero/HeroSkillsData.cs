using System;

namespace MyGame.Data.Hero
{
    [Serializable]
    public struct HeroSkillsData
    {
        public string[] Names;
        public HeroSkillsData(string[] names)
        {
            Names = names;
        }
    }
}