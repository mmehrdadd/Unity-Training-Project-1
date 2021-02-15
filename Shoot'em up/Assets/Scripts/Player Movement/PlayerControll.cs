﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    [SerializeField] float Max_y, Min_y;
    [SerializeField] private GameObject PlayerBullet;
    [SerializeField] private Transform shootPoint;

    public float attackTimer = 0.35f;
    private float current_AttackTimer;
    private bool canAttack;
    private Animator anim;


    void Start()
    {
        current_AttackTimer = attackTimer;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        Attack();
    }
    void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > current_AttackTimer) { canAttack = true; }
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Instantiate(PlayerBullet, shootPoint.transform.position, Quaternion.identity);
            canAttack = false;
            attackTimer = 0f;
        }
    }
    void PlayerMovement()
    {
        if (Input.GetAxis("Vertical") > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += Time.deltaTime * Speed;
            if (temp.y > Max_y)
            {
                temp.y = Max_y;
            }
            transform.position = temp;

        }

        else if (Input.GetAxis("Vertical") < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= Time.deltaTime * Speed;
            if (temp.y < Min_y)
            {
                temp.y = Min_y;
            }
            transform.position = temp;

        }
    }
     void Destroy()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy" || target.tag == "Enemy Bullet")
        {
            Debug.Log("player got hit");
            anim.Play("playerExplosion");
            //sound
            Invoke("Destroy", 1f);
        }
    }
}
