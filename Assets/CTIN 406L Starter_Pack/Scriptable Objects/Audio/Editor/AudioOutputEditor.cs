using UnityEditor;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Scriptable_Objects.Audio.Editor
{
	[CustomEditor(typeof(AudioPlaylist), true)]
	public class AudioOutputEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
			if (GUILayout.Button("Play"))
			{
				((AudioPlaylist) target).CheckAudioSource();
				((AudioPlaylist) target).StartMyCoroutine();
			}
			if (GUILayout.Button("Stop"))
			{
				((AudioPlaylist) target).StopMyCoroutine();
			}
			EditorGUI.EndDisabledGroup();
			DrawDefaultInspector();


		}
	}
}
