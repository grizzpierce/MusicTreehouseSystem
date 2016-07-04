using UnityEngine;
using System.Collections;

public class MHSPlayer : MonoBehaviour {

    MHSPlayerReader m_Reader;
    public GameObject m_Speaker;
    AudioSource m_SpeakerAudio;

    bool m_IsAudioStarted = false;

	// Use this for initialization
	void Start ()
    {
        m_Reader = transform.FindChild("MHS Player Reader").gameObject.GetComponent<MHSPlayerReader>();
        if (m_Reader != null)
        {
            Debug.Log("Player Reader is set!");
        }

        m_SpeakerAudio = m_Speaker.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckMHSPlayer();
	}

    void CheckMHSPlayer()
    {
        // CHECKS IF THE READER HAS A TAPE INSIDE
        if (m_Reader.GetIfTapeInside() == true)
        {
            Debug.Log("Check 1 Complete: There's a tape!");

            if (m_Reader.GetIsGarbageInside() != true)
            {
                Debug.Log("Check 2 Complete: No garbage inside!");

                if (m_SpeakerAudio.clip == null)
                {
                    Debug.Log("Check 3 Complete: New audio added!");

                    m_SpeakerAudio.clip = m_Reader.GetCurrentTape().GetComponent<MHSTapeData>().GetAudio();
                    m_SpeakerAudio.Play();
                    Debug.Log("Now Playing " + m_Reader.GetCurrentTape().name);
                }

                else if (m_SpeakerAudio.clip.name == m_Reader.GetCurrentTape().GetComponent<MHSTapeData>().GetAudio().name && m_SpeakerAudio.isPlaying != true)
                {
                    Debug.Log("Check 3 Complete: Audio restarted!");

                    m_SpeakerAudio.clip = m_Reader.GetCurrentTape().GetComponent<MHSTapeData>().GetAudio();
                    m_SpeakerAudio.Play();
                    Debug.Log("Now Playing " + m_Reader.GetCurrentTape().name);
                }
            }

            else
            {
                m_SpeakerAudio.Pause();
            }
        }

        // IF NO TAPE INSIDE, STOP AUDIO IMMEDIATELY
        else
        {
            Debug.Log("No tape == no audio!");

            if (m_SpeakerAudio.isPlaying == true)
            {
                m_IsAudioStarted = false;
                m_SpeakerAudio.Stop();
                m_SpeakerAudio.clip = null;
                Debug.Log("No tape currently.");
            }

        }
    }
}
