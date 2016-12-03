using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource sFXSource, musicSource;

    public AudioClip peg, collectPeg, button, menuMusic, gameMusic, winMusic, loseMusic;

    private static AudioManager _Instance;
    public static AudioManager Instance
    {
        get { return _Instance; }
    }
    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PlayMusic(menuMusic);
    }

    public void PlaySFX(AudioClip inclip)
    {
        sFXSource.PlayOneShot(inclip);
    }


    public void PlayMusic(AudioClip inclip)
    {
        musicSource.Stop();
        musicSource.loop = true;
        musicSource.clip = inclip;
        musicSource.Play();
    }

    public void Button()
    {
        PlaySFX(button);
    }

    public void PlayGameMusic()
    {
        PlayMusic(gameMusic);
    }

    public void PlayMenuMusic()
    {
        PlayMusic(menuMusic);
    }

}
