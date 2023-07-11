using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instanse;

    private static int health = 10;
    private static int maxHealth = 10;
    private static float moveSpeed = 5;
    private static float fireRate = 0.5f;
    [SerializeField] 
    public TextMeshProUGUI healthText;

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

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
        //Debug.Log("Before hit: " +  health);
        health -= damage;
        if (health <= 0)
        {
            KillPlayer(); 
        }

    }

    public static void HealPlayer(int healAmount) 
    {
        health = Mathf.Min(maxHealth, health + healAmount);

    }
    public static void KillPlayer()
    {
        //Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
}
