using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public Text scoreText;
    public Text ballsText;
    public Text levelText;
    public Text highscoreText;

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
                break;
            case State.GAMEOVER:
                if(Score > PlayerPrefs.GetInt("highscore"))
                {
                    PlayerPrefs.SetInt("highscore", Score);
                }
                panelGameOver.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.PLAY:
                if(currBall == null)
                {
                    if(Balls > 0)
                    {
                        currBall = Instantiate(ballPrefab);
                    }
                    else 
                    {
                        SwitchState(State.GAMEOVER);
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
}
