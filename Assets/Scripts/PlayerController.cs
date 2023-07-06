using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rigidBody2D.velocity = new Vector3(horizontal * speed, vertical * speed, 0);        
    }
}
