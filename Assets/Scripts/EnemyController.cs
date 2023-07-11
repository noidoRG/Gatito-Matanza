using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Mellee,
    Ranged,
};

public class EnemyController : MonoBehaviour
{
    GameObject player;
    public EnemyState currentState = EnemyState.Wander;

    public EnemyType enemyType;
    public float range;
    public float speed;
    public float attackRange;
    public float coolDown;
    public GameObject bulletPrefab;



    private bool chooseDirection = false;
    //private bool dead = false;
    private bool coolDownAttack = false;
    private Vector3 randomDirection; // = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case EnemyState.Wander:
                Wander();
            break;
            
            case EnemyState.Follow:
                Follow();
            break;
            
            case EnemyState.Die:
                Death();
            break;
            
            case EnemyState.Attack:
                Attack();
            break;
        }

        if (IsPlayerInRange(range) && currentState != EnemyState.Die)
        {
            currentState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currentState != EnemyState.Die)
            currentState = EnemyState.Wander;
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
        }
    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDirection = true;
        yield return new WaitForSeconds(Random.Range(1f,4f));
         randomDirection = new Vector3(0,0,Random.Range(0,360));
        // Нужно для поворачивания врага, не знаю нужно ли сейчас
        Quaternion nextRotation = Quaternion.Euler(randomDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, Random.Range(0.5f, 2f));
        chooseDirection = false;
    }

    void Wander()
    {
        if (!chooseDirection)
        {
            StartCoroutine(ChooseDirection());
        }

        transform.position += speed * Time.deltaTime * -transform.right;
        
        if (IsPlayerInRange(range))
        {
            currentState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void Attack()
    {
        if (!coolDownAttack)
        {
            switch (enemyType) 
            {
                case EnemyType.Mellee:
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                break;

                case EnemyType.Ranged: 
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }
    }

    private IEnumerator CoolDown()
    {
        Debug.Log("cooldown init!!!");
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        Debug.Log("cooldown fin!!!");
        coolDownAttack = false;
    }

    public void Death()
    {
        Destroy(gameObject); 
    }

}
