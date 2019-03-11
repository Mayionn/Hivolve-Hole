using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerupSystem
{
    public enum Powerups
    {
        Fire,
        Vortex,
        Magnet,
        Duplicar,
        wallBreaking
    }

    static Powerups currentPowerup;

    static void ChangePowerup(Powerups tmp)
    {
        currentPowerup = tmp;
    }

    static Powerups GetPowerup()
    {
        return currentPowerup;
    }

    static void ChoosePowerup()
    {

    }
}
