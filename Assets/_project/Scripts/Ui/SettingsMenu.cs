using _project.Scripts.Data;
using _project.Scripts.Observer;
using _project.Scripts.Save;
using _project.Scripts.Ui.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _project.Scripts.Ui
{
    public class SettingsMenu : UiElement
    {
        [SerializeField] private Button _previousDifficulty;
        [SerializeField] private Button _nextDifficulty;
        [SerializeField] private Button _exit;
        [SerializeField] private Toggle _music;
        [SerializeField] private Toggle _sounds;
        [SerializeField] private TextMeshProUGUI _difficultyText;
        [SerializeField] private DifficultyLevelsConfig _difficultyConfigs;

        private GameObserver _gameObserver;
        private SaveManager _saveManager;
        private int _currentDifficultyCounter;
        
        private void Start()
        {
            _gameObserver = GameObserver.GetInstance();

            _previousDifficulty.onClick.AddListener(OnPreviousDifficultyButtonClick);
            _nextDifficulty.onClick.AddListener(OnNextDifficultyButtonClick);
            _exit.onClick.AddListener(OnExitButtonClick);
            _music.onValueChanged.AddListener(OnMusicToggleChange);
            _sounds.onValueChanged.AddListener(OnSoundsToggleChange);
        }

        private void OnDestroy()
        {
            _previousDifficulty.onClick.RemoveListener(OnPreviousDifficultyButtonClick);
            _nextDifficulty.onClick.RemoveListener(OnNextDifficultyButtonClick);
            _exit.onClick.RemoveListener(OnExitButtonClick);
            _music.onValueChanged.RemoveListener(OnMusicToggleChange);
            _sounds.onValueChanged.RemoveListener(OnSoundsToggleChange);
        }

        protected override void OnInit()
        {
            _saveManager = SaveManager.GetInstance();
        }

        protected override void OnShow()
        {
            _currentDifficultyCounter = _saveManager.SaveData.difficultyLevel;
            SetDifficultyText();

            _music.isOn = _saveManager.SaveData.isMusicOn;
            _sounds.isOn = _saveManager.SaveData.isSoundsOn;
        }

        private void OnPreviousDifficultyButtonClick()
        {
            _currentDifficultyCounter--;

            if (_currentDifficultyCounter < 0)
            {
                _currentDifficultyCounter = _difficultyConfigs.DifficultyConfigs.Length - 1;
            }
            
            SetDifficultyText();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
        }
        
        private void OnNextDifficultyButtonClick()
        {
            _currentDifficultyCounter++;
            
            if (_currentDifficultyCounter > _difficultyConfigs.DifficultyConfigs.Length - 1)
            {
                _currentDifficultyCounter = 0;
            }
            
            SetDifficultyText();
            
            _gameObserver.Notify(GameAction.ButtonPressed);
        }
        
        private void OnExitButtonClick()
        {
            Hide();

            var saveData = _saveManager.SaveData;
            saveData.difficultyLevel = _currentDifficultyCounter;
            saveData.isMusicOn = _music.isOn;
            saveData.isSoundsOn = _sounds.isOn;
            
            _saveManager.Save(saveData);
            _gameObserver.Notify(GameAction.ButtonPressed);
        }

        private void OnMusicToggleChange(bool isOn)
        {
            _gameObserver.Notify(isOn ? GameAction.EnableMusic : GameAction.DisableMusic);
        }
        
        private void OnSoundsToggleChange(bool isOn)
        {
            _gameObserver.Notify(isOn ? GameAction.EnableSounds : GameAction.DisableSounds);
        }

        private void SetDifficultyText()
        {
            _difficultyText.SetText(_difficultyConfigs.Get(_currentDifficultyCounter).Name);
        }
    }
}