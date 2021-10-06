using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Sound_Manager
{
    public enum SpatialBlendType { Blend2D, Blend3D };
    public abstract class SoundEvent : ScriptableObject
    {
        public static string nowPlaying;

        public enum FadeIn
        {
            NoFadeIn,
            FadeIn
        };
        public enum FadeOut
        {
            NoFadeOut,
            FadeOut
        };
        
        [Header("Fade In & Out")]
        public FadeIn fadeIn;
        //public FadeOut fadeOut;
        [Range(0.0f, 10.0f)]
        public float fadeTime = 3.0f;
        
        public abstract void Play(AudioSource source);
    }
}