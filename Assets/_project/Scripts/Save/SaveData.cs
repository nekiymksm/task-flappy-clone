using System;

namespace _project.Scripts.Save
{
    [Serializable]
    public class SaveData
    {
        public int difficultyLevel;
        public bool isMusicOn;
        public bool isSoundsOn;
        public int lastRecord;
        public int lastDifficultyLevel;
    }
}