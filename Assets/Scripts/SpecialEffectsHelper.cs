using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelper : MonoBehaviour
{
    public GameObject StarsEffects(Vector3 position)
    {
        GameObject newParticleSystem = Instantiate(Resources.Load("Prefabs/PS_Stars", typeof(GameObject)), position, Quaternion.identity) as GameObject;
        newParticleSystem.GetComponent<ParticleSystem>().Play();
        return newParticleSystem;
    }
}