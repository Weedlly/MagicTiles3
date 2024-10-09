using SuperMaxim.Messaging;
using UnityEngine;

namespace Common.Scripts
{
    public struct AudioPlayOneShotPayload
    {
        public AudioClip AudioClip;
    }

    public struct AudioPlayLoopPayload
    {
        public AudioClip AudioClip;
    }

    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSourceLoop;
        [SerializeField] private AudioSource _audioSourceOneShot;
        private void Awake()
        {
            Messenger.Default.Subscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Subscribe<AudioPlayLoopPayload>(PlayLoop);
        }
        private void OnDestroy()
        {
            Messenger.Default.Unsubscribe<AudioPlayOneShotPayload>(PlayOneShot);
            Messenger.Default.Unsubscribe<AudioPlayLoopPayload>(PlayLoop);
        }
        private void PlayOneShot(AudioPlayOneShotPayload payload)
        {
            _audioSourceOneShot.PlayOneShot(payload.AudioClip);
        }
        private void PlayLoop(AudioPlayLoopPayload payload)
        {
            _audioSourceLoop.clip = payload.AudioClip;
            _audioSourceLoop.loop = true;
            _audioSourceLoop.Play();
        }
        // float[] spectrumData = new float[256]; // Tạo mảng chứa dữ liệu phổ tần số

        // void Update() {
        //     _audioSourceLoop.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);
        //     AnalyzeSpectrum(spectrumData);
        // }
        // void AnalyzeSpectrum(float[] spectrum) {
        //     // Xác định chỉ số tần số chính
        //     float maxFrequency = 0f;
        //     int maxIndex = 0;
        //
        //     // Tìm tần số có giá trị lớn nhất (đỉnh)
        //     for (int i = 0; i < spectrum.Length; i++) {
        //         if (spectrum[i] > maxFrequency) {
        //             maxFrequency = spectrum[i];
        //             maxIndex = i;
        //         }
        //     }
        //
        //     // Chuyển đổi chỉ số thành tần số thực tế
        //     float frequency = maxIndex * (float)AudioSettings.outputSampleRate / 2 / spectrum.Length;
        //     Debug.Log("Frequency: " + frequency);
        //     
        //     // Nếu tần số nằm trong khoảng nhịp nhạc, bạn có thể xử lý logic nhịp tại đây.
        // }
    }
}
