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


  public Vector3 blueSpawn;
  public Vector3 redSpawn;

  public int blueScore = 0;
  public int redScore = 0;

  public Text blueScoreText;
  public Text redScoreText;
  public Text winnerText;

  public const int TOTALCELLS = 10;
  public const float TURN_WAIT_TIME = 1.0f; // 1 second
  public const bool WAIT_TIL_STATIC = true;
  private float turnTimer = 0;

  public GameState state = GameState.BLUETURN;

  //executed at the beginning
  private void Start()
  {
        blueScoreText.text = blueScore.ToString();
        redScoreText.text = redScore.ToString();
        gameOverUI.SetActive(false);
        Validate();
        SpawnInfectableCells();
        SpawnObstacles();
  }


  private void SpawnInfectableCells()
  {
    // print debug
    cells = new List<InfectableCell>(TOTALCELLS);
    // spawn infectable cells
    for (int i = 0; i < 10; i++)
    {
      InfectableCell newCell = Instantiate(cellPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
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
    // spawn infectable cells
    for (int i = 0; i < 10; i++)
    {
      Obstacles newObstacles = Instantiate(obstaclePrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
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


    if (state == GameState.END)
    {
      winnerText.text = $"{winner} Wins!";
      gameOverUI.SetActive(true);
    }

    if (Input.GetKeyDown(KeyCode.R))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    turnTimer += Time.deltaTime;

    // if WAIT_TIL_STATIC is true, then wait until both players are not moving
    if (WAIT_TIL_STATIC && (bluePlayer.rb.velocity.magnitude > 0.1f || redPlayer.rb.velocity.magnitude > 0.1f))
    {
      return;
    }

    if (Input.GetMouseButtonDown(0) && turnTimer > TURN_WAIT_TIME)
    {
      turnTimer = 0;

      //if blue turn, become red turn
      if (state == GameState.BLUETURN)
      {
        bluePlayer.Move();
        state = GameState.REDTURN; //set game state to red
      }
      else if (state == GameState.REDTURN)
      {
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
    if (redScore == TOTALCELLS)
    {
        winner = "Red Player";
        state = GameState.END;
    }
    else if (blueScore == TOTALCELLS)
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
