using UnityEngine;
using UnityEngine.UIElements;

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
    }

    public void Move()
    {
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


private void Update()
    {
        //aiming arrow follows mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        aimer.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (canMove == true)
        {
            aimer.SetActive(true);
        }

    }


  private void OnCollisionEnter2D(Collision2D collision)
  {
  }

}
