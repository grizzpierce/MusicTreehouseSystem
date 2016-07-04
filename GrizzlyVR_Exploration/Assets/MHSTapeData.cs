using UnityEngine;
using System.Collections;

public class MHSTapeData : MonoBehaviour {


    public AudioClip m_TapeAudio;
    public string m_TrackName;
    public string m_CreatorName;
    public string m_AudioType;

    string m_AudioDuration;

    public AudioClip GetAudio() { return m_TapeAudio; }
    public string GetTrackName() { return m_TrackName; }
    public string GetCreatorName() { return m_CreatorName; }
    public string GetAudioType() { return m_AudioType; }
    public string GetAudioDuration() { return m_AudioDuration; }
}
