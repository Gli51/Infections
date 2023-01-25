using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
    public Player player;
    public InfectableCell[] cell;
    public GameObject gameOverUI;


    public Transform blueSpawn;
    public Transform redSpawn;

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
        Instantiate(player, blueSpawn.position, Quaternion.identity);
        player.infectedState = 1; // this is blue player
        Instantiate(player, redSpawn.position, Quaternion.identity);
        player.infectedState = 2; // this is red player
        
    }

}
