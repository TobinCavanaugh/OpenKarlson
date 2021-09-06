using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public TextMeshProUGUI sens;

    public TextMeshProUGUI volume;

    public TextMeshProUGUI music;

    public TextMeshProUGUI fov;

    public TextMeshProUGUI[] sounds;

    public TextMeshProUGUI[] graphics;

    public TextMeshProUGUI[] shake;

    public TextMeshProUGUI[] slowmo;

    public TextMeshProUGUI[] blur;

    public Slider sensS;

    public Slider volumeS;

    public Slider musicS;

    public Slider fovS;

    public Options()
    {
    }

    public void ChangeBlur(bool b)
    {
        GameState.Instance.SetBlur(b);
        this.UpdateList(this.blur, b);
    }

    public void ChangeGraphics(bool b)
    {
        GameState.Instance.SetGraphics(b);
        this.UpdateList(this.graphics, b);
    }

    public void ChangeShake(bool b)
    {
        GameState.Instance.SetShake(b);
        this.UpdateList(this.shake, b);
    }

    public void ChangeSlowmo(bool b)
    {
        GameState.Instance.SetSlowmo(b);
        this.UpdateList(this.slowmo, b);
    }

    private void OnEnable()
    {
        this.UpdateList(this.graphics, GameState.Instance.GetGraphics());
        this.UpdateList(this.shake, GameState.Instance.shake);
        this.UpdateList(this.slowmo, GameState.Instance.slowmo);
        this.UpdateList(this.blur, GameState.Instance.blur);
        this.sensS.@value = GameState.Instance.GetSensitivity();
        this.volumeS.@value = GameState.Instance.GetVolume();
        this.musicS.@value = GameState.Instance.GetMusic();
        this.fovS.@value = GameState.Instance.GetFov();
        MonoBehaviour.print(GameState.Instance.GetMusic());
        this.UpdateSensitivity();
        this.UpdateFov();
        this.UpdateVolume();
        this.UpdateMusic();
    }

    public void UpdateFov()
    {
        float single = this.fovS.@value;
        GameState.Instance.SetFov(single);
        this.fov.text = String.Concat(single);
    }

    private void UpdateList(TextMeshProUGUI[] list, bool b)
    {
        if (!b)
        {
            list[1].color = Color.white;
            list[0].color = (Color.clear + Color.white) / 2f;
            return;
        }
        list[1].color = (Color.clear + Color.white) / 2f;
        list[0].color = Color.white;
    }

    public void UpdateMusic()
    {
        float single = this.musicS.@value;
        GameState.Instance.SetMusic(single);
        this.music.text = String.Format("{0:F2}", single);
    }

    public void UpdateSensitivity()
    {
        float single = this.sensS.@value;
        GameState.Instance.SetSensitivity(single);
        this.sens.text = String.Format("{0:F2}", single);
    }

    public void UpdateVolume()
    {
        float single = this.volumeS.@value;
        AudioListener.volume = single;
        GameState.Instance.SetVolume(single);
        this.volume.text = String.Format("{0:F2}", single);
    }
}