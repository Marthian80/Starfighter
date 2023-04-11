using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int lives = 3;
    [SerializeField] int pointsValue = 10;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem healEffect;
    [SerializeField] GameObject player;
    [SerializeField] Vector3 startingPosition = new Vector3(0, -6.5f, 0);

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;


    int startingHealth;    

    public int GetCurrentHealth()
    {
        return health;
    }

    public int GetCurrentLives()
    {
        return lives;
    }

    public bool IsPlayer()
    {
        return isPlayer;
    }

    void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();  
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
        startingHealth = health;        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();
        PowerUp powerUp = collision.GetComponent<PowerUp>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            ShakeCamera();
            damageDealer.Hit();         
        }        
        else if (powerUp != null)
        {
            HealDamage(powerUp.GetHealthBonus());
            PlayHealEffect();
            powerUp.PickedUp();
        }
    }

    void HealDamage(int healingAmount)
    {
        if (health < startingHealth)
        {
            health = Mathf.Clamp(health += healingAmount,0,50);            
        }
    }

    void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0) 
        {            
            if(!isPlayer) 
            {
                scoreKeeper.ModifyScore(pointsValue);
                Destroy(gameObject);
            }
            else if (lives <= 0 && isPlayer)
            {
                if(scoreKeeper.GetPlayerName() != null)
                {
                    scoreKeeper.SaveHighScore();
                }                
                levelManager.LoadGameOver();
                Destroy(gameObject);
            }
            else
            {
                player.GetComponentInChildren<SpriteRenderer>().enabled = false;
                StartCoroutine(LoseLife());
                
            }            
        }
    }

    IEnumerator LoseLife()
    {
        yield return new WaitForSeconds(0.75f);
        Math.Max(lives--,0);
        health = startingHealth;                
        player.transform.position = startingPosition;
        player.GetComponentInChildren<SpriteRenderer>().enabled = true;        
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            audioPlayer.PlayDamageClip();
        }
    }

    void PlayHealEffect()
    {
        if (healEffect != null)
        {
            ParticleSystem instance = Instantiate(healEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
            audioPlayer.PlayHealingClip();
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
        }
    }
}
