using UnityEngine;

namespace _project.Scripts.Data
{
    [CreateAssetMenu(fileName = "PlayerCharacterConfig", menuName = "Configs/PlayerCharacterConfig")]
    public class PlayerCharacterConfig : ScriptableObject
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _jumpForce;
        
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
    }
}