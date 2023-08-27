using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;

namespace _project.Scripts.Audio
{
    public class AudioItem : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip audioClip, AudioMixerGroup group, bool isLoop = false)
        {
            gameObject.SetActive(true);
            
            _audioSource.clip = audioClip;
            _audioSource.outputAudioMixerGroup = group;
            _audioSource.loop = isLoop;
            _audioSource.Play();

            if (isLoop == false)
            {
                AsyncDelayDisable();
            }
        }

        public void Stop()
        {
            _audioSource.Stop();
            gameObject.SetActive(false);
        }

        private async void AsyncDelayDisable()
        {
            await Task.Delay((int)(_audioSource.clip.length * 1000));
            Stop();
        }
    }
}