using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public enum SpatialBlendType { Blend2D, Blend3D };

[CreateAssetMenu(menuName = "Audio Events/Custom Audio")]
public class CustomAudioEvent : AudioEvent
{
	public AudioClip[] myClips;
	public AudioMixerGroup[] myMixer;
	public bool Mute = false;

	public SpatialBlendType spatialBlend;
	[MinMaxRange(0f, 1f)] public RangedFloat Volume;
	[MinMaxRange(-3f, 3f)] public RangedFloat Pitch;
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


	public override void Play(AudioSource source)
	{
		if (myClips.Length == 0) return;
		if (myMixer.Length != 0)
			source.outputAudioMixerGroup = myMixer[Random.Range(0, myMixer.Length)];

		CheckSpatialBlend(source);
		source.clip = myClips[Random.Range(0, myClips.Length)];
		AudioEvent.nowPlaying = source.clip.name;
		source.volume = Random.Range(Volume.minValue, Volume.maxValue);
		source.pitch = Random.Range(Pitch.minValue, Pitch.maxValue);
		source.Play();
	}

	private void CheckSpatialBlend(AudioSource source)
	{
		source.spatialBlend = spatialBlend == SpatialBlendType.Blend2D ? 0 : 1;
	}
}


