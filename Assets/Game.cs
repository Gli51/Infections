using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Player player;
    public Asteroid asteroidPrefab;
    public ParticleSystem explosionEffect;
    public GameObject gameOverUI;

    public int score = 0;
    public int lives = 3;

    public int asteroidsPerWave = 3;
    public float spawnMargin = 1f;

    public Text scoreText;
    public Text livesText;

    public float respawnDelay = 2;
    public float respawnInvulnerability = 2;

    public AudioSource audioSource;
    public AudioClip smallExplosionSound;
    public AudioClip mediumExplosionSound;
    public AudioClip bigExplosionSound;


    //executed at the beginning
    private void Start()
    {
        //update the text
        scoreText.text = score.ToString();
        livesText.text = lives.ToString();

        gameOverUI.SetActive(false);
        SpawnPlayer();
        
    }

    //executed continuously
    private void Update()
    {
        //restart whole scene 0 lives and return pressed
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

   
    //called at the beginning and when the played dies
    public void SpawnPlayer()
    {
        player.gameObject.SetActive(true);
    }

    //called by the player
    public void PlayerDeath(Player player)
    {
        explosionEffect.transform.position = player.transform.position;
        explosionEffect.Play();

        //disable the game object until respawn
        player.gameObject.SetActive(false);

        //subtract lifes
        lives--;

        //visualize on the interface
        livesText.text = lives.ToString();

        //I need to play the sound from here because the spaceship and its audio source 
        //is being destroyed
        audioSource.PlayOneShot(bigExplosionSound, 1);

        //check game over condition
        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            //invoke calls a function by name after a delay of x seconds
            Invoke("SpawnPlayer", respawnDelay);
        }
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }


}
