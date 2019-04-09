using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePowerupMechanics : MonoBehaviour
{
    public List<GameObject> Mechanics;

    // Update is called once per frame
    void Update()
    {
        if (PowerupSystem.IsCurrentPowerup(PowerupSystem.Powerups.Fire))
        {
            DisableAll();
            Mechanics[0].SetActive(true);
        }
        else if (PowerupSystem.IsCurrentPowerup(PowerupSystem.Powerups.Vortex))
        {
            DisableAll();
            Mechanics[1].SetActive(true);
        }
        else if (PowerupSystem.IsCurrentPowerup(PowerupSystem.Powerups.Magnet))
        {
            DisableAll();
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
