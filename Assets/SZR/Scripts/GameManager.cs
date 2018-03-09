// Copyright 2015 - 2018 Jesse Freeman
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Collections;
using System.Collections.Generic;
using jessefreeman.utools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Sounds
{
    DeathTheme,
    Selection,
    WalkingSound,
    StartGame,
    Explosion,
    Thud,
    HitHard,
    Hurt,
    Death,
    Jump,
    Thud2,
    IntroLoop,
    MonsterStompLoop,
    None
}

public class GameManager : MonoBehaviour
{
    private bool beatBestScore;
    private bool beatBestTime;
    private int bestScore;
    private float bestTime;
    private bool blink;
    private float blinkTime;
    public Text continueText;
    private Spawner doorSpawner;
    public GameObject firstSelected;
    private GameObject floor;
    private bool gameOverLock;
    private readonly float gameOverLockDelay = 2;
    private bool gameStarted;
    public TextAsset jsonData;
    private float lastTimeScale = 1;
    private Spawner obstacleSpawner;
    public PauseButton pauseButton;
    private GameObject player;

    public GameObject playerPrefab;
    public GameObject previousSelected;
    public GameObject quitPanel;
    public GameObject quitPanelFirstSelected;
    private int scoreBuffer;
    public Text scoreText;
    private SoundManager soundManager;
    
    private float timeElapsed;
    private TimeManager timeManager;
    public Text timeText;
    public GameObject[] togglePauseMenuGameObject;

    private StatsManager statsManager
    {
        get { return GameObjectUtil.GetSingleton<StatsManager>(); }
    }

    private AchievementManager achievementManager
    {
        get { return GameObjectUtil.GetSingleton<AchievementManager>(); }
    }

    private AchievementMessageBox achievementMessageBox
    {
        get { return GameObjectUtil.GetSingleton<AchievementMessageBox>(); }
    }

    private ScoreManager scoreManager
    {
        get { return GameObjectUtil.GetSingleton<ScoreManager>(); }
    }

    private TextManager textManager
    {
        get { return GameObjectUtil.GetSingleton<TextManager>(); }
    }

    public string actionButtonType
    {
        get
        {
            var platformDetectionUtil = GameObjectUtil.GetSingleton<PlatformDetectionUtil>();

            var buttonName = "";
            if (platformDetectionUtil.GetPlatformDefinition().hasController)
                buttonName = "controller";
            else if (platformDetectionUtil.GetPlatformDefinition().hasTouch)
                buttonName = "touch";
            else
                buttonName = "keyboard";

            return buttonName;
        }
    }

    private string deviceKeyString
    {
        get
        {
            var buttonText = textManager.GetText(actionButtonType);//data.GetObject("Keys"));
//            var buttonText = keyStrings.GetString(actionButtonType);
            return buttonText;
        }
    }

    private MessageBox messageBox
    {
        get { return GameObjectUtil.GetSingleton<MessageBox>(); }
    }

    private void Awake()
    {
        GameObjectUtil.RegisterSingleton<GameManager>(this);

        //Time.timeScale = 0;
        floor = GameObject.Find("Foreground");
        obstacleSpawner = GameObject.Find("Obstacle Spawner").GetComponent<Spawner>();
        doorSpawner = GameObject.Find("Door Spawner").GetComponent<Spawner>();
        timeManager = GetComponent<TimeManager>();

        textManager.ParseJSON(jsonData.ToString());
    }

    // Use this for initialization
    private void Start()
    {
        soundManager = GameObjectUtil.GetSingleton<SoundManager>();
        var floorHeight = floor.transform.localScale.y;

        var pos = floor.transform.position;
        pos.x = 0;
        pos.y = -(Screen.height / PixelPerfectCamera.pixelsToUnits / 2) + floorHeight / 2;
        floor.transform.position = pos;

        obstacleSpawner.active = false;
        doorSpawner.active = false;

        UpdateContinueText("Start");

        bestTime = PlayerPrefs.GetFloat("BestTime");
        bestScore = PlayerPrefs.GetInt("BestScore");

        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);

