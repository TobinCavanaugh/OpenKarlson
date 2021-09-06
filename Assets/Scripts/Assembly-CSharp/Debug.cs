using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Debug : MonoBehaviour
{
    public Text fps;

    public TMP_InputField console;

    public TextMeshProUGUI consoleLog;

    private bool fpsOn;

    private bool speedOn;

    private float deltaTime;

    public Debug()
    {
    }

    private void ChangeFov(int n)
    {
        //GameState.Instance.SetFov((float)n);
        //TextMeshProUGUI textMeshProUGU = this.consoleLog;
        //textMeshProUGU.text = String.Concat(new Object[] { textMeshProUGU.text, "FOV set to ", n, "\n" });
    }
    

    private void ChangeSens(int n)
    {
        //GameState.Instance.SetSensitivity((float)n);
        //TextMeshProUGUI textMeshProUGU = this.consoleLog;
        //textMeshProUGU.text = String.Concat(new Object[] { textMeshProUGU.text, "Sensitivity set to ", n, "\n" });
    }

    private void CloseConsole()
    {
        //this.console.gameObject.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        //PlayerMovement.Instance.paused = false;
        //Time.timeScale = 1f;
    }

    private int CountWords(string text)
    {
        // int num = 0;
        // int num1 = 0;
        // while (num1 < text.Length)
        // {
        //     if (Char.IsWhiteSpace(text[num1]))
        //     {
        //         num1++;
        //     }
        //     else
        //     {
        //         break;
        //     }
        // }
        // while (num1 < text.Length)
        // {
        //     while (num1 < text.Length && !Char.IsWhiteSpace(text[num1]))
        //     {
        //         num1++;
        //     }
        //     num++;
        //     while (num1 < text.Length && Char.IsWhiteSpace(text[num1]))
        //     {
        //         num1++;
        //     }
        // }
        // return num;

        return 1;
    }

    private void Fps()
    {
        // if (!this.fpsOn && !this.speedOn)
        // {
        //     if (this.fps.enabled)
        //     {
        //         return;
        //     }
        //     this.fps.gameObject.SetActive(false);
        //     return;
        // }
        // if (!this.fps.gameObject.activeInHierarchy)
        // {
        //     this.fps.gameObject.SetActive(true);
        // }
        // this.deltaTime = this.deltaTime + (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
        // float single = this.deltaTime * 1000f;
        // float single1 = 1f / this.deltaTime;
        // string str = "";
        // if (this.fpsOn)
        // {
        //     str = String.Concat(str, String.Format("{0:0.0} ms ({1:0.} fps)", single, single1));
        // }
        // if (this.speedOn)
        // {
        //     Vector3 instance = PlayerMovement.Instance.rb.velocity;
        //     str = String.Concat(str, "\nm/s: ", String.Format("{0:F1}", instance.magnitude));
        // }
        // this.fps.text = str;
    }

    private void FpsLimit(int n)
    {
        //Application.targetFrameRate = n;
        //TextMeshProUGUI textMeshProUGUI = this.consoleLog;
        //textMeshProUGUI.text = String.Concat(new Object[] { textMeshProUGUI.text, "Max FPS set to ", n, "\n" });
    }

    private void Help()
    {
        //string str = "The console can be used for simple commands.\nEvery command must be followed by number i (0 = false, 1 = true)\n<i><b>fps 1</b></i>            shows fps\n<i><b>speed 1</b></i>      shows speed\n<i><b>fov i</b></i>             sets fov to i\n<i><b>sens i</b></i>          sets sensitivity to i\n<i><b>fpslimit i</b></i>    sets max fps\n<i><b>TAB</b></i>              to open/close the console\n";
        //TextMeshProUGUI textMeshProUGUI = this.consoleLog;
        //textMeshProUGUI.text = String.Concat(textMeshProUGUI.text, str);
    }

    private void OpenCloseFps(int n)
    {
        //this.fpsOn = n == 1;
        //TextMeshProUGUI textMeshProUGUI = this.consoleLog;
        //string str = textMeshProUGUI.text;
        //bool flag = String.Concat((object)"FPS set to ", n) == String.Concat(1, (object)"\n");
        //textMeshProUGUI.text = String.Concat(str, flag.ToString());
    }

    private void OpenCloseSpeed(int n)
    {
        //this.speedOn = n == 1;
        //TextMeshProUGUI textMeshProUGUI = this.consoleLog;
        //string str = textMeshProUGUI.text;
        //bool flag = String.Concat((object)"Speedometer set to ", n) == String.Concat(1, (object)"\n");
        //textMeshProUGUI.text = String.Concat(str, flag.ToString());
    }

    private void OpenConsole()
    {
        //this.console.gameObject.SetActive(true);
        //this.console.Select();
        //this.console.ActivateInputField();
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        //PlayerMovement.Instance.paused = true;
        //Time.timeScale = 0f;
    }

    public void RunCommand()
    {
        //int num;
        //string str = this.console.text;
        //TextMeshProUGUI textMeshProUGUI = this.consoleLog;
        //textMeshProUGUI.text = String.Concat(textMeshProUGUI.text, str, "\n");
        //if (str.Length < 2 || str.Length > 30 || this.CountWords(str) != 2)
        //{
        //    this.console.text = "";
        //    this.console.Select();
        //    this.console.ActivateInputField();
        //    return;
        //}
        //this.console.text = "";
        //string str1 = str.Substring(str.IndexOf(' ') + 1);
        //string str2 = str.Substring(0, str.IndexOf(' '));
        //if (!Int32.TryParse(str1, out num))
        //{
        //    TextMeshProUGUI textMeshProUGUI1 = this.consoleLog;
        //    textMeshProUGUI1.text = String.Concat(textMeshProUGUI1.text, "Command not found\n");
        //    return;
        //}
        //if (str2 == "fps")
        //{
        //    this.OpenCloseFps(num);
        //}
        //else if (str2 == "fpslimit")
        //{
        //    this.FpsLimit(num);
        //}
        //else if (str2 == "fov")
        //{
        //    this.ChangeFov(num);
        //}
        //else if (str2 == "sens")
        //{
        //    this.ChangeSens(num);
        //}
        //else if (str2 == "speed")
        //{
        //    this.OpenCloseSpeed(num);
        //}
        //else if (str2 == "help")
        //{
        //    this.Help();
        //}
        //this.console.Select();
        //this.console.ActivateInputField();
    }

    private void Start()
    {
        Application.targetFrameRate = 150;
    }

    private void Update()
    {
        //this.Fps();
        //if (Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if (this.console.isActiveAndEnabled)
        //    {
        //        this.CloseConsole();
        //        return;
        //    }
        //    this.OpenConsole();
        //}
    }
}