using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameModel
{
    public enum Status
    {
        NOT_STARTED,
        STARTED,
        ENDED,
    }

    public Status CurrentStatus 
    { 
        get => currStatus; 
        set => currStatus = value; 
    }
    Status currStatus;

    public int Score
    {
        get => score;
        set => score = value;
    }
    int score;

    public float ButtonsTime
    {
        get => buttonsTime;
        set => buttonsTime = value;
    }
    float buttonsTime = 3;

    public int NumTimeToSpawnBtns
    {
        get => numTimeToSpawnBtns;
        set => numTimeToSpawnBtns = value;
    }
    int numTimeToSpawnBtns = 5;
}

public class GameController : MonoBehaviour
{
    public static GameController INSTANCE = null;

    [Header("HUD gameobjects")]
    [SerializeField] Slider timeSlider;
    [SerializeField] TMP_Text scoreText;
    [Header("Start Screen UI gameobjects")]
    [SerializeField] TMP_Text startingText;
    [Header("End Screen UI gameobjects")]
    [SerializeField] TMP_Text highscoreText;

    GameModel gameModel;
    float timer;
    int count;

    void Awake()
    {
        // singleton
        if (!INSTANCE)
            INSTANCE = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        gameModel = new GameModel();
        gameModel.CurrentStatus = GameModel.Status.NOT_STARTED;
        timeSlider.maxValue = gameModel.ButtonsTime;
        StaticDataStorage.Highscore = PlayerPrefs.GetInt("Highscore");
    }

    void Update()
    {
        switch(gameModel.CurrentStatus)
        {
            case GameModel.Status.NOT_STARTED:
                startingText.gameObject.GetParentObj().SetActive(true);
                startingText.text = "Game starting in " +  (3 - (int)timer).ToString() + "...";

                timer += Time.deltaTime;
                
                //start game
                if (timer >= 4)
                {
                    startingText.gameObject.GetParentObj().SetActive(false);
                    gameModel.CurrentStatus = GameModel.Status.STARTED;
                    ButtonController.INSTANCE.SpawnButtons();
                    timer = 0;
                }
                break; 

            case GameModel.Status.STARTED:
                scoreText.gameObject.GetParentObj().SetActive(true);
                scoreText.text = gameModel.Score + "/" + gameModel.NumTimeToSpawnBtns + " POINTS";

                if (timer >= gameModel.ButtonsTime)
                {
                    if (ButtonController.INSTANCE.GetAreAllBtnsClicked())
                        ++gameModel.Score;
                    else
                        ButtonController.INSTANCE.DespawnButtons();
                    ButtonController.INSTANCE.SpawnButtons();
                    timer = 0;
                    ++count;
                }

                timeSlider.value = timer;
                timer += Time.deltaTime;

                //end game
                if (count == gameModel.NumTimeToSpawnBtns)
                {
                    scoreText.gameObject.GetParentObj().SetActive(false);
                    StaticDataStorage.Highscore = gameModel.Score;
                    gameModel.CurrentStatus = GameModel.Status.ENDED;
                }
                break; 

            case GameModel.Status.ENDED:
                highscoreText.gameObject.GetParentObj().SetActive(true);
                timer = 0;
                highscoreText.text = "Your Highscore is " + StaticDataStorage.Highscore.ToString() + "!";
                break;

        }
    } 
    
    public void OnReturnButtonClicked()
    {
        SceneManager.LoadScene((int)LoadingModel.Scene.SPLASH_SCENE);
    }
}
