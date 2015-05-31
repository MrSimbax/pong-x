using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class HitSoundPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct HitSound
    {
        public AudioClip clip;
        public string tag;
    }
    
    public HitSound[] hitSounds;
    
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> sounds;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sounds = new Dictionary<string, AudioClip>();
        foreach(HitSound sound in hitSounds)
        {
            sounds[sound.tag] = sound.clip;
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (sounds.ContainsKey(col.gameObject.tag))
        {
            audioSource.clip = sounds[col.gameObject.tag];
            audioSource.Play();
        }
    }
	
}
