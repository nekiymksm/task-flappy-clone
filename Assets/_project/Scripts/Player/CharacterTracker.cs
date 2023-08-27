using UnityEngine;

namespace _project.Scripts.Player
{
    public class CharacterTracker : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _character;

        private Transform _selfTransform;
        private Vector3 _currentPosition;

        private void Awake()
        {
            _selfTransform = transform;
        }

        private void Update()
        {
            _currentPosition = _selfTransform.position;
            _currentPosition.x = _character.transform.position.x;
            transform.position = _currentPosition;
        }
    }
}