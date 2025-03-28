using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public SpawnData spawnData;
    
    public void Initialize(SpawnData data)
    {
        spawnData = data;
        damage = data.damage;
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
        exp = data.creatureExp;
        dropGold = data.dropGold;
    }
    
    public Rigidbody target;
    public float speed;
    public float health;
    public float maxHealth;
    public int exp;
    public float damage = 100;
    public int dropGold;

    public float attackRange = 2f;
    public float attackDeley = 1f;

    private bool canAttack = true;
    private bool isLive;
    
    private PlayerController _player;
    private Rigidbody rigid;
    private WaitForFixedUpdate wait;
    private Animator anim;
    
    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); 
        rigid = GetComponent<Rigidbody>();
        wait = new WaitForFixedUpdate();
        anim = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if (!isLive)
            return;
        Vector3 dirVec = target.position - rigid.position;
        Vector3 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector3.zero;
        
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        if (distanceToTarget <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
            canAttack = false;
            StartCoroutine(ResetAttackState());
        }
        
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.transform.position.z));
        
    }
    
    private void OnEnable() 
    {
        target = GameManager.instance.player.GetComponent<Rigidbody>();
        isLive = true;
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!isLive)
        {
            return;
        }
        
        health -= (damage + GameDataManager.Instance.PlusStr);
        Debug.Log("애니메hp "+health);
        
        if (health > 0)
        {
            StartCoroutine(KnokBack());
        }
        else
        {
            Debug.Log("Die");
            Dead();
            GameManager.instance.kill++;
            GameDataManager.Instance.CurrentExp += exp;
            GameDataManager.Instance.HaveGold += dropGold;
        }
    }

    IEnumerator KnokBack()
    {
        yield return wait; // 1 frame delay
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 3, ForceMode.Impulse);
    }

    void Dead()
    {
        GameDataManager.Instance.KillScore++;
        gameObject.SetActive(false);
    }

    IEnumerator Attack()
    {
        Debug.Log("공격");
        // 실제 공격 코드
        _player.DamageAction(damage);
        yield return new WaitForSeconds(attackDeley);
    }

    IEnumerator ResetAttackState()
    {
        yield return new WaitForSeconds(attackDeley);
        canAttack = true;
    }
    
}