using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SuperMaxim.Messaging;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class Header
    {
        [JsonProperty("PPQ")]
        public int PPQ;

        [JsonProperty("timeSignature")]
        public List<int> TimeSignature;

        [JsonProperty("bpm")]
        public double Bpm;

        [JsonProperty("name")]
        public string Name;
    }

    public class Note
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("midi")]
        public int Midi;

        [JsonProperty("time")]
        public double Time;

        [JsonProperty("velocity")]
        public double Velocity;

        [JsonProperty("duration")]
        public double Duration;
    }

    public class SongInformation
    {
        [JsonProperty("header")]
        public Header Header;

        [JsonProperty("tempo")]
        public List<Tempo> Tempo;

        [JsonProperty("timeSignature")]
        public List<TimeSignature> TimeSignature;

        [JsonProperty("startTime")]
        public double StartTime;

        [JsonProperty("duration")]
        public double Duration;

        [JsonProperty("tracks")]
        public List<Track> Tracks;
    }

    public class Tempo
    {
        [JsonProperty("absoluteTime")]
        public double AbsoluteTime;

        [JsonProperty("seconds")]
        public double Seconds;

        [JsonProperty("bpm")]
        public double Bpm;
    }

    public class TimeSignature
    {
        [JsonProperty("absoluteTime")]
        public double AbsoluteTime;

        [JsonProperty("seconds")]
        public double Seconds;

        [JsonProperty("numerator")]
        public int Numerator;

        [JsonProperty("denominator")]
        public int Denominator;

        [JsonProperty("click")]
        public int Click;

        [JsonProperty("notesQ")]
        public int NotesQ;
    }

    public class Track
    {
        [JsonProperty("startTime")]
        public double StartTime;

        [JsonProperty("duration")]
        public double Duration;

        [JsonProperty("length")]
        public double Length;

        [JsonProperty("notes")]
        public List<Note> Notes;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("channelNumber")]
        public int ChannelNumber;

        [JsonProperty("isPercussion")]
        public bool IsPercussion;
    }

    public struct OnDetectBeatPayload
    {
        public Note Note;
    }

    public class NoteDetectionSystem : MonoBehaviour
    {
        [SerializeField] private string _songJsonData;
        [SerializeField] private SongInformation _songInformation;
        [SerializeField] private float _delayPlaying = 1f;
        private async void Start()
        {
            _songInformation = JsonConvert.DeserializeObject<SongInformation>(_songJsonData);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_delayPlaying));
            StartCoroutine(DetectNote());
        }
        IEnumerator DetectNote()
        {
            foreach (Track track in _songInformation.Tracks)
            {
                for (int index = 0; index < track.Notes.Count; index++)
                {
                    Note preNote = new Note();
                    preNote = index == 0 ? track.Notes[0] : track.Notes[index - 1];
                    Note curNote = track.Notes[index];
                    Debug.Log("wait for " + (float)curNote.Time);
                    yield return new WaitForSeconds((float)(curNote.Time - preNote.Time));
                    Messenger.Default.Publish(new OnDetectBeatPayload
                    {
                        Note = curNote,
                    });
                }
            }

        }
    }

}
