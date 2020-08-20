using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    //reference to text, btn, panel, bird, medal and image
    [SerializeField]
    private Text scoreText, endScore, bestScore, gameOverText;
    [SerializeField]
    private Button restartGameButton, instructionButton;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject[] birds;
    [SerializeField]
    private Sprite[] medals;
    [SerializeField]
    private Image medalImg;

    void Awake()
    {
        MakeInstance();
        Time.timeScale = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void PauseGame()
    {
        //if the game has started
        if (BirdScript.instance != null)
        {
            //if our bird is alive
            if (BirdScript.instance.isAlive)
            {
                pausePanel.SetActive(true);
                gameOverText.gameObject.SetActive(false);
                endScore.text = "" + BirdScript.instance.score;
                bestScore.text = "" + GameController.instance.GetHighScore();

                //stop all the animation
                Time.timeScale = 0f;
                restartGameButton.onClick.RemoveAllListeners();
                restartGameButton.onClick.AddListener(() => ResumeGame());
            }
        }
    }

    public void GoToMenuButton()
    {
        SceneFader.instance.FadeIn("MainMenu");
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; //to continue the game
    }

    public void RestartGame()
    {
        SceneFader.instance.FadeIn(Application.loadedLevelName);
    }

    public void PlayGame()
    {
        scoreText.gameObject.SetActive(true);
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        instructionButton.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    public void PlayerDiedShowScore(int score)
    {
        pausePanel.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);

        endScore.text = "" + score;

        if (score > GameController.instance.GetHighScore())
        {
            GameController.instance.SetHighScore(score);
        }

        bestScore.text = "" + GameController.instance.GetHighScore();

        //score < 20: blue bird & white medal
        //20 < score < 40: green bird unlocked & bronze medal
        //score > 40: red bird unlocked & gold medal
        if (score <= 20)
        {
            medalImg.sprite = medals[0];
        }
        else if (score>20 && score < 40)
        {
            medalImg.sprite = medals[1];
        }
        else
        {
            medalImg.sprite = medals[2];
        }

        //Unlock bird
        if (GameController.instance.GetHighScore()> 20 && GameController.instance.GetHighScore()< 40)
        {
            //Debug.Log("unlock green bird");
            Debug.Log(GameController.instance.GetHighScore());
            if (GameController.instance.GreenBirdUnlocked() == 0)
            {
                GameController.instance.UnlockGreenBird();
            }
        }
        else if(GameController.instance.GetHighScore() > 40)
        {
            if (GameController.instance.GreenBirdUnlocked() == 0)
            {
                GameController.instance.UnlockGreenBird();
            }
            if (GameController.instance.RedBirdUnlocked() == 0)
            {
                GameController.instance.UnlockRedBird();
            }
        }

        restartGameButton.onClick.RemoveAllListeners();
        restartGameButton.onClick.AddListener(() => RestartGame());
    }
}
