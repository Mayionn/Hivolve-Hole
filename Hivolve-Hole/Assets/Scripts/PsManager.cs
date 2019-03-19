using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem[] particleSystems;

    bool once = true;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void OneUpdate()
    {
        foreach (var ps in particleSystems)
        {
            ps.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (once)
        {
            OneUpdate();
            once = false;
        }

        Debug.Log(particleSystems[0].isPlaying);

        if (PowerupSystem.GetCurrentPowerup(PowerupSystem.Powerups.Fire))
        {
            if (!particleSystems[0].isPlaying)
            {
                particleSystems[0].Play(); // 0 is Fire
            }
        }
        else
        {
            particleSystems[0].Stop(withChildren: true);
        }

        if (PowerupSystem.GetCurrentPowerup(PowerupSystem.Powerups.Vortex))
        {
            if (!particleSystems[1].isPlaying)
            {
                particleSystems[1].Play(); // 1 is Vortex
            }
        }
        else
        {
            particleSystems[1].Stop(withChildren: true);
        }
    }
}