        ResetGame();
    }

    private void NewTask()
    {
        ClearTask();

        var am = achievementManager;

        var task = achievementManager.PickAchievement() as GenericAchievement;

        task.CompleteCallback += CompletedTask;
        task.FailCallback += FailedTask;

        achievementMessageBox.ShowMessage(task.GetMessage(), 3, task.defaultIcon);
    }

    private void ClearTask()
    {
        var am = achievementManager;

        if (am.currenAchievement != null)
        {
            var currentTask = am.currenAchievement as GenericAchievement;
            currentTask.CompleteCallback -= CompletedTask;
            currentTask.FailCallback -= FailedTask;
            achievementManager.currenAchievement = null;
        }
    }

    private void CompletedTask()
    {
        var task = achievementManager.PickAchievement() as GenericAchievement;

        achievementMessageBox.ShowMessage(task.GetMessage(), 2, task.completedIcon);

        scoreManager.IncreaseScore(task.rewardValue);

        StartCoroutine(NextTask());
    }

    private IEnumerator NextTask()
    {
        yield return new WaitForSeconds(3);
        NewTask();
    }

    private void FailedTask()
    {
    }

    // Update is called once per frame
    private void Update()
    {
//        if (!gameStarted && !gameOverLock)
//            if (Input.anyKeyDown)
//                if (!Input.GetKeyDown(KeyCode.Escape))
//                {
//                    timeManager.ManipulateTime(1, 1f);
//                    soundManager.PlayClip((int) Sounds.StartGame);
//
//                    ResetGame();
//                }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Only toggle the pause menu when time is stopped or at full speed, not during a transition.
            if (Time.timeScale % 1 != 0)
                return;
            
            if (gameStarted)
            {
                if (quitPanel.activeSelf)
                    ExitQuit();
                else
                    TogglePause();
            }
            else
            {
                if (quitPanel.activeSelf)
                    ExitQuit();
                else
                    Quit();
            }

        }
            

        if (!gameStarted)
        {
            blinkTime++;

            if (blinkTime % 40 == 0) blink = !blink;

            if (!gameOverLock)
                continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
            
        }
        else
        {
            timeElapsed += Time.deltaTime;
            timeText.text = "TIME: " + FormatTime(timeElapsed);

            scoreBuffer = (int) GetValue(scoreBuffer, scoreManager.GetScore(), 2);
            scoreText.text = "SCORE: " + scoreBuffer.ToString("D5");
            var combo = (int) statsManager.GetStatValue("JumpCombo") - 1;

            if (combo > 1)
                scoreText.text += "\nX COMBO: " + combo.ToString("D2");
        }
    }

    private void OnPlayerKilled()
    {
        if (pauseButton != null)
            pauseButton.gameObject.SetActive(false);

        //  obstacleSpawner.active = false;
        //  doorSpawner.active = false;

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallback -= OnPlayerKilled;

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        timeManager.ManipulateTime(0, 5.5f);
        gameStarted = false;

        if (timeElapsed > bestTime)
        {
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            beatBestTime = true;
        }

        soundManager.PlayClip((int) Sounds.Explosion);

        statsManager.ResetStat("JumpCombo");
        scoreManager.IncreaseScore((int) timeElapsed);

        if (scoreManager.GetScore() > bestScore)
        {
            bestScore = scoreManager.GetScore();
            PlayerPrefs.SetInt("BestScore", bestScore);
            beatBestScore = true;
        }

        achievementMessageBox.Reset();
        achievementMessageBox.ToggleDisplay(false);

        UpdateContinueText("restart");

        gameOverLock = true;
        StartCoroutine(ResetGameOverLock(gameOverLockDelay));
    }

    public void UpdateContinueText(string id)
    {
        var token = new Dictionary<string, string>();
        token.Add("${key}", deviceKeyString);

        continueText.text = textManager.GetText(id, token);
    }

    public GameObject stats;
    public GameObject statsMask;
    
    private IEnumerator ResetGameOverLock(float delay)
    {
        var pauseEndTime = Time.realtimeSinceStartup + delay;
        while (Time.realtimeSinceStartup < pauseEndTime) yield return 0;

        gameOverLock = false;

        Debug.Log("Game Over");
        
        statsMask.SetActive(true);
        
        stats.GetComponent<Stats>().showGameOverSummary = true;
        stats.SetActive(true);
        
//        Debug.Log("Reset Game Over Lock");

//        var textColor = beatBestTime ? "#FF0" : "#FFF";
//        var textColor2 = beatBestScore ? "#FF0" : "#FFF";

        timeText.text = "";//"TIME: " + FormatTime(timeElapsed) + "\n<color=" + textColor + ">BEST: " + FormatTime(bestTime) + "</color>";
        scoreText.text = "";//"SCORE: " + scoreManager.GetScore().ToString("D5") + "\n<color=" + textColor2 + ">BEST: " + bestScore.ToString("D5") + "</color>";
    }

    private void ResetGame()
    {
        obstacleSpawner.active = true;
        doorSpawner.active = true;

        player = GameObjectUtil.Instantiate(playerPrefab,
            new Vector3(0, Screen.height / PixelPerfectCamera.pixelsToUnits / 2 + 100, 0));

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallback += OnPlayerKilled;

        gameStarted = true;

        continueText.canvasRenderer.SetAlpha(0);
        scoreBuffer = 0;
        timeElapsed = 0;
        beatBestTime = false;
        beatBestScore = false;
        statsManager.Reset();
        //TODO need a way to restart background music

        achievementMessageBox.ToggleDisplay(true);

        StartCoroutine(NextTask());

        messageBox.ShowMessage(textManager.GetText("instructions"), 4);

        pauseButton.gameObject.SetActive(true);
        
        statsMask.SetActive(false);
        stats.SetActive(false);
        stats.GetComponent<Stats>().showGameOverSummary = false;
        
        if (pauseButton != null)
            pauseButton.gameObject.SetActive(true);
    }

    private string FormatTime(float value)
    {
        var t = TimeSpan.FromSeconds(value);

        return string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
    }

    public void Quit()
    {

        if (Time.timeScale == 0)
            return;
        
        if (quitPanel != null)
        {
            previousSelected = EventSystem.current.currentSelectedGameObject;
            quitPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(quitPanelFirstSelected, null);
            if (!gameStarted)
            {
                gameOverLock = true;
                lastTimeScale = Time.timeScale;
//                Time.timeScale = 0;
            }
        }
    }

    public void OnQuit()
    {
        //TogglePause();
        Time.timeScale = 1;
        GameObjectUtil.CleanupSingletons();
        GameObjectUtil.ClearPools();
        Application.LoadLevel(0);
        //TODO need to call transition manager
    }

    public void ExitQuit()
    {
        if (quitPanel != null)
        {
            quitPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(firstSelected, null);
            if (!gameStarted)
            {
                StartCoroutine(ResetGameOverLock(.5f));
                Time.timeScale = lastTimeScale;
            }
        }
    }

    public void TogglePause()
    {
        var timeScale = Time.timeScale;
        
        Time.timeScale = timeScale == 0 ? 1 : 0;
        if (pauseButton != null)
            pauseButton.UpdateButtonSprite();

        // Loop through game objects
        foreach (var instance in togglePauseMenuGameObject)
            if (instance != null)
                instance.SetActive(!instance.activeSelf);

        if (Time.timeScale == 0)
        {
            // pause
            soundManager.PauseSounds(true);
            //todo need to see if last PlatformInputTypes was mouse or keyboard

            EventSystem.current.SetSelectedGameObject(firstSelected, null);
        }
        else
        {
            soundManager.PauseSounds(false);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private float GetValue(int bufferValue, int value, float speed)
    {
        var buffer = Mathf.Lerp(bufferValue, value, speed * Time.deltaTime);

        if (Math.Abs(bufferValue - value) > 10)
            bufferValue = (int) buffer;
        else
            bufferValue = value;
        return bufferValue;
    }

    public void PlayerAction()
    {
        // Test to see if the game is over
        if (!gameStarted && !gameOverLock)
        {
            timeManager.ManipulateTime(1, 1f);
            soundManager.PlayClip((int) Sounds.StartGame);

            ResetGame();
        }
        // If game is playing see if we can make the player jump
        else
        {
            // Look for a player instance
            if (player != null)
            {
                // Call action on the player's jump component
                player.GetComponent<Jump>().Action();
            }
        }

    }
}