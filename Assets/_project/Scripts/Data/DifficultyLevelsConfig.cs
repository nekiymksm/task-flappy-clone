using System;
using UnityEngine;

namespace _project.Scripts.Data
{
    [CreateAssetMenu(fileName = "DifficultyLevelsConfig", menuName = "Configs/DifficultyLevelsConfig")]
    public class DifficultyLevelsConfig : ScriptableObject
    {
        [SerializeField] private DifficultyConfig[] _difficultyConfigs;

        public DifficultyConfig[] DifficultyConfigs => _difficultyConfigs;
        
        public DifficultyConfig Get(int currentDifficultyIndex)
        {
            return _difficultyConfigs[currentDifficultyIndex];
        }
    }
    
    [Serializable]
    public class DifficultyConfig
    {
        [SerializeField] private string _name;
        [SerializeField] private float _timeBetweenGatesCreating;
        [SerializeField] private float _yAxisPositionRange;
        [SerializeField] private float _distanceBetweenWalls;

        public string Name => _name;
        public float TimeBetweenGatesCreating => _timeBetweenGatesCreating;
        public float YAxisPositionRange => _yAxisPositionRange;
        public float DistanceBetweenWalls => _distanceBetweenWalls;
    }
}