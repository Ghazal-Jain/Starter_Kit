using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AudioOutput), true)]
public class AudioOutputEditor : Editor
{
	public override void OnInspectorGUI()
	{EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
		if (GUILayout.Button("Preview"))
		{
			((AudioOutput) target).CheckAudioSource();
			((AudioOutput) target).StartMyCoroutine();
		}
		if (GUILayout.Button("Stop"))
		{
			((AudioOutput) target).StopMyCoroutine();
		}
		EditorGUI.EndDisabledGroup();
		DrawDefaultInspector();


	}
}
