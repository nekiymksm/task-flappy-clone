using _project.Scripts.Gates;
using UnityEngine;

namespace _project.Scripts.Data
{
    [CreateAssetMenu(fileName = "GatesCreatorConfig", menuName = "Configs/GatesCreatorConfig")]
    public class GatesCreatorConfig : ScriptableObject
    {
        [SerializeField] private Gate _gatePrefab;
        [SerializeField] private int _startGatesCount;
        [SerializeField] private float _gateLifetime;
        
        public Gate GatePrefab => _gatePrefab;
        public int StartGatesCount => _startGatesCount;
        public float GateLifetime => _gateLifetime;
    }
}