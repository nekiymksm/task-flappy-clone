using _project.Scripts.Observer;
using _project.Scripts.Ui.Base;
using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui
{
    public class PauseMenu : UiElement
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resume;
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exit;
        [SerializeField] private Transform _pauseView;

        private MainMenu _mainMenu;
        private GameObserver _gameObserver;
        
        private void Start()
        {
            _gameObserver = GameObserver.GetInstance();
            
            _pauseButton.onClick.AddListener(OnPauseButtonClick);
            _resume.onClick.AddListener(OnResumeButtonClick);
            _restart.onClick.AddListener(OnRestartButtonClick);
            _exit.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDestroy()
        {
            _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
            _resume.onClick.RemoveListener(OnResumeButtonClick);
            _restart.onClick.RemoveListener(OnRestartButtonClick);
            _exit.onClick.RemoveListener(OnExitButtonClick);
        }

        protected override void OnInit()
        {
            _mainMenu = UiRoot.GetUiElement<MainMenu>();
            _pauseView.gameObject.SetActive(false);
        }

        private void OnPauseButtonClick()
        {
            _pauseView.gameObject.SetActive(true);
            Time.timeScale = 0;
            
            _gameObserver.Notify(GameAction.ButtonPressed);
        }
        
        private void OnResumeButtonClick()
        {
            Time.timeScale = 1;
            _pauseView.gameObject.SetActive(false);
            
            _gameObserver.Notify(GameAction.ButtonPressed);
        }
        
        private void OnRestartButtonClick()
        {
            SceneManager.UnloadSceneAsync((int) LoadableScene.Game);
            SceneManager.LoadSceneAsync((int)LoadableScene.Game, LoadSceneMode.Additive);
            
            OnResumeButtonClick();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
            _gameObserver.Notify(GameAction.GameRestart);
        }
        
        private void OnExitButtonClick()
        {
            SceneManager.UnloadSceneAsync((int)LoadableScene.Game);
            
            OnResumeButtonClick();
            Hide();
            _mainMenu.Show();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
            _gameObserver.Notify(GameAction.GameEnd);
        }
    }
}