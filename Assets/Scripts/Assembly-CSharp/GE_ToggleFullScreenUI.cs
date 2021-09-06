using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GE_ToggleFullScreenUI : MonoBehaviour
{
    private int m_DefWidth;

    private int m_DefHeight;

    public GE_ToggleFullScreenUI()
    {
    }

    public void OnButton_ToggleFullScreen()
    {
        if (!Application.isEditor)
        {
            Screen.fullScreen = !Screen.fullScreen;
            if (!Screen.fullScreen)
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
                return;
            }
            Screen.SetResolution(this.m_DefWidth, this.m_DefHeight, false);
        }
        else if (base.gameObject.activeSelf)
        {
            base.gameObject.GetComponent<Button>().interactable = false;
            foreach (object obj in base.transform)
            {
                ((Transform)obj).gameObject.SetActive(true);
            }
        }
    }

    private void Start()
    {
        this.m_DefWidth = Screen.width;
        this.m_DefHeight = Screen.height;
        if (!Application.isEditor)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
            {
                base.gameObject.SetActive(true);
                return;
            }
            base.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
    }
}