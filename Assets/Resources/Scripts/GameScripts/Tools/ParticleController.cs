using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;

public static class ParticleController 
{

    public static void PlaceAttackParticles(GameObject onMe, string particleEffect, GameObject defender)
    {
        particleEffect = "Prefabs/ParticleEffects/" + particleEffect;
        GameObject particle = ResourseLoader.GetGameObject(particleEffect);
        GameObject aParticle = GameObject.Instantiate(particle, onMe.transform.position, Quaternion.identity);
        aParticle.GetComponent<ParticleMono>().defender = defender;
    }

}
