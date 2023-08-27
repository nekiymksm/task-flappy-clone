using _project.Scripts.Audio;
using _project.Scripts.Observer;
using _project.Scripts.Save;
using _project.Scripts.Ui.Base;
using _project.Scripts.Utilities;
using UnityEngine;

namespace _project.Scripts
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField] private UiRoot _uiRoot;
        [SerializeField] private AudioController _audioController;

        private GameObserver _gameObserver;
        private SaveManager _saveManager;
        private PointsCounter _pointsCounter;

        private void Awake()
        {
            _gameObserver = GameObserver.GetInstance();
            _saveManager = SaveManager.GetInstance();
            _pointsCounter = new PointsCounter();
        }

        private void Start()
        {
            _gameObserver.Add(_uiRoot);
            _gameObserver.Add(_audioController);
            _gameObserver.Add(_pointsCounter);
        }
    }
}