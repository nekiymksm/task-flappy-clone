using System.Collections;
using _project.Scripts.Data;
using _project.Scripts.Save;
using _project.Scripts.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _project.Scripts.Gates
{
    public class GatesCreator : MonoBehaviour
    {
        [SerializeField] private GatesCreatorConfig _gatesCreatorConfig;
        [SerializeField] private DifficultyLevelsConfig _difficultyLevelsConfig;

        private ItemsPool<Gate> _gatesPool;
        private DifficultyConfig _currentDifficultyConfig;
        private bool _isCreating;

        private void Awake()
        {
            _currentDifficultyConfig = _difficultyLevelsConfig.Get(SaveManager.GetInstance().SaveData.difficultyLevel);
        }

        private void Start()
        {
            _gatesPool = new ItemsPool<Gate>(_gatesCreatorConfig.GatePrefab, _gatesCreatorConfig.StartGatesCount, transform);
            
            _isCreating = true;
            StartCoroutine(Create());
        }

        private IEnumerator Create()
        {
            while (_isCreating)
            {
                var position = transform.position;
                position.y += Random.Range(-_currentDifficultyConfig.YAxisPositionRange, _currentDifficultyConfig.YAxisPositionRange);
                
                _gatesPool.GetItem().Set(position, _currentDifficultyConfig.DistanceBetweenWalls, _gatesCreatorConfig.GateLifetime);
                
                yield return new WaitForSeconds(_currentDifficultyConfig.TimeBetweenGatesCreating);
            }
        }
    }
}