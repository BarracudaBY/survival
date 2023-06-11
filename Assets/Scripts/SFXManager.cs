using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    public AudioSource[] SoundFX;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySfx(int sfxToPlay)
    {
        SoundFX[sfxToPlay].Stop();
        SoundFX[sfxToPlay].Play();
    }

    public void PlaySfxPitched(int sfxToPlay)
    {
        SoundFX[sfxToPlay].pitch = Random.Range(.8f, 1.2f);

        PlaySfx(sfxToPlay);
    }
}
