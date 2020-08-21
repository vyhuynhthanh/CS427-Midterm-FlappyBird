using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private const string HIGH_SCORE = "High Score";
    private const string SELECTED_BIRD = "Selected Bird";
    private const string GREEN_BIRD = "Green Bird";
    private const string RED_BIRD = "Red Bird";

    void Awake()
    {
        Debug.Log("Awake");
        MakeSingleton();
        GameStartedFirstTime();
        //Cmt this command when build
        //PlayerPrefs.DeleteKey("GameStartedFirstTime");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    //use singleton to generate only 1 game controller in each scene
    void MakeSingleton()
    {
        if (instance != null)
        {
            //if we already have an instance of this class, we'll destroy the game obj that is holding this script
            Destroy(gameObject);
        }
        else
        {
            //else, we'll make this class the instance and won't destroy this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //called in the first time to intitialize variable
    void GameStartedFirstTime()
    {
        //use player preference to store data
        //if player pref doesn't contain GameStartedFirstTime keyword: this is the first time
        if (!PlayerPrefs.HasKey("GameStartedFirstTime"))
        {

            Debug.Log("First time");
      
            PlayerPrefs.SetInt(HIGH_SCORE, 0);
            //value 0 for locked and 1 for unlocked (like true and false)
            PlayerPrefs.SetInt(SELECTED_BIRD,0);
            PlayerPrefs.SetInt(GREEN_BIRD, 0);
            PlayerPrefs.SetInt(RED_BIRD, 0);
            PlayerPrefs.SetInt("GameStartedFirstTime", 1);
        }
    }

    //set up getter and setter to manipulate high score and select bird
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }

    public void SetSelectedBird(int selectedBird)
    {
        PlayerPrefs.SetInt(SELECTED_BIRD, selectedBird);
    }

    public int GetSelectedBird()
    {
        return PlayerPrefs.GetInt(SELECTED_BIRD);
    }

    public void UnlockGreenBird()
    {
        PlayerPrefs.SetInt(GREEN_BIRD, 1);
    }

    public int GreenBirdUnlocked()
    {
        return PlayerPrefs.GetInt(GREEN_BIRD);
    }

    public void UnlockRedBird()
    {
        PlayerPrefs.SetInt(RED_BIRD, 1);
    }

    public int RedBirdUnlocked()
    {
        return PlayerPrefs.GetInt(RED_BIRD);
    }
}
