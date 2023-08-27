using System;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Button _inputButton;

        public event Action Clicked;
        
        private void Start()
        {
            _inputButton.onClick.AddListener(OnInputButtonClick);
        }

        private void OnDestroy()
        {
            _inputButton.onClick.RemoveListener(OnInputButtonClick);
        }

        private void OnInputButtonClick()
        {
            Clicked?.Invoke();
        }
    }
}