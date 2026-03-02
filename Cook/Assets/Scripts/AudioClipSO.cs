using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioClipSO:ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] fail;
    public AudioClip[] success;
    public AudioClip[] footstep;
    public AudioClip[] objectdrop;
    public AudioClip[] objectpick;
    public AudioClip[] stove;
    public AudioClip[] trash;
    public AudioClip[] fire;
}
