using UnityEngine;

namespace CTIN_406L_Starter_Pack.Scriptable_Objects.Audio
{
	public abstract class AudioEvent : ScriptableObject
	{
		public static string nowPlaying;
		public abstract void Play(AudioSource source);
	}
}
