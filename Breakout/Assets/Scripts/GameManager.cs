using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public GameObject textDisplay;
    public Text scoreText;
    public Text ballsText;
    public Text levelText;
    public Text highscoreText;
    public Text timertxt;

    public GameObject panelMenu;
    public GameObject panelPlay;
    public GameObject panelLevelCompeleted;
    public GameObject panelGameOver;


    public GameObject[] levels;
    
    

    public static GameManager Instance { get; private set; }

    public enum State { MENU, INIT, PLAY, COMPLETED, LOADLEVEL, GAMEOVER }
    public State state;
    GameObject currBall;
    GameObject currLevel;
    GameObject currPlayer;
    bool isSwitchingState;
    bool timerStarted = false;
    int numCurrBalls = 0;
    public int secondsLeft = 5;

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value;
            scoreText.text = "Score:" + score;
        }
    }
    
    private int level;
    public int Level
    {
        get { return level; }
        set { level = value; 
            levelText.text = "Level: " + level;
        }
    }

    private int balls;
    public int Balls
    {
        get { return balls; }
        set { balls = value;
            ballsText.text = "Balls: " + balls;
        }
    }
    
    

    public void PlayClicked()
    {
        SwitchState(State.INIT);
    }
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        Instance = this;
        SwitchState(State.MENU);
    }

    

    public void SwitchState(State newState, float delay = 0)
    {
        StartCoroutine(SwitchDelay(newState, delay));
    }

    IEnumerator SwitchDelay(State newState, float delay)
    {
        isSwitchingState = true;
        yield return new WaitForSeconds(delay);
        EndState();
        state = newState;
        BeginState(newState);
        isSwitchingState = false;
    }

    void BeginState(State newState)
    {
        GameObject[] fasterball = GameObject.FindGameObjectsWithTag("FasterBall");
        GameObject[] additionalBallPowerup = GameObject.FindGameObjectsWithTag("AdditionalBall");
        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        switch(newState)
        {
            case State.MENU:
                Cursor.visible = true;
                highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
                panelMenu.SetActive(true);
                break;
            case State.INIT:
                Cursor.visible = false;
                panelPlay.SetActive(true);
                Score = 0;
                Level = 0;
                Balls = 3;
                numCurrBalls = 0;
                if(currLevel != null)
                {
                    Destroy(currLevel);
                }
                currPlayer = Instantiate(playerPrefab);
                SwitchState(State.LOADLEVEL);
                break;
            case State.PLAY:
                break;
            case State.COMPLETED:
                Destroy(currBall);
                Destroy(currLevel);
                DestroyObjects(fasterball);
                DestroyObjects(additionalBallPowerup);
                DestroyObjects(allBalls);
                Balls += 3;
                numCurrBalls = 0;
                Level++;
                panelLevelCompeleted.SetActive(true);
                SwitchState(State.LOADLEVEL, 2f);
                break;
            case State.LOADLEVEL:
                if(Level >= levels.Length)
                {
                    SwitchState(State.GAMEOVER);
                }
                else 
                {
                    currLevel = Instantiate(levels[Level]);
                    SwitchState(State.PLAY);
                }
                textDisplay.SetActive(false);
                Ball.DecreaseBallSpeed(); 
                break;
            case State.GAMEOVER:
                DestroyObjects(fasterball);
                DestroyObjects(additionalBallPowerup);
                DestroyObjects(allBalls);
                
                if(Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                panelGameOver.SetActive(true);
                textDisplay.SetActive(false);
                Ball.DecreaseBallSpeed();
                break;
        }
    }

    private void DestroyObjects(GameObject[] obj)
    {
        foreach(GameObject ob in obj)
        {
            Destroy(ob);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(timerStarted == true)
        {
            timertxt.text = secondsLeft + "";
        }

        switch(state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(Balls == 0)
                {
                    SwitchState(State.GAMEOVER);
                }
                if(currBall == null)
                {
                    if(Balls > 0 && numCurrBalls == 0)
                    {
                        currBall = Instantiate(ballPrefab);
                        numCurrBalls++;
                    }
                    else if(Balls > 0 && numCurrBalls > 0)
                    {
                        // do nothing because we still have balls on the screen
                    }
                }
                if(currLevel != null && currLevel.transform.childCount == 0 && !isSwitchingState)
                {
                    SwitchState(State.COMPLETED);
                }
                break;
            case State.COMPLETED:
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                if(Input.anyKeyDown)
                {
                    SwitchState(State.MENU);
                }
                break;
        }
    }

    void EndState()
    {
        switch(state)
        {
            case State.MENU:
                panelMenu.SetActive(false);
                break;
            case State.INIT:
                break;
            case State.PLAY:
                break;
            case State.COMPLETED:
                panelLevelCompeleted.SetActive(false);
                break;
            case State.LOADLEVEL:
                break;
            case State.GAMEOVER:
                panelPlay.SetActive(false);
                panelGameOver.SetActive(false);
                break;
        }
    }

    public void QuitGame()
    {
        SwitchState(State.MENU);
        Destroy(currBall);
        Destroy(currLevel);
        Destroy(currPlayer);
        balls = 0;
    }

    public void AddBall()
    {
        currBall = Instantiate(ballPrefab);
        numCurrBalls++;
    }

    public void DecBallCount()
    {
        numCurrBalls--;
    }

    public void IncreaseBallSpeed()
    {
        secondsLeft = 10;
        timerStarted = true;
        textDisplay.SetActive(true);
        Ball.IncreaseBallSpeed();
        StartCoroutine(StartTimer("ballspeed"));
    }

    public void IncreasePlayerScale()
    {
        secondsLeft = 15;
        timerStarted = true;
        textDisplay.SetActive(true);
        // increase player scale by calling player class
        Player.instance.IncreasePlayerSize();
        StartCoroutine(StartTimer("playerscale"));
    }

    IEnumerator StartTimer(string powerupType)
    {
        while(secondsLeft > 0)
        {
            textDisplay.GetComponent<Text>().text = "" + secondsLeft;
            yield return new WaitForSeconds(1);
            secondsLeft -= 1;
        }
        textDisplay.SetActive(false);
        timerStarted = false;
        if(powerupType == "ballspeed")
        {
            Ball.DecreaseBallSpeed();
        }
        else if(powerupType == "playerscale")
        {
            Player.instance.DecreasePlayerSize();
        }
    }

    

}
