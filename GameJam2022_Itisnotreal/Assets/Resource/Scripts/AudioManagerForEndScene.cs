using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerForEndScene : MonoBehaviour
{
    public Sounds[] sounds;

    private void Awake()
    {

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            

            s.source.rolloffMode = s.rollof;
        }
    }

    public void Stop(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.source.Stop();
                break;
            }
        }
    }

    public void Play(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                if (!s.source.isPlaying)
                {
                    if (s.source.loop)
                    {
                        s.source.Play();
                    }
                    else
                    {
                        s.source.PlayOneShot(s.source.clip);
                    }
                }
                break;
            }
        }
    }
}
