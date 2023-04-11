using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] int healthBonus = 0;

    public int GetHealthBonus()
    { 
        return healthBonus; 
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }
}
