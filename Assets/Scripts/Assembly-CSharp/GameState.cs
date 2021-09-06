using Audio;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameState : MonoBehaviour
{
	public GameObject ppVolume;

	public PostProcessProfile pp;

	private MotionBlur ppBlur;

	public bool graphics = true;

	public bool muted;

	public bool blur = true;

	public bool shake = true;

	public bool slowmo = true;

	private float sensitivity = 1f;

	private float volume;

	private float music;

	public float fov = 1f;

	public float cameraShake = 1f;

    	public SaveManager saveManager;
    	public MoveCamera moveCamera;
    	public Music musicM;
    	public PlayerMovement playerMovement;

	public static GameState Instance
	{
		get;
		private set;
	}

	public GameState()
	{
	}

	public float GetFov()
	{
		return this.fov;
	}

	public bool GetGraphics()
	{
		return this.graphics;
	}

	public float GetMusic()
	{
		return this.music;
	}

	public bool GetMuted()
	{
		return this.muted;
	}

	public float GetSensitivity()
	{
		return this.sensitivity;
	}

	public float GetVolume()
	{
		return this.volume;
	}

	public void SetBlur(bool b)
	{
		this.blur = b;
		if (!b)
		{
			this.ppBlur.shutterAngle.@value = 0f;
		}
		else
		{
			this.ppBlur.shutterAngle.@value = 160f;
		}
		saveManager.state.motionBlur = b;
		saveManager.Save();
	}

	public void SetFov(float f)
	{
		float single = Mathf.Clamp(f, 50f, 150f);
		this.fov = single;
		if (moveCamera)
		{
			moveCamera.UpdateFov();
		}
		saveManager.state.fov = single;
		saveManager.Save();
	}

	public void SetGraphics(bool b)
	{
		this.graphics = b;
		this.ppVolume.SetActive(b);
		saveManager.state.graphics = b;
		saveManager.Save();
	}

	public void SetMusic(float s)
	{
		float single = Mathf.Clamp(s, 0f, 1f);
		this.music = single;
		if (Music.Instance)
		{
			Music.Instance.SetMusicVolume(single);
		}
		saveManager.state.music = single;
		saveManager.Save();
		MonoBehaviour.print(String.Concat((object)"music saved as: ", this.music));
	}

	public void SetMuted(bool b)
	{
		AudioManager.Instance.MuteSounds(b);
		this.muted = b;
		saveManager.state.muted = b;
		saveManager.Save();
	}

	public void SetSensitivity(float s)
	{
		float single = Mathf.Clamp(s, 0f, 5f);
		this.sensitivity = single;
		if (PlayerMovement.Instance)
		{
			PlayerMovement.Instance.UpdateSensitivity();
		}
		saveManager.state.sensitivity = single;
		saveManager.Save();
	}

	public void SetShake(bool b)
	{
		this.shake = b;
		if (!b)
		{
			this.cameraShake = 0f;
		}
		else
		{
			this.cameraShake = 1f;
		}
		saveManager.state.cameraShake = b;
		saveManager.Save();
	}

	public void SetSlowmo(bool b)
	{
		this.slowmo = b;
		saveManager.state.slowmo = b;
		saveManager.Save();
	}

	public void SetVolume(float s)
	{
		float single = Mathf.Clamp(s, 0f, 1f);
		this.volume = single;
		AudioListener.volume = single;
		saveManager.state.volume = single;
		saveManager.Save();
	}

	private void Start()
	{
		GameState.Instance = this;
		this.ppBlur = this.pp.GetSetting<MotionBlur>();
		this.graphics = saveManager.state.graphics;
		this.shake = saveManager.state.cameraShake;
		this.blur = saveManager.state.motionBlur;
		this.slowmo = saveManager.state.slowmo;
		this.muted = saveManager.state.muted;
		this.sensitivity = saveManager.state.sensitivity;
		this.music = saveManager.state.music;
		this.volume = saveManager.state.volume;
		this.fov = saveManager.state.fov;
		this.UpdateSettings();
	}

	private void UpdateSettings()
	{
		this.SetGraphics(this.graphics);
		this.SetBlur(this.blur);
		this.SetSensitivity(this.sensitivity);
		this.SetMusic(this.music);
		this.SetVolume(this.volume);
		this.SetFov(this.fov);
		this.SetShake(this.shake);
		this.SetSlowmo(this.slowmo);
		this.SetMuted(this.muted);
	}
}