using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoader : MonoBehaviour
{
    [SerializeField] private AudioSO audioLoader;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        foreach (Sound s in audioLoader.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
}
