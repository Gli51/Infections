using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
  public Player bluePlayer;
  public Player redPlayer;
  public InfectableCell[] cell;
  public GameObject gameOverUI;


  public Vector3 blueSpawn;
  public Vector3 redSpawn;

  public int blueScore = 0;
  public int redScore = 0;

  public GameState state = GameState.BLUETURN;

  //executed at the beginning
  private void Start()
  {

    SetupGame();

  }

  private void SetupGame()
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
      gameOverUI.SetActive(true);
    }
  }
}
