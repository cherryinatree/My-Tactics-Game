using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAnimate : MonoBehaviour
{

    private AudioSource voice;
    private Animator animator;

    public string[] animationTriggers;
    public AudioClip[] Sounds;


    private void Start()
    {
        voice = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTriggers();
    }

    public void CheckTriggers()
    {
        if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Idle_Battle")
        {
            CheckAllAudioTriggers();
        }
    }

    private void CheckAllAudioTriggers()
    {
        for (int i = 0; i < animationTriggers.Length; i++)
        {
            if (animator.GetBool(animationTriggers[i]))
            {
                voice.clip = Sounds[i];
                voice.Play();
            }
        }
    }

    public void PlaySound(AudioClip sound, string triggerName)
    {
        if (animator.GetBool(triggerName))
        {
            voice.clip = sound;
            voice.Play();
        }
    }
    public void PlaySound(int sound, string triggerName)
    {
        if (animator.GetBool(triggerName))
        {
            voice.clip = Sounds[sound];
            voice.Play();
        }
    }
    public void PlaySound(int sound)
    {
         voice.clip = Sounds[sound];
         voice.Play();
    }


}
