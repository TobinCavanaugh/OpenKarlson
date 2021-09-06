using Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public AudioManager manager;
    public Lobby()
    {
    }

    public void ButtonSound()
    {
        manager.Play("Button");
    }

    public void Discord()
    {
        Application.OpenURL("https://discord.gg/P53pFtR");
    }

    public void EvanTwitter()
    {
        Application.OpenURL("https://twitter.com/EvanKingAudio");
    }

    public void EvanYoutube()
    {
        Application.OpenURL("https://www.youtube.com/user/EvanKingAudio");
    }

    public void Exit()
    {
        Application.Quit(0);
    }

    public void Facebook()
    {
        Application.OpenURL("https://www.facebook.com/DWSgames");
    }

    public void LoadMap(string s)
    {
        if (s.Equals(""))
        {
            return;
        }
        SceneManager.LoadScene(s);
        Game.Instance.StartGame();
    }

    private void Start()
    {
    }

    public void Steam()
    {
        Application.OpenURL("https://store.steampowered.com/app/1228610/Karlson");
    }

    public void Twitter()
    {
        Application.OpenURL("https://twitter.com/DaniDevYT");
    }

    public void Youtube()
    {
        Application.OpenURL("https://youtube.com/danidev");
    }
}