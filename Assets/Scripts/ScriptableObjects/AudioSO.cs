using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioSO")]
public class AudioSO : ScriptableObject
{
    public List<Sound> sounds;

    public void Play(string soundName)
    {
        var sound = sounds.Find(sound => sound.name == soundName);
        if(sound == null)
        {
            Debug.LogWarning("Son " + soundName + " non trouvé !");
        }
        else
        {
            sound.source.Play();
        }
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}