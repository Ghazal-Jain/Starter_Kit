using System;
using System.Collections;
using CTIN_406L_Starter_Pack.Scriptable_Objects.Audio;
using CTIN_406L_Starter_Pack.Sound_Manager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.LowLevel;
using Random = UnityEngine.Random;



[CreateAssetMenu(menuName = "Audio Events/Custom Audio")]
public class CustomAudioEvent : AudioEvent
{
	private int clipIndex = 0;
	public AudioClip[] myClips;
	public AudioMixerGroup[] myMixer;

    public bool loop = true;

	public enum EventPlayType
	{
		Sequential,
		Random
	};

	public EventPlayType _playType;

	public SpatialBlendType spatialBlend;
	[MinMaxRange(0f, 1f)] public RangedFloat Volume;
	[MinMaxRange(0, 3f)] public RangedFloat Pitch;
	

	public override void Play(AudioSource source)
	{
		if (source)
		{
			if (myClips.Length == 0) return;
			if (myMixer.Length != 0)
				source.outputAudioMixerGroup = myMixer[Random.Range(0, myMixer.Length)];

			CheckSpatialBlend(source);

			CheckEventPlayType(source);
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
		if (clipIndex == myClips.Length)
		{
			clipIndex = 0;
		}
		source.clip = myClips[Random.Range(0, myClips.Length)];
		nowPlaying = source.clip.name;
		source.volume = Random.Range(Volume.minValue, Volume.maxValue);
		source.pitch = Random.Range(Pitch.minValue, Pitch.maxValue);
		clipIndex++;
	}

	public void ResetClipIndex()
	{
		clipIndex = 0;
	}

	private void PlaySequential(AudioSource source)
	{
		if (source)
		{
			if (clipIndex == myClips.Length)
			{
				source.clip = null;
				return;
			}

			source.clip = myClips[clipIndex];
			clipIndex++;
			nowPlaying = source.clip.name;
			source.volume = Random.Range(Volume.minValue, Volume.maxValue);
			source.pitch = Random.Range(Pitch.minValue, Pitch.maxValue);
		}
	}

	private void PlayRandom(AudioSource source)
	{
		if (clipIndex >= myClips.Length)
		{
			source.clip = null;
			return;
		}
		clipIndex++;
		source.clip = myClips[Random.Range(0, myClips.Length)];
		nowPlaying = source.clip.name;
		source.volume = Random.Range(Volume.minValue, Volume.maxValue);
		source.pitch = Random.Range(Pitch.minValue, Pitch.maxValue);
	}

	private void PlayLoop(AudioSource source)
	{
		if (clipIndex >= myClips.Length)
		{
			clipIndex = 0;
		}
		source.clip = myClips[clipIndex];
		clipIndex++;
		nowPlaying = source.clip.name;
		source.volume = Random.Range(Volume.minValue, Volume.maxValue);
		source.pitch = Random.Range(Pitch.minValue, Pitch.maxValue);
	}

	private void CheckSpatialBlend(AudioSource source)
	{
		if(source)
		source.spatialBlend = spatialBlend == SpatialBlendType.Blend2D ? 0 : 1;
	}
	
}



// [MinMaxRange(-1f, 1f)] public RangedFloat PanStereo;
// [MinMaxRange(0f, 1.1f)] public RangedFloat ReverbZoneMix;
//
// [Header("Spatialisation")] [MinMaxRange(0f, 1f)]
// public RangedFloat SpatialBlend;
//
// public AudioRolloffMode RolloffMode = AudioRolloffMode.Logarithmic;
// [MinMaxRange(0.1f, 5f)] public RangedFloat MinDistance;
// [MinMaxRange(5f, 100f)] public RangedFloat MaxDistance;
// [MinMaxRange(0, 360)] public RangedFloat Spread;
// [MinMaxRange(0f, 5f)] public RangedFloat DopplerLevel;
//
// [Header("Ignores")] public bool BypassEffects = false;
// public bool BypassListenerEffects = false;
// public bool BypassReverbZones = false;
// public bool IgnoreListenerVolume = false;
// public bool IgnoreListenerPause = false;




