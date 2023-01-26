using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
  public Player bluePlayer;
  public Player redPlayer;
  public InfectableCell cellPrefab; // act as a prefab
  public InfectableCell[] cells; // tracking all cells
  public GameObject gameOverUI;
  private string winner;


  public Vector3 blueSpawn;
  public Vector3 redSpawn;

  public int blueScore = 0;
  public int redScore = 0;

  public Text blueScoreText;
  public Text redScoreText;

  public GameState state = GameState.BLUETURN;

  //executed at the beginning
  private void Start()
  {
        blueScoreText.text = blueScore.ToString();
        redScoreText.text = redScore.ToString();
        gameOverUI.SetActive(false);
        Validate();
        SpawnInfectableCells();

  }


  private void SpawnInfectableCells()
  {
    // print debug
    Debug.Log("Spawning cells");
    cells =  new InfectableCell[10];
    // spawn infectable cells
    for (int i = 0; i < cells.Length; i++)
    {
      // print debug
      Debug.Log("Spawning cell " + i);
      cells[i] = Instantiate(cellPrefab, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0), Quaternion.identity);
      cells[i].gameManager = this;
      while (cells[i].isTouching(cells))
      {
        cells[i].transform.position = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 0);
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
      //gameOverUI.GetComponent(Text).Text = "{winner} Wins!";
      gameOverUI.SetActive(true);
    }

    if (Input.GetMouseButtonDown(0))
    {

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
    //if score = max number of cells, game.winner = this
    state = GameState.END;
  }
}
