using UnityEngine;

public abstract class AudioEvent : ScriptableObject
{

	public static string nowPlaying;
	public abstract void Play(AudioSource source);
}
