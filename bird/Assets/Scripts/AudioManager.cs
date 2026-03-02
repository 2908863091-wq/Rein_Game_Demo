using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioClip title_theme;
    public AudioClip wood_c;
    public AudioClip wood_d;
    public AudioClip pig_c1;
    public AudioClip pig_c2;
    public AudioClip bird_fly;
    public AudioClip bird_select;
    public AudioClip bird_c;
    public AudioClip boom;

    private void Awake()
    {
        Instance = this;
    }
    public void Theme(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(title_theme, position, 1f);
    }
    public void Wood_c(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(wood_c, position, 0.3f);
    }
    public void Wood_d(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(wood_d, position, 0.3f);
    }
    public void Pig_c1(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(pig_c1, position, 1f);
    }
    public void Pig_c2(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(pig_c2, position, 1f);
    }
    public void Bird_fly(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(bird_fly, position, 1f);
    }
    public void Bird_select(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(bird_select, position, 1f);
    }
    public void Bird_c(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(bird_c, position, 1f);
    }
    public void Boom(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(boom, position, 1f);
    }
}
