//using UnityEditor.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    [Range(0f, 1f)]
    [Header("Указывает, зависит ли звук от пространства 0 - 2D")]
    public float spatialBlend;
    public AudioRolloffMode rollof;
    public bool loop, outside=true, IsTheme;

    public GameObject[] Characters;

    [HideInInspector]
    public AudioSource source;
    public float originalVolume;
}
