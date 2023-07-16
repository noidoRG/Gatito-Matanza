using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instanse;

    private static float health = 6;
    private static int maxHealth = 6;
    private static float moveSpeed = 5;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.2f;


    public TextMeshProUGUI healthText;

    public static float Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            KillPlayer(); 
        }

    }

    public static void HealPlayer(float healAmount) 
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    
    public static void FireRateChange(float rate) 
    {
        fireRate -= rate;
    }

    public static void MoveSpeedChange(float speed) 
    {
        moveSpeed += speed;
    }
    
    public static void BulletSizeChange(float size) 
    {
        bulletSize += size;
    }



    public static void KillPlayer()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
