using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public Sound[] sounds;

        public Sound[] footsteps;

        public Sound[] wallrun;

        public Sound[] jumps;

        public AudioLowPassFilter filter;

        private float desiredFreq = 500f;

        private float velFreq;

        private float freqSpeed = 0.2f;

        public bool muted;

        public static AudioManager Instance
        {
            get;
            set;
        }

        public AudioManager()
        {
        }

        private void Awake()
        {
            int i;
            AudioManager.Instance = this;
            Sound[] soundArray = this.sounds;
            for (i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                sound.source = base.gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.loop = sound.loop;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.bypassListenerEffects = sound.bypass;
            }
            soundArray = this.footsteps;
            for (i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound1 = soundArray[i];
                sound1.source = base.gameObject.AddComponent<AudioSource>();
                sound1.source.clip = sound1.clip;
                sound1.source.loop = sound1.loop;
                sound1.source.volume = sound1.volume;
                sound1.source.pitch = sound1.pitch;
                sound1.source.bypassListenerEffects = sound1.bypass;
            }
            soundArray = this.wallrun;
            for (i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound2 = soundArray[i];
                sound2.source = base.gameObject.AddComponent<AudioSource>();
                sound2.source.clip = sound2.clip;
                sound2.source.loop = sound2.loop;
                sound2.source.volume = sound2.volume;
                sound2.source.pitch = sound2.pitch;
                sound2.source.bypassListenerEffects = sound2.bypass;
            }
            soundArray = this.jumps;
            for (i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound3 = soundArray[i];
                sound3.source = base.gameObject.AddComponent<AudioSource>();
                sound3.source.clip = sound3.clip;
                sound3.source.loop = sound3.loop;
                sound3.source.volume = sound3.volume;
                sound3.source.pitch = sound3.pitch;
                sound3.source.bypassListenerEffects = sound3.bypass;
            }
        }

        public void MuteMusic()
        {
            Sound[] soundArray = this.sounds;
            for (int i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                if (sound.name == "Song")
                {
                    sound.source.volume = 0f;
                    return;
                }
            }
        }

        public void MuteSounds(bool b)
        {
            if (!b)
            {
                AudioListener.volume = 1f;
            }
            else
            {
                AudioListener.volume = 0f;
            }
            this.muted = b;
        }

        public void Play(string n)
        {
            if (this.muted && n != "Song")
            {
                return;
            }
            Sound[] soundArray = this.sounds;
            for (int i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                if (sound.name == n)
                {
                    sound.source.Play();
                    return;
                }
            }
        }

        public void PlayButton()
        {
            if (this.muted)
            {
                return;
            }
            Sound[] soundArray = this.sounds;
            int num = 0;
            while (num < (int)soundArray.Length)
            {
                Sound sound = soundArray[num];
                if (sound.name != "Button")
                {
                    num++;
                }
                else
                {
                    sound.source.pitch = 0.8f + UnityEngine.Random.Range(-0.03f, 0.03f);
                    break;
                }
            }
            this.Play("Button");
        }

        public void PlayFootStep()
        {
            if (this.muted)
            {
                return;
            }
            int num = UnityEngine.Random.Range(0, (int)this.footsteps.Length - 1);
            this.footsteps[num].source.Play();
        }

        public void PlayJump()
        {
            if (this.muted)
            {
                return;
            }
            int num = UnityEngine.Random.Range(0, (int)this.jumps.Length - 1);
            Sound sound = this.jumps[num];
            if (sound.source)
            {
                sound.source.Play();
            }
        }

        public void PlayLanding()
        {
            if (this.muted)
            {
                return;
            }
            int num = UnityEngine.Random.Range(0, (int)this.wallrun.Length - 1);
            this.wallrun[num].source.Play();
        }

        public void PlayPitched(string n, float v)
        {
            if (this.muted)
            {
                return;
            }
            Sound[] soundArray = this.sounds;
            int num = 0;
            while (num < (int)soundArray.Length)
            {
                Sound sound = soundArray[num];
                if (sound.name != n)
                {
                    num++;
                }
                else
                {
                    sound.source.pitch = 1f + UnityEngine.Random.Range(-v, v);
                    break;
                }
            }
            this.Play(n);
        }

        public void SetFreq(float freq)
        {
            this.desiredFreq = freq;
        }

        public void SetVolume(float v)
        {
            Sound[] soundArray = this.sounds;
            for (int i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                if (sound.name == "Song")
                {
                    sound.source.volume = v;
                    return;
                }
            }
        }

        public void Stop(string n)
        {
            Sound[] soundArray = this.sounds;
            for (int i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                if (sound.name == n)
                {
                    sound.source.Stop();
                    return;
                }
            }
        }

        public void UnmuteMusic()
        {
            Sound[] soundArray = this.sounds;
            for (int i = 0; i < (int)soundArray.Length; i++)
            {
                Sound sound = soundArray[i];
                if (sound.name == "Song")
                {
                    sound.source.volume = 1.15f;
                    return;
                }
            }
        }

        private void Update()
        {
        }
    }
}