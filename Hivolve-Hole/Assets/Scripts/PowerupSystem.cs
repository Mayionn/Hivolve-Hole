using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PowerupSystem
{
    public enum Powerups
    {
        None,
        Fire,
        Vortex,
        Magnet,
        DoubleSize
    }

    public static Powerups currentPowerup = Powerups.None;

    public static void ChangePowerup(Powerups tmp)
    {
        currentPowerup = tmp;
    }

    public static bool GetCurrentPowerup(Powerups pw)
    {
        return currentPowerup == pw;
    }

    public static void ChoosePowerup(int index)
    {
        switch (index)
        {
            case 1:
                currentPowerup = Powerups.Fire;
                break;
            case 2:
                currentPowerup = Powerups.Vortex;
                break;
            case 3:
                currentPowerup = Powerups.Magnet;
                break;
            case 4:
                currentPowerup = Powerups.DoubleSize;
                break;
        }
    }
}
