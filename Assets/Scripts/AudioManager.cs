using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Rigidbody player;
    public Sound[] sounds;

    public static AudioManager singleton;

    private Sound m_windSound;

	private void Awake () {

        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}
    private void Start()
    {
        WindEffect();
    }

    private void FixedUpdate()
    {
        Debug.Log("player velocity magnitude = " + player.velocity.magnitude);

        if (player.velocity.magnitude < 1)
        {
            m_windSound.source.volume = 0.05f;
            m_windSound.source.pitch = 1;
        }
        else
        {
            m_windSound.source.volume = 0.025f + (player.velocity.magnitude / 40);
            m_windSound.source.pitch = 0.95f + (player.velocity.magnitude / 20);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    private void WindEffect()
    {
        m_windSound = Array.Find(sounds, sound => sound.name == "WindSound");
        if (m_windSound == null)
        {
            return;
        }
        m_windSound.source.Play();
    }
}
