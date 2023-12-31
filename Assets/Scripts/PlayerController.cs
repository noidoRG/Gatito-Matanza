using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public TextMeshProUGUI collectedText;
    public static int collectedAmount = 0;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    Rigidbody2D rigidBody2D;
    Animator animator;
    Vector2 movementInput;
    Vector2 smoothedMovementInput;
    Vector2 movementInputSmoothedVelocity;
    bool isWalking;

    void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput, movementInput, ref movementInputSmoothedVelocity, 0.1f);
        rigidBody2D.velocity = smoothedMovementInput * speed;

        // ��������� ���������� ��������
        animator.SetFloat("Horizontal", smoothedMovementInput.x);
        animator.SetFloat("Vertical", smoothedMovementInput.y);
        animator.SetBool("isWalking", isWalking);
    }

    void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();

        if (movementInput != Vector2.zero)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    void Update()
    {
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float shootHorizontal = Input.GetAxis("ShootHorizontal");
        float shootVertical = Input.GetAxis("ShootVertical");

        if ((shootHorizontal != 0 || shootVertical != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHorizontal, shootVertical);
            lastFire = Time.time;
        }

        collectedText.text = "Items Collected: " + collectedAmount;
    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
        if (bulletPrefab != null) { }
    }
}
