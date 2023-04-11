using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.25f;
    [SerializeField] float fireRateVariance = 1.5f;
    [SerializeField] bool useAI;
    [SerializeField] bool dualFireMode;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    public bool isFiring;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();        
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }        
    }

    IEnumerator FireContinously()
    {
        while (true) 
        {
            if (dualFireMode)
            {
                CreateFiringProjectile(projectilePrefab, new Vector3(transform.position.x -0.5f,transform.position.y));
                CreateFiringProjectile(projectilePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y));
            }
            else
            {
                CreateFiringProjectile(projectilePrefab, transform.position);
            }
            

            if(useAI)
            {
                firingRate = GetRandomShootTime();
            }

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(firingRate);
        }        
    }

    void CreateFiringProjectile(GameObject projectilePrefab, Vector3 position)
    {
        var projectile = Instantiate(projectilePrefab, position, Quaternion.identity);

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.up * projectileSpeed;
        }

        Destroy(projectile, projectileLifeTime);
    }

    float GetRandomShootTime()
    {
        float shootTime = Random.Range(firingRate - fireRateVariance, firingRate + fireRateVariance);

        return Mathf.Clamp(shootTime, firingRate, float.MaxValue);
    }
}
