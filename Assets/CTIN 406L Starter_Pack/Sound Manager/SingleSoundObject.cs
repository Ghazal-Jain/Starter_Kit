using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Audio;


namespace CTIN_406L_Starter_Pack.Sound_Manager
{
    [CreateAssetMenu(menuName = "Sound Objects/Single Sound Object")]
    public class SingleSoundObject : SoundEvent
    {
        
        [Header("Audio File & Mixer Group")]
        public AudioClip myAudioClip;
        public AudioMixerGroup audioMixerGroup;
    
        public SpatialBlendType spatialBlend;
    
        [Header("Volume and Pitch Range")]
    
        [MinMaxSlider(0.2f,1.0f)]
        public Vector2 setVolume = new Vector2(0.8f,0.8f);
        [MinMaxSlider(0.2f,3.0f)]
        public Vector2 setPitch = new Vector2(1.0f,1.0f);

        [HideInInspector]
        public float volume, pitch,spatialBlendVal;

        public void GetParameters()
        {
            volume = Random.Range(setVolume.x, setVolume.y);
            pitch = Random.Range(setPitch.x, setPitch.y);
            CheckSpatialBlend();
        }

        public void SetDefaultParameters()
        {
            volume = 0.8f;
            pitch = 1.0f;
        }
    
        public override void Play(AudioSource source)
        {
            if (source)
            {
                if (!myAudioClip) return;
                if (audioMixerGroup)
                    source.outputAudioMixerGroup = audioMixerGroup;

                CheckSpatialBlend(source);
                PlaySingleSound(source);
                if (fadeIn == FadeIn.FadeIn)
                {
                    AudioFader.StartFadeIn(source,volume,fadeTime);   
                }
                source.Play();


            }
        }

        public void Reset()
        {
            setVolume = new Vector2(0.8f,0.8f);
            setPitch = new Vector2(1.0f,1.0f);
        }
        private void CheckSpatialBlend(AudioSource source)
        {
            if(source)
                source.spatialBlend = spatialBlend == SpatialBlendType.Blend2D ? 0 : 1;
        }
        private void CheckSpatialBlend()
        {
            spatialBlendVal = spatialBlend == SpatialBlendType.Blend2D ? 0 : 1;
        }
    
        private void PlaySingleSound(AudioSource source)
        {
            if (source)
            {
                source.clip = myAudioClip;
                nowPlaying = source.clip.name;
                source.volume =  Random.Range(setVolume.x, setVolume.y);
                source.pitch =  Random.Range(setPitch.x, setPitch.y);
            }
        }
    }
}
