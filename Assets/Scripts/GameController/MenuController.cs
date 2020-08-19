using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    [SerializeField]
    private GameObject[] birds;
    private bool greenBirdUnlocked, redBirdUnlocked;

    void Awake()
    {
        MakeInstane();
    }

    // Start is called before the first frame update
    void Start()
    {
        //activate the selected bird
        birds[GameController.instance.GetSelectedBird()].SetActive(true);
        CheckIfBirdsAreUnlocked();
    }

    void MakeInstane()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void CheckIfBirdsAreUnlocked()
    {
        if (GameController.instance.RedBirdUnlocked() == 1)
        {
            redBirdUnlocked = true;
        }
        if (GameController.instance.GreenBirdUnlocked() == 1)
        {
            greenBirdUnlocked = true;
        }
    }

    public void PlayGame()
    {
        SceneFader.instance.FadeIn("GamePlay");
    }

    public void ChangeBird()
    {
        //the blue bird is selected
        if (GameController.instance.GetSelectedBird() == 0)
        {
            //click once and select the green bird
            if (greenBirdUnlocked)
            {
                //de-select the blue bird
                birds[0].SetActive(false);
                //activate the green bird
                GameController.instance.SetSelectedBird(1);
                birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
        }
        //the green bird is currently selected
        else if(GameController.instance.GetSelectedBird() == 1)
        {
            if (redBirdUnlocked)
            {
                //deactivating the green bird
                birds[1].SetActive(false);
                //activate the red bird
                GameController.instance.SetSelectedBird(2);
                birds[GameController.instance.GetSelectedBird()].SetActive(true);
            } else
            {
                //deactivating the green bird
                birds[1].SetActive(false);
                //activate the blue bird instead of red bird
                GameController.instance.SetSelectedBird(0);
                birds[GameController.instance.GetSelectedBird()].SetActive(true);
            }
        //the red bird is currently selected
        } else if (GameController.instance.GetSelectedBird() == 2)
        {
            //deactivating the red bird
            birds[2].SetActive(false);
            //activate the blue bird
            GameController.instance.SetSelectedBird(0);
            birds[GameController.instance.GetSelectedBird()].SetActive(true);
        }
    }


}
