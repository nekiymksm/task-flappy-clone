using _project.Scripts.Data;
using _project.Scripts.Observer;
using _project.Scripts.Save;
using _project.Scripts.Ui.Base;
using _project.Scripts.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _project.Scripts.Ui
{
    public class MainMenu : UiElement
    {
        [SerializeField] private TextMeshProUGUI _lastDifficultyText;
        [SerializeField] private TextMeshProUGUI _lastCountText;
        [SerializeField] private Button _play;
        [SerializeField] private Button _settings;
        [SerializeField] private Button _exit;
        [SerializeField] private DifficultyLevelsConfig _difficultyConfigs;

        private PauseMenu _pauseMenu;
        private PointsValueView _pointsValueView;
        private SettingsMenu _settingsMenu;
        private GameObserver _gameObserver;
        private SaveManager _saveManager;

        private void Start()
        {
            _gameObserver = GameObserver.GetInstance();
            _saveManager = SaveManager.GetInstance();

            _play.onClick.AddListener(OnPlayButtonClick);
            _settings.onClick.AddListener(OnSettingsButtonClick);
            _exit.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDestroy()
        {
            _play.onClick.RemoveListener(OnPlayButtonClick);
            _settings.onClick.RemoveListener(OnSettingsButtonClick);
            _exit.onClick.RemoveListener(OnExitButtonClick);
        }

        protected override void OnInit()
        {
            _pauseMenu = UiRoot.GetUiElement<PauseMenu>();
            _pointsValueView = UiRoot.GetUiElement<PointsValueView>();
            _settingsMenu = UiRoot.GetUiElement<SettingsMenu>();
            
            Show();
        }

        protected override void OnShow()
        {
            _lastDifficultyText.SetText(_difficultyConfigs.Get(_saveManager.SaveData.lastDifficultyLevel).Name);
            _lastCountText.SetText(_saveManager.SaveData.lastRecord.ToString());
            
            _pauseMenu.Hide();
        }

        private void OnPlayButtonClick()
        {
            SceneManager.LoadSceneAsync((int)LoadableScene.Game, LoadSceneMode.Additive);
            
            Hide();
            _pauseMenu.Show();
            _pointsValueView.Show();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
            _gameObserver.Notify(GameAction.GameStart);
        }
        
        private void OnSettingsButtonClick()
        {
            _settingsMenu.Show();
            UiRoot.GetUiElement<ConversionDataView>().Hide();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
        }
        
        private void OnExitButtonClick()
        {
            _gameObserver.Notify(GameAction.ButtonPressed);
            
            Application.Quit();
        }
    }
}