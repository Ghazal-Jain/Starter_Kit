using UnityEngine;
using UnityEngine.Audio;

namespace CTIN_406L_Starter_Pack.Sound_Manager
{
	[CreateAssetMenu(menuName = "Sound Objects/Multiple Sounds Object")]
	public class MultipleSoundsObject : SoundEvent
	{
		[Header("Audio File & Mixer Group")]
		private int clipIndex = 0;
		public AudioClip[] myAudioClips;
		public AudioMixerGroup audioMixerGroup;
    
		public SpatialBlendType spatialBlend;
    
		[Header("Volume and Pitch Range")]
    
		[MinMaxSlider(0.2f,1.0f)]
		public Vector2 setVolume = new Vector2(0.8f,0.8f);
		[MinMaxSlider(0.2f,3.0f)]
		public Vector2 setPitch = new Vector2(1.0f,1.0f);
        
		public enum EventPlayType
		{
			Sequential,
			Random
		};

		public EventPlayType _playType;
		public bool loop = true;

		[HideInInspector]
		public float volume, pitch,spatialBlendVal;

		public void GetParameters()
		{
			volume = Random.Range(setVolume.x, setVolume.y);
			pitch = Random.Range(setPitch.x, setPitch.y);
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
				if (myAudioClips.Length == 0) return;
				if (audioMixerGroup)
					source.outputAudioMixerGroup =audioMixerGroup;

				CheckSpatialBlend(source);

				CheckEventPlayType(source);
				if (fadeIn == FadeIn.FadeIn)
				{
					AudioFader.StartFadeIn(source,volume,fadeTime);   
				}
				source.Play();
				if (source.clip == null)
				{
					ResetClipIndex();
				}
			}
		}

		private void CheckEventPlayType(AudioSource source)
		{
			if (_playType == EventPlayType.Random && !loop)
				PlayRandom(source);

			if (_playType == EventPlayType.Sequential && loop)
				PlayLoop(source);

			if (_playType == EventPlayType.Sequential && !loop)
			{
				PlaySequential(source);
			}
			if (_playType == EventPlayType.Random && loop)
			{
				PlayRandomLoop(source);
			}
		}

		private void PlayRandomLoop(AudioSource source)
		{
			if (clipIndex == myAudioClips.Length)
			{
				clipIndex = 0;
			}
			source.clip = myAudioClips[Random.Range(0, myAudioClips.Length)];
			nowPlaying = source.clip.name;
			source.volume =  Random.Range(setVolume.x, setVolume.y);
			source.pitch =  Random.Range(setPitch.x, setPitch.y);
			clipIndex++;
		}

		public void ResetClipIndex()
		{
			clipIndex = 0;
		}
		public void Reset()
		{
			setVolume = new Vector2(0.8f,0.8f);
			setPitch = new Vector2(1.0f,1.0f);
		}

		private void PlaySequential(AudioSource source)
		{
			if (source)
			{
				if (clipIndex == myAudioClips.Length)
				{
					source.clip = null;
					return;
				}

				source.clip = myAudioClips[clipIndex];
				clipIndex++;
				nowPlaying = source.clip.name;
				source.volume =  Random.Range(setVolume.x, setVolume.y);
				source.pitch =  Random.Range(setPitch.x, setPitch.y);
			}
		}

		private void PlayRandom(AudioSource source)
		{
			if (clipIndex >= myAudioClips.Length)
			{
				source.clip = null;
				return;
			}
			clipIndex++;
			source.clip = myAudioClips[Random.Range(0, myAudioClips.Length)];
			nowPlaying = source.clip.name;
			source.volume =  Random.Range(setVolume.x, setVolume.y);
			source.pitch =  Random.Range(setPitch.x, setPitch.y);
		}

		private void PlayLoop(AudioSource source)
		{
			if (clipIndex >= myAudioClips.Length)
			{
				clipIndex = 0;
			}
			source.clip = myAudioClips[clipIndex];
			clipIndex++;
			nowPlaying = source.clip.name;
			source.volume =  Random.Range(setVolume.x, setVolume.y);
			source.pitch =  Random.Range(setPitch.x, setPitch.y);
		}

		private void CheckSpatialBlend(AudioSource source)
		{
			if(source)
				source.spatialBlend = spatialBlend == SpatialBlendType.Blend2D ? 0 : 1;
		}
	}
}

