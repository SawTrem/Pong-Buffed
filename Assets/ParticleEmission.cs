using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmission : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    public Action EmitParticleAction;

    private void EmitParticle()
    {
        _particleSystem.Play();
    }
    private void OnEnable()
    {
        EmitParticleAction += EmitParticle;
    }
    private void OnDisable()
    {
        EmitParticleAction -= EmitParticle;
    }
}
    
