using _project.Scripts.Observer;
using _project.Scripts.Ui.Base;
using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui
{
    public class DefeatMenu : UiElement
    {
        [SerializeField] private Button _restart;
        [SerializeField] private Button _exit;
        
        private MainMenu _mainMenu;
        private GameObserver _gameObserver;
        
        private void Start()
        {
            _gameObserver = GameObserver.GetInstance();
            
            _restart.onClick.AddListener(OnRestartButtonClick);
            _exit.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDestroy()
        {
            _restart.onClick.RemoveListener(OnRestartButtonClick);
            _exit.onClick.RemoveListener(OnExitButtonClick);
        }

        protected override void OnInit()
        {
            _mainMenu = UiRoot.GetUiElement<MainMenu>();
        }

        protected override void OnShow()
        {
            Time.timeScale = 0;
        }

        protected override void OnHide()
        {
            Time.timeScale = 1;
        }

        private void OnRestartButtonClick()
        {
            Hide();
            
            SceneManager.UnloadSceneAsync((int) LoadableScene.Game);
            SceneManager.LoadSceneAsync((int)LoadableScene.Game, LoadSceneMode.Additive);
            
            _gameObserver.Notify(GameAction.ButtonPressed);
            _gameObserver.Notify(GameAction.GameRestart);
        }
        
        private void OnExitButtonClick()
        {
            Hide();
            _mainMenu.Show();
            
            SceneManager.UnloadSceneAsync((int)LoadableScene.Game);
            
            _gameObserver.Notify(GameAction.ButtonPressed);
            _gameObserver.Notify(GameAction.GameEnd);
        }
    }
}