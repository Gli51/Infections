using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody2D rb;

  // 0 = invalid player, 1 = blue player, 2 = red player
  public int infectedState = 1;
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
      if (game.state == GameState.END)
      {
        return;
      }

      if (game.state == GameState.BLUETURN && infectedState == 2)
      {
        return;
      }

      if (game.state == GameState.REDTURN && infectedState == 1)
      {
        return;
      }
      // get mouse position
      Vector3 mousePos = Input.mousePosition;
      // convert mouse position to world position
      Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
      // get direction from player to mouse position
      Vector3 direction = (worldPos - transform.position).normalized;
      // apply force in direction of mouse
      rb.AddForce(direction * maxSpeed);

      if (infectedState == 1) // blue player
      {
        game.state = GameState.REDTURN;
        Debug.Log("Set it to red turn");
      }
      else if (infectedState == 2) // red player
      {
        game.state = GameState.BLUETURN;
        Debug.Log("Set it to blue turn");
      }
    }
  }

  private void OnEnable()
  {
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    // when player collide with obstacles, the player will bounce off the obstacles
    if (collision.gameObject.CompareTag("Obstacles"))
    {
      // we find the normal of the collision
      Vector3 normal = collision.contacts[0].normal;
      // we find the direction of the player
      Vector3 direction = rb.velocity.normalized;
      // we find the reflection of the direction
      Vector3 reflection = Vector3.Reflect(direction, normal);
      // we apply the reflection as the new direction
      rb.velocity = reflection * maxSpeed;
    }
  }

}
