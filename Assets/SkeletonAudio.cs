using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAudio : MonoBehaviour
{

    private AudioSource SkeletonAudioSurce;
    [SerializeField] private AudioClip AxeSound;
    [SerializeField] private AudioClip DeathAndAwakeSound;
    [SerializeField] private AudioClip[] WalkingSound;
    private int RandomNumber;
    void Start()
    {
        SkeletonAudioSurce = GetComponent<AudioSource>();
        if(GetComponent<EnemyController>().EnemyIsAwake)
        {
            RandomNumber = Random.Range(0, WalkingSound.Length);
            SkeletonAudioSurce.clip = WalkingSound[RandomNumber];
            SkeletonAudioSurce.Play();
        }
    }

    void Update()
    {
        if (GetComponent<EnemyController>().EnemyIsAwake)
        {
            if(!IsInvoking("PlayWalkingSound"))
            {
                Invoke("PlayWalkingSound",SkeletonAudioSurce.clip.length);
            }
        }
    }

    void PlaySkeletonAxeSound()
    {
        SkeletonAudioSurce.clip = AxeSound;
        SkeletonAudioSurce.Play();
    }
    void PlayDeathAndAwakeSound()
    {
        SkeletonAudioSurce.clip = DeathAndAwakeSound;
        SkeletonAudioSurce.Play();
    }
    void PlayWalkingSound()
    {
        if (GetComponent<EnemyController>().EnemyIsAwake)
        {
            int x = RandomNumber;
            RandomNumber = Random.Range(0, WalkingSound.Length);
            if(RandomNumber==x)
            {
                if (RandomNumber!=WalkingSound.Length)
                {
                    RandomNumber++;
                }
                else
                {
                    RandomNumber--;
                }
            }
            SkeletonAudioSurce.clip = WalkingSound[RandomNumber];
            SkeletonAudioSurce.Play();
        }
    }
}
