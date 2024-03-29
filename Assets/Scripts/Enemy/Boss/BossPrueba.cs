﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class BossPrueba : Enemy
{
    public Transform trPlayer;
    [SerializeField] float moveSpeed = 3.0f;
    private bool canMove = true;

    int fase = 1;

    private float waitTime;
    [SerializeField] float startWaitTime;

    private float iniMoveSpeed;
    [SerializeField] float dashTime;

    [SerializeField] private float bossAttack;
    //[SerializeField] GameObject bulletsFase1;
    //[SerializeField] GameObject bulletsFase2;
    [Header("balas semi")]
    [SerializeField] ParticleSystem bullets1Fase1;
    [SerializeField] ParticleSystem bullets2Fase1;
    [SerializeField] ParticleSystem bullets3Fase1;

    [Header("balas full")]
    [SerializeField] ParticleSystem bulletsFase2;
    
    [Header("explosion")]
    [SerializeField] ParticleSystem explosionParticulas;

    bool canDash = true;
    int lastFase;
    float cooldown;
    bool ranged = false;

    private int maxHealth;

    [SerializeField] GameObject punch;
    public Animator transition;
    public int transitionTime;

    bool melee = true;
    [SerializeField] int damage;

    float distancia;
    [Header("papa")]
    [SerializeField] private float recoveryTime;
    int iniDamage;
    
    bool explosion = true;
    bool speedBoost = true;
    NavMeshAgent agente;

    float lockPos = 0;
    void Start()
    {
        //trPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        iniMoveSpeed = moveSpeed;
        waitTime = startWaitTime;
        maxHealth = health;
        bullets1Fase1.Stop();
        bullets2Fase1.Stop();
        bullets3Fase1.Stop();
        bulletsFase2.Stop();
        iniDamage = damage;
        //agente = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //agente.SetDestination(trPlayer.position);
        distancia = Vector3.Distance(transform.position, trPlayer.position);

        if (canMove == true)
        {
            transform.LookAt(trPlayer);
            transform.rotation = Quaternion.Euler(lockPos, transform.rotation.eulerAngles.y, lockPos);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        if (health <= 0)
        {
            Debug.Log("boss muerto");
            Die();
        }
        /*
        if (distancia <= 0.2f)
        {
            transform.position = trPlayer.position - transform.position;
        }
        */

        switch (fase)
        {
            case 1:
                //dash
                if(lastFase != fase)
                {
                    lastFase = fase;
                }
                if (canDash)
                {
                    waitTime -= Time.deltaTime;
                    if (waitTime <= 0)
                    {
                        Debug.Log("dash");
                        StartCoroutine(Dash());
                        waitTime = startWaitTime;
                    }
                }
                break;
            case 2:
                //ranged1
                if (lastFase != fase)
                {
                    lastFase = fase;
                }
                //Debug.Log("ranged1");
                RangedFase1();
                break;
            case 3:
                //bullet1
                if (lastFase != fase)
                {
                    lastFase = fase;
                }
                //Debug.Log("bullet1");
                break;
            case 4:
                //raned2
                if (lastFase != fase)
                {
                    lastFase = fase;
                }
                //Debug.Log("ranged2");
                RangedFase2();
                break;
            case 5:
                //bullet2
                if (lastFase != fase)
                {
                    lastFase = fase;
                }
                //Debug.Log("bullet2");
                break;
            case 6:
                switch (lastFase)
                {
                    case 1:
                        if (WaitTime())
                        {
                            //fase = 2;
                            if (health <= maxHealth / 2)
                            {
                                fase = 4;
                            }
                            else
                            {
                                fase = 2;
                            }
                        }
                        break;
                    case 2:
                        if (WaitTime())
                        {
                            fase = 3;
                            StartCoroutine(BulletsFase1());
                        }
                        break;
                    case 3:
                        if (WaitTime())
                        {
                            fase = 1;
                        }
                        break;
                    case 4:
                        if (WaitTime())
                        {
                            fase = 5;
                            StartCoroutine(BulletsFase2());
                        }
                        break;
                    case 5:
                        if (WaitTime())
                        {
                            fase = 1;
                        }
                        break;

                }
                break;
            default:
                fase = 1;
                break;
        }
    }

    bool WaitTime()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return false;
        }
        return true;
    }

    public IEnumerator Dash()
    {
        moveSpeed = 20;
        canDash = false;

        yield return new WaitForSeconds(dashTime);

        moveSpeed = iniMoveSpeed;
        canDash = true;

        cooldown = 1;
        fase = 6;
    }

    void RangedFase1()
    {
        Instantiate(punch, transform.position, transform.rotation);
        cooldown = 1;
        fase = 6;
    }

    public IEnumerator BulletsFase1()
    {
        melee = false;
        //bulletsFase1.SetActive(true);
        bullets1Fase1.Play();
        bullets2Fase1.Play();
        bullets3Fase1.Play();

        yield return new WaitForSeconds(bossAttack);

        bullets1Fase1.Stop();
        bullets2Fase1.Stop();
        bullets3Fase1.Stop();
        //bulletsFase1.SetActive(false);
        melee = true;
        cooldown = 1;
        fase = 6;
    }

    void RangedFase2()
    {
        if (speedBoost)
        {
            moveSpeed = 50;
            melee = false;
            speedBoost = false;
        }
        if (distancia <= 4f)
        {
            canMove = false;
            Debug.Log("moverse");
            if (explosion)
            {
                StartCoroutine(DashExplosion());
                explosion = false;
            }
        }
        else
        {
            Debug.Log("asdasho");
        }
    }

    public IEnumerator DashExplosion()
    {
        moveSpeed = iniMoveSpeed;
        Debug.Log("preexplosion");

        explosionParticulas.Play();

        yield return new WaitForSeconds(recoveryTime);

        melee = true;
        Debug.Log("explosion");
        canMove = true;
        damage = iniDamage;
        
        cooldown = 1;
        fase = 6;
    }

    public IEnumerator BulletsFase2()
    {
        melee = false;
        //bulletsFase2.SetActive(true);
        bulletsFase2.Play();
        Debug.Log("balas circulo");
        yield return new WaitForSeconds(bossAttack);

        bulletsFase2.Stop();
        //bulletsFase2.SetActive(false);
        melee = true;

        explosion = true;
        speedBoost = true;
        cooldown = 1;
        fase = 6;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!melee)
            {
                other.GetComponent<Player>().Damaged(damage);
                /*
                if (other.GetComponent<Invisibility>().canBeDamaged)
                {
                    other.GetComponent<Player>().Damaged(damage);
                }
                */
            }
        }
        
    }
    /*
    IEnumerator Hitted()
    {

        yield return new WaitForSeconds(0.1f);

    }
    */
    void Die()
    {

        LoadNextLevel();
        //Destroy(gameObject);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }


    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        //Debug.Log("se carga la sieguiente escena");
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(6);
    }

    IEnumerator Hitted()
    {
        //cuerpo.GetComponent<SkinnedMeshRenderer>().material = hitmat;
        yield return new WaitForSeconds(0.1f);
        //cuerpo.GetComponent<SkinnedMeshRenderer>().material = normalmat;
    }
}
