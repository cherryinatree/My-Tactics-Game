using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : TriggerEvent
{
    public bool makesASound = true;
    public string soundEffectName;

    private AudioSource voice;
    private void Start()
    {

        voice = GetComponent<AudioSource>();
    }
    public override void TriggerMe()
    {
        Debug.Log("TrigerMe");
        gameObject.GetComponent<Animator>().SetTrigger("EventTrigger");

        if (makesASound) SoundEffect();
    }

    private void SoundEffect()
    {
        AudioClip audioClip = GamingTools.ResourseLoader.GetAudio("Sounds/EnviormentSoundEffects/" + soundEffectName);

        voice.clip = audioClip;
        voice.Play();
    }
}
