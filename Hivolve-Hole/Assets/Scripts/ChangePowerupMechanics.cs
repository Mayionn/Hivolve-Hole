using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerupMechanics : MonoBehaviour
{
    public List<GameObject> Mechanics;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PowerupSystem.GetCurrentPowerup(PowerupSystem.Powerups.Fire))
        {
            Mechanics[0].SetActive(true);
        }
        else if (PowerupSystem.GetCurrentPowerup(PowerupSystem.Powerups.Vortex))
        {
            Mechanics[1].SetActive(true);
        }
        else if (PowerupSystem.GetCurrentPowerup(PowerupSystem.Powerups.Magnet))
        {
            Mechanics[2].SetActive(true);
        }
        else
        {
            DisableAll();
        }
    }

    void DisableAll()
    {
        foreach (var obj in Mechanics)
        {
            obj.SetActive(false);
        }
    }
}
