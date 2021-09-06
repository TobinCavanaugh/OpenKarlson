using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public class Sound
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 2f)]
        public float volume;

        [Range(0f, 2f)]
        public float pitch;

        public bool loop;

        public bool bypass;

        [HideInInspector]
        public AudioSource source;

        public Sound()
        {
        }
    }
}