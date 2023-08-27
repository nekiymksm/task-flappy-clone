using _project.Scripts.Audio;
using UnityEngine;

namespace _project.Scripts.Data
{
    [CreateAssetMenu(fileName = "AudioControllerConfig", menuName = "Configs/AudioControllerConfig")]
    public class AudioControllerConfig : ScriptableObject
    {
        [SerializeField] private AudioItem _itemPrefab;
        [SerializeField] private int _startItemsCount;
        [SerializeField] private AudioClip[] _sounds;
        [SerializeField] private AudioClip[] _music;
        
        public AudioItem ItemPrefab => _itemPrefab;
        public int StartItemsCount => _startItemsCount;
        public AudioClip[] Sounds => _sounds;
        public AudioClip[] Music => _music;
    }
}