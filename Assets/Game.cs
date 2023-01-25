using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameState { START, REDTURN, BLUETURN, END }

public class Game : MonoBehaviour
{
    public Player player;
    public Cell cell;
    public GameObject gameOverUI;


    public Transform playerSpawn;

    public int score = 0;
    public int score2 = 0;

    public bool redTurn = true;



    //executed at the beginning
    private void Start()
    {
        GameState.START;
        SetupGame();
        
    }

    private void SetupGame()
    {
        //Instantiate red and blue players
        
    }

}
