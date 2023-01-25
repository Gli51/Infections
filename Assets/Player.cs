using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Game game;
    public float maxSpeed;
    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        game = FindObjectOfType<Game>();
    }


    private void Update()
    {
      // detect mouse input
      if (Input.GetMouseButtonDown(0))
      {
        // get mouse position
        Vector3 mousePos = Input.mousePosition;
        // convert mouse position to world position
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        // get direction from player to mouse position
        Vector3 direction = (worldPos - transform.position).normalized;
        // apply force in direction of mouse
        rb.AddForce(direction * maxSpeed);
      }
    }

    private void OnEnable()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
    }

}
