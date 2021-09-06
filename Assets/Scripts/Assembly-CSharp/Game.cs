using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public bool playing;

    public bool done;

    public static Game Instance
    {
        get;
        private set;
    }

    public Game()
    {
    }

    public void EndGame()
    {
        this.playing = false;
    }

    public void MainMenu()
    {
        this.playing = false;
        SceneManager.LoadScene("MainMenu");
        UIManger.Instance.GameUI(false);
        Time.timeScale = 1f;
    }

    public void NextMap()
    {
        Time.timeScale = 1f;
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (activeScene + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            this.MainMenu();
            return;
        }
        SceneManager.LoadScene(activeScene + 1);
        this.StartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        this.StartGame();
    }

    private void Start()
    {
        Game.Instance = this;
        this.playing = false;
    }

    public void StartGame()
    {
        this.playing = true;
        this.done = false;
        Time.timeScale = 1f;
        UIManger.Instance.StartGame();
        Timer.Instance.StartTimer();
    }

    public void Win()
    {
        int num;
        this.playing = false;
        Timer.Instance.Stop();
        Time.timeScale = 0.05f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManger.Instance.WinUI(true);
        float timer = Timer.Instance.GetTimer();
        Scene activeScene = SceneManager.GetActiveScene();
        char chr = activeScene.name[0];
        int num1 = Int32.Parse(chr.ToString() ?? "");
        activeScene = SceneManager.GetActiveScene();
        if (Int32.TryParse(activeScene.name.Substring(0, 2) ?? "", out num))
        {
            num1 = num;
        }
        float instance = SaveManager.Instance.state.times[num1];
        if (timer < instance || instance == 0f)
        {
            SaveManager.Instance.state.times[num1] = timer;
            SaveManager.Instance.Save();
        }
        MonoBehaviour.print(String.Concat("time has been saved as: ", Timer.Instance.GetFormattedTime(timer)));
        this.done = true;
    }
}