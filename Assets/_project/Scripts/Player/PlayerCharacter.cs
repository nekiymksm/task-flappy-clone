using _project.Scripts.Data;
using _project.Scripts.Gates;
using _project.Scripts.Observer;
using UnityEngine;

namespace _project.Scripts.Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private PlayerCharacterConfig _playerCharacterConfig;

        private GameObserver _gameObserver;

        private void Start()
        {
            _gameObserver = GameObserver.GetInstance();
            
            _input.Clicked += Jump;
        }

        private void Update()
        {
            transform.Translate(Vector2.right * _playerCharacterConfig.MoveSpeed * Time.deltaTime);
        }

        private void OnDestroy()
        {
            _input.Clicked -= Jump;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Wall wall))
            {
                _gameObserver.Notify(GameAction.CharacterCollide);
            }
            else if (col.TryGetComponent(out PointTrigger pointTrigger))
            {
                _gameObserver.Notify(GameAction.TakePoint);
            }
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _playerCharacterConfig.JumpForce, ForceMode2D.Impulse);
            _gameObserver.Notify(GameAction.CharacterJump);
        }
    }
}