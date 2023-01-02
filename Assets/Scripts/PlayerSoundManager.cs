using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [HideInInspector]public AudioSource PlayerAudioSource;


    [SerializeField] private AudioClip[] PlayerRunnig;
    [SerializeField] private AudioClip[] PlayerJump;
    [SerializeField] private AudioClip PlayerFirstAttack;
    [SerializeField] private AudioClip PlayerSecondAttack;
    [SerializeField] private AudioClip PlayerThirdAttack;
    [SerializeField] private AudioClip CastFireBall;
    [SerializeField] private AudioClip[] HittingEnemy;
    void Start()
    {
        PlayerAudioSource = GetComponent<AudioSource>();

       
    }

    void Update()
    {
    }

    public void PlayRunnigSound()
    {
        int x =Random.Range(0, PlayerRunnig.Length);
        PlayerAudioSource.clip = PlayerRunnig[x];
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1;
        PlayerAudioSource.Play();
    }
    void playJumpSoundd()
    {
        int x = Random.Range(0, PlayerJump.Length);
        PlayerAudioSource.clip = PlayerJump[x];
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1.15f;
        PlayerAudioSource.Play();
    }

    void PlayerFirstAttackSound()
    {
        PlayerAudioSource.clip = PlayerFirstAttack;
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1;
        PlayerAudioSource.Play();
    }

    void PlaySecondAttackSound()
    {
        PlayerAudioSource.clip = PlayerSecondAttack;
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1;
        PlayerAudioSource.Play();
    }
    void PlayThirdAttackSound()
    {
        PlayerAudioSource.clip = PlayerThirdAttack;
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1;
        PlayerAudioSource.Play();
    }

   /* public void PlayerHittingEnemy()
    {
        int x = Random.Range(0, HittingEnemy.Length);
        PlayerAudioSource.clip = HittingEnemy[x];
        PlayerAudioSource.spatialBlend = 0.9f;
        PlayerAudioSource.pitch = 1.15f;
        PlayerAudioSource.Play();
    }*/
    void PlayCastFireBallSound()
    {

    }
}
