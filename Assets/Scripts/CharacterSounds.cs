using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    public AudioClip stepAudio;
    public AudioClip jumpAudio;
    public AudioClip attackAudio;
    public AudioClip drownAudio;
    public AudioClip splashAudio;
    public AudioSource audioSource;

    public void StepAudio()
    {

        audioSource.PlayOneShot(stepAudio, 0.3f);

    }
    public void AttackAudio()
    {

        audioSource.PlayOneShot(attackAudio, 1f);

    }
    public void JumpAudio()
    {
        if (jumpAudio != null)
            audioSource.PlayOneShot(jumpAudio, 1f);

    }
    public void DrownAudio()
    {

        audioSource.PlayOneShot(drownAudio, 0.3f);

    }
    public void SplashAudio()
    {

        audioSource.PlayOneShot(splashAudio, 1f);

    }
}
