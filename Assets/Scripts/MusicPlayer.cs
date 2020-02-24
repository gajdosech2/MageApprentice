using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audio_source;
    static bool already_exists = false;
    //GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();

    private void Awake()
    {
        if (already_exists)
        {
            Destroy(gameObject);
        }
        already_exists = true;
        DontDestroyOnLoad(transform.gameObject);
        audio_source = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (audio_source.isPlaying) return;
        audio_source.Play();
    }

    public void StopMusic()
    {
        audio_source.Stop();
    }
}
