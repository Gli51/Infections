using UnityEngine;

public class Player : MonoBehaviour
{
  public Rigidbody2D rb;

  // 0 = invalid player, 1 = blue player, 2 = red player
  public int infectedState;
  public Game game;
  public float maxSpeed;
  public Vector2 startPos, endPos, direction;
  public bool canMove;
  private Vector3 mousePos;
  private Camera mainCam;
  public GameObject aimer;

  private void Awake()
  {
    if (rb == null)
      rb = GetComponent<Rigidbody2D>();

    game = FindObjectOfType<Game>();
  }

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        aimer.SetActive(true);
    }

    public void Move()
    {
        ////sets movement
        //// get mouse position
        //Vector3 mousePos = Input.mousePosition;
        //// convert mouse position to world position
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        //// get direction from player to mouse position
        //Vector3 direction = (worldPos - transform.position);
        //Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
        //// apply force in direction of mouse
        //rb.AddForce(direction2D * maxSpeed);
        canMove = true;

    }
        

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        aimer.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        if (Input.GetMouseButtonDown(0) && canMove == true)
        {
            aimer.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0) && canMove == true)
        {
            print("mouse released");
            Vector3 mousePos = Input.mousePosition;
            // convert mouse position to world position
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            // get direction from player to mouse position
            Vector3 direction = (worldPos - transform.position);
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            rb.AddForce(direction * maxSpeed);
            canMove = false;
            aimer.SetActive(false);
        }

        //if (canMove == true)
        //{
        //    //show aiming line to mouse

        //    aimer.SetActive(true);
        //}


    }


  private void OnCollisionEnter2D(Collision2D collision)
  {
    // when player collide with obstacles, the player will bounce off the obstacles
    if (collision.gameObject.CompareTag("Asteroid"))
    {
      Vector3 normal = collision.contacts[0].normal;
      Vector3 direction = rb.velocity.normalized;
      Vector3 reflection = Vector3.Reflect(direction, normal);
      rb.velocity = reflection * maxSpeed;
    } else if (collision.gameObject.CompareTag("Bullet")) {
      Vector3 normal = collision.contacts[0].normal;
      Vector3 direction = rb.velocity.normalized;
      Vector3 reflection = Vector3.Reflect(direction, normal);
      rb.velocity = reflection * maxSpeed;
    } else if (collision.gameObject.CompareTag("Player")) {
      Vector3 normal = collision.contacts[0].normal;
      Vector3 direction = rb.velocity.normalized;
      Vector3 reflection = Vector3.Reflect(direction, normal);
      rb.velocity = reflection * maxSpeed;
    }
  }

}
