using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
    public Player player;
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
        Instantiate(player, blueSpawn, Quaternion.identity);
        player.infectedState = 1; // this is blue player
        Instantiate(player, redSpawn, Quaternion.identity);
        player.infectedState = 2; // this is red player
      Debug.Log("Blue player state: " + player.infectedState + " Red player state: " + player.infectedState);
        
    }

    private void Update()
    {
      // print both player's state
      Debug.Log("Blue player state: " + player.infectedState + " Red player state: " + player.infectedState);

    }

}
