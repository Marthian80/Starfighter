using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f,1f)] float shootingVolume = 1.0f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1.0f;

    [Header("Healing")]
    [SerializeField] AudioClip healingClip;
    [SerializeField] [Range(0f, 1f)] float healingVolume = 1.0f;

    [Header("Music")]
    [SerializeField] AudioClip menuMusicClip;
    [SerializeField] AudioClip levelOneMusicClip;

    AudioSource audioSource;

    static AudioPlayer instance;

    void Awake()
    {
        ManageSingleton();

        audioSource = FindObjectOfType<AudioSource>();

        //start game with menu music
        PlayMenuMusic();        
    }

    //void SceneManager_sceneLoaded(Scene scene, LoadSceneMode sceneMode)
    //{
    //    // Replacement variable (doesn't change the original audio source)
    //    AudioSource source = new AudioSource();

    //    switch (scene.name)
    //    {
    //        case "LevelOne":
    //            source.clip = musicClips[1];
    //            break;
    //        default:
    //            source.clip = musicClips[0];
    //            break;
    //    }

    //    // Only switch the music if it changed
    //    if (source.clip != audioSource.clip)
    //    {
    //        audioSource.enabled = false;
    //        audioSource.clip = source.clip;
    //        audioSource.enabled = true;
    //    }

    //}

    void ManageSingleton()
    {        
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayMenuMusic()
    {
        if (menuMusicClip != null && audioSource.clip != menuMusicClip)
        {            
            audioSource.Stop();
            audioSource.clip = menuMusicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayGameMusic()
    {
        if (levelOneMusicClip != null && audioSource.clip != levelOneMusicClip)
        {
            audioSource.Stop();
            audioSource.clip = levelOneMusicClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void PlayShootingClip()
    {
        if(shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
        }
    }

    public void PlayDamageClip()
    {
        if(damageClip != null)
        {
            AudioSource.PlayClipAtPoint(damageClip, Camera.main.transform.position, damageVolume);
        }
    }

    public void PlayHealingClip()
    {
        if(healingClip != null)
        {
            AudioSource.PlayClipAtPoint(healingClip, Camera.main.transform.position, healingVolume);
        }
    }
}
