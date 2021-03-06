﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Laser : BaseAbility
{
    public override void UseSpell()
    {
        Debug.Log("Laser");
    }
    /*
    [SerializeField] GameObject rayo;

    private bool canShoot;
    [SerializeField] float shotDuration = 2;

    private bool isCooldown = false;
    private float cooldown;
    [SerializeField] float iniCooldown = 2;
    [SerializeField] Image imageCooldown;

    PlayerInput input;

    void Start()
    {
        //rayo = GameObject.Find("Laser");
        rayo = GameObject.FindGameObjectWithTag("Laser");
        //rayo.SetActive(false);
        imageCooldown.fillAmount = 0.0f;
    }

    private void Awake()
    {
        input = new PlayerInput();
        //input.CharacterControls.Ability1.performed += UseSpell;
    }

    void Update()
    {
        
        if (Input.GetKeyDown("4"))
        {
            UseSpell();
        }
        
        if (canShoot == true)
        {
            rayo.SetActive(true);
        }
        if (canShoot == false)
        {
            rayo.SetActive(false);
        }

        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        cooldown -= Time.deltaTime;

        if(cooldown < 0.0f)
        {
            isCooldown = false;
            imageCooldown.fillAmount = 0.0f;
        }
        else
        {
            imageCooldown.fillAmount = cooldown / iniCooldown;
        }
    }

    protected virtual void UseSpell()
    {
        if (isCooldown)
        {
            //StartCoroutine(Shot());
        }
        else
        {
            StartCoroutine(Shot());
            //isCooldown = true;
            cooldown = iniCooldown;
        }
    }

    private IEnumerator Shot()
    {
        canShoot = true;
        Debug.Log("on");
        yield return new WaitForSeconds(shotDuration);
        canShoot = false;
        isCooldown = true;
        //overlayCooldown.GetComponent<Image>().fillAmount = 0.5;
        Debug.Log("off");
    }

    public void NotifyAddedAtSlot(int slot)
    {
        enabled = true;
        //input = new PlayerInput();
        switch (slot)
        {
            case 0:
                input.CharacterControls.Ability1.performed += ctx => UseSpell();
                break;
            case 1:
                input.CharacterControls.Ability2.performed += ctx => UseSpell();
                break;
            case 2:
                input.CharacterControls.Ability3.performed += ctx => UseSpell();
                break;
            case 3:
                input.CharacterControls.Ability4.performed += ctx => UseSpell();
                break;
        }
    }

    private void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    private void OnDisable()
    {
        input.CharacterControls.Disable();
    }
    */
}
