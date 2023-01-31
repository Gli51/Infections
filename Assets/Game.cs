using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
  public Player bluePlayer;
  public Player redPlayer;
  public Obstacles obstaclePrefab; // act as a prefab
  public List<Obstacles> obstacles; // tracking all obstacles
  public InfectableCell cellPrefab; // act as a prefab
  public List<InfectableCell> cells; // tracking all cells
  public GameObject gameOverUI;
  private string winner;

  //red and green colors
  private Color brightGreen;
  private Color brightRed;

  //blue and red spawn locations
  public Vector3 blueSpawn;
  public Vector3 redSpawn;

  //player scores
  public int blueScore = 0;
  public int redScore = 0;

  //UI text
  public Text blueScoreText;
  public Text redScoreText;
  public Text winnerText;
  public Text turnText;

  public const int TOTAL_CELLS = 10;
  public const int TOTAL_OBSTACLES = 10;
  public const float TURN_WAIT_TIME = 1.0f; // 1 second
  public const float STATIC_SPEED = 0.1f; // minimum speed that is considered static
  public const bool WAIT_TIL_STATIC = true;
  private float turnTimer = 0;

  //game starts on blue turn
  public GameState state = GameState.BLUETURN;

  //executed at the beginning
  private void Start()
  {
    blueScoreText.text = blueScore.ToString();
    redScoreText.text = redScore.ToString();
    gameOverUI.SetActive(false);
    Validate();

    //initialize color variables
    brightRed = new Color(254, 44, 69);
    brightGreen = new Color(81, 255, 192);

    // note that the order must be this way
    SpawnInfectableCells();
    SpawnObstacles();
    //sets blue player and red player aiming to start as blue turn
    bluePlayer.aimer.SetActive(true);
    redPlayer.GetComponent<SpriteRenderer>().color = Color.gray;
    redPlayer.aimer.SetActive(false);
  }


  private void SpawnInfectableCells()
  {
    // print debug
    cells = new List<InfectableCell>();
    // get half screen size
    float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    float halfHeight = Camera.main.orthographicSize;
    // spawn infectable cells
    for (int i = 0; i < TOTAL_CELLS; i++)
    {
      // generate cells within screen
      InfectableCell newCell = Instantiate(cellPrefab, new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), 0), Quaternion.identity);
      cells.Add(newCell);
      newCell.gameManager = this;
      while (newCell.isTouching(cells.Cast<Component>().ToList()))
      {
        newCell.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
      }
    }
  }

  private void SpawnObstacles()
  {
    obstacles = new List<Obstacles>();
    // get half screen size
    float halfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    float halfHeight = Camera.main.orthographicSize;
    // spawn infectable cells
    for (int i = 0; i < TOTAL_OBSTACLES; i++)
    {
      Obstacles newObstacles = Instantiate(obstaclePrefab, new Vector3(Random.Range(-halfWidth, halfWidth), Random.Range(-halfHeight, halfHeight), 0), Quaternion.identity);
      obstacles.Add(newObstacles);
      newObstacles.gameManager = this;
      while (newObstacles.isTouching(obstacles.Cast<Component>().ToList()) || newObstacles.isTouching(cells.Cast<Component>().ToList()))
      {
        newObstacles.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
      }
    }
  }

  private void Validate()
  {

    if (bluePlayer == null)
    {
      Debug.LogWarning("Blue player is not set");
    }
    if (redPlayer == null)
    {
      Debug.LogWarning("Red player is not set");
    }
  }

  private void Update()
  {
    turnTimer += Time.deltaTime;

    if (state == GameState.END)
    {
      winnerText.text = $"{winner} Wins!";
      gameOverUI.SetActive(true);
      return;
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    if (turnTimer < TURN_WAIT_TIME)
    {
      // show whose turn it is
      if (state == GameState.BLUETURN)
      {
        turnText.text = "Green Turn";
        turnText.color = brightGreen;

      }
      else if (state == GameState.REDTURN)
      {
        turnText.text = "Red Turn";
        turnText.color = brightRed;
      }
    }

    // if WAIT_TIL_STATIC is true, then wait until both players are not moving
    if (WAIT_TIL_STATIC && (bluePlayer.rb.velocity.magnitude > STATIC_SPEED || redPlayer.rb.velocity.magnitude > STATIC_SPEED))
    {
      return;
    }

    //right now this is buggy. Maybe the GetMouseButtonUp should be inside the gamestate condition?
    if (Input.GetMouseButtonUp(0) && turnTimer > TURN_WAIT_TIME)
    {
      turnTimer = 0;

      //if blue turn, become red turn
      if (state == GameState.BLUETURN)
      {
        redPlayer.GetComponent<SpriteRenderer>().color = Color.white;
        bluePlayer.GetComponent<SpriteRenderer>().color = Color.gray;
        bluePlayer.canMove = true;
        redPlayer.aimer.SetActive(true);
        bluePlayer.Move();
        state = GameState.REDTURN; //set game state to red
      }
      else if (state == GameState.REDTURN)
      {
        bluePlayer.GetComponent<SpriteRenderer>().color = Color.white;
        redPlayer.GetComponent<SpriteRenderer>().color = Color.gray;
        redPlayer.canMove = true;
        bluePlayer.aimer.SetActive(true);
        redPlayer.Move();
        state = GameState.BLUETURN; //set game state to blue
      }
    }
  }

public void IncreaseScore(Player player)
{
    //increase personal score
    if (player == redPlayer)
    {
        redScore += 1;
        redScoreText.text = redScore.ToString();
    }
    if (player == bluePlayer)
    {
        blueScore += 1;
        blueScoreText.text = blueScore.ToString();
    }
    //checks if any player has the full score
    if (redScore == TOTAL_CELLS)
    {
        winner = "Red Player";
        state = GameState.END;
    }
    else if (blueScore == TOTAL_CELLS)
    {
        winner = "Blue Player";
        state = GameState.END;
    }
}

  public void DecreaseScore(Player player)
  {
        //decrease score of player
        if (player == redPlayer)
        {
            redScore -= 1;
            redScoreText.text = redScore.ToString();
        }
        if (player == bluePlayer)
        {
            blueScore -= 1;
            blueScoreText.text = blueScore.ToString();
        }
    }
}
