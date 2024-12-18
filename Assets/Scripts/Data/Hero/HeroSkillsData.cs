using System;

namespace Data
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