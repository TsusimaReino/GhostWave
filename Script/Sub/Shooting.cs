﻿using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int _damagePerShot = 20;
    public float _timeBetweenBullets = 0.15f;
    public float _range = 100f;


    float _timer;
    Ray _shootRay = new Ray();
    RaycastHit _shootHit;
    int _shootableMask;
    ParticleSystem _gunParticles;
    LineRenderer _gunLine;
    AudioSource _gunAudio;
    Light _gunLight;
    float _effectsDisplayTime = 0.2f;


    void Awake()
    {
        _shootableMask = LayerMask.GetMask("Shootable");
        _gunParticles = GetComponent<ParticleSystem>();
        _gunLine = GetComponent<LineRenderer>();
        _gunAudio = GetComponent<AudioSource>();
        _gunLight = GetComponent<Light>();
    }
    void Update ()
    {
        _timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && _timer >= _timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(_timer >= _timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        _gunLine.enabled = false;
        _gunLight.enabled = false;
    }


    void Shoot ()
    {
        _timer = 0f;

        _gunAudio.Play ();

        _gunLight.enabled = true;

        _gunParticles.Stop ();
        _gunParticles.Play ();

        _gunLine.enabled = true;
        _gunLine.SetPosition (0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        if(Physics.Raycast (_shootRay, out _shootHit, _range, _shootableMask))
        {
            EnemyHealth enemyHealth = _shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage (_damagePerShot, _shootHit.point);
            }
            _gunLine.SetPosition (1, _shootHit.point);
        }
        else
        {
            _gunLine.SetPosition (1, _shootRay.origin + (_shootRay.direction * _range));
        }
    }
}