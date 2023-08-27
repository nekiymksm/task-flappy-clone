using _project.Scripts.Data;
using _project.Scripts.Observer;
using _project.Scripts.Save;
using _project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace _project.Scripts.Audio
{
    public class AudioController : MonoBehaviour, IObserver
    {
        [SerializeField] private AudioControllerConfig _audioControllerConfig;
        [SerializeField] private AudioMixerGroup _soundsGroup;
        [SerializeField] private AudioMixerGroup _musicGroup;

        private ItemsPool<AudioItem> _audioItemsPool;
        private AudioItem _currentMusicItem;
        private SaveManager _saveManager;
        private bool _isSoundsEnable;
        private bool _isMusicEnable;

        private void Awake()
        {
            _saveManager = SaveManager.GetInstance();
            
            _isSoundsEnable = _saveManager.SaveData.isSoundsOn;
            _isMusicEnable = _saveManager.SaveData.isMusicOn;
        }

        private void Start()
        {
            _audioItemsPool = new ItemsPool<AudioItem>(_audioControllerConfig.ItemPrefab, _audioControllerConfig.StartItemsCount, transform);
            PlayMusic(MusicTrack.Menu);
        }

        private void PlaySound(SoundEffect soundEffect)
        {
            if (_isSoundsEnable)
            {
                _audioItemsPool.GetItem().Play(_audioControllerConfig.Sounds[(int)soundEffect], _soundsGroup);
            }
        }
        
        private void PlayMusic(MusicTrack musicTrack)
        {
            if (_isMusicEnable)
            {
                _currentMusicItem = _audioItemsPool.GetItem();
                _currentMusicItem.Play(_audioControllerConfig.Music[(int)musicTrack], _musicGroup, true);
            }
        }

        private void StopMusic()
        {
            if (_isMusicEnable)
            {
                _currentMusicItem.Stop();
            }
        }

        public void React(GameAction gameAction)
        {
            switch (gameAction)
            {
                case GameAction.ButtonPressed:
                    PlaySound(SoundEffect.ButtonPressed);
                    break;
                case GameAction.CharacterJump:
                    PlaySound(SoundEffect.CharacterJump);
                    break;
                case GameAction.CharacterCollide:
                    PlaySound(SoundEffect.CharacterCollide);
                    break;
                case GameAction.GameStart:
                    StopMusic();
                    PlayMusic(MusicTrack.Level);
                    break;
                case GameAction.GameEnd:
                    StopMusic();
                    PlayMusic(MusicTrack.Menu);
                    break;
                case GameAction.DisableSounds:
                    _isSoundsEnable = false;
                    break;
                case GameAction.EnableSounds:
                    _isSoundsEnable = true;
                    break;
                case GameAction.DisableMusic:
                    StopMusic();
                    _isMusicEnable = false;
                    break;
                case GameAction.EnableMusic:
                    _isMusicEnable = true;
                    PlayMusic(MusicTrack.Menu);
                    break;
            }
        }
    }
}