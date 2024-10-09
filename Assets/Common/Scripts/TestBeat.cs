using SuperMaxim.Messaging;

namespace Common.Scripts
{
    using UnityEngine;

    public struct OnDetectBeatPayload
    {
        public float BeatVal;
    }
    public class TestBeat : MonoBehaviour
    {

        void Start()
        {
            //Select the instance of AudioProcessor and pass a reference
            //to this object
            AudioProcessor processor = FindObjectOfType<AudioProcessor>();
            processor.onBeat.AddListener(OnOnBeatDetected);
            // processor.onSpectrum.AddListener(onSpectrum);
        }

        //this event will be called every time a beat is detected.
        //Change the threshold parameter in the inspector
        //to adjust the sensitivity
        void OnOnBeatDetected()
        {
            float beat = Random.Range(0f,1f);
            Messenger.Default.Publish(new OnDetectBeatPayload
            {
                BeatVal = beat,
            });
            Debug.Log("Beat!!!");
        }

        //This event will be called every frame while music is playing
        void onSpectrum(float[] spectrum)
        {
            //The spectrum is logarithmically averaged
            //to 12 bands

            for (int i = 0; i < spectrum.Length; ++i)
            {
                Vector3 start = new Vector3(i, 0, 0);
                Vector3 end = new Vector3(i, spectrum[i], 0);
                Debug.DrawLine(start, end);
            }
        }
    }
}
