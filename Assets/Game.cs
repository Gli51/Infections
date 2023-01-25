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
        //Instantiate red and blue players
        Instantiate(bluePlayer, blueSpawn, Quaternion.identity);
        bluePlayer.infectedState = 1; // this is blue player
        Instantiate(redPlayer, redSpawn, Quaternion.identity);
        redPlayer.infectedState = 2; // this is red player
        
    }

    private void Update()
    {
    }

}
