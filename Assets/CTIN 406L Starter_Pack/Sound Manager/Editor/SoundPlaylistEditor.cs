using UnityEditor;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Sound_Manager.Editor
{
    [CustomEditor(typeof(SoundObjectPlaylist), true)]
    public class SoundPlaylistEditor : UnityEditor.Editor
    {
    
   
        public override void OnInspectorGUI()
        {EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Play"))
            {
                ((SoundObjectPlaylist) target).CheckAudioSource();
                ((SoundObjectPlaylist) target).StartMyCoroutine();
            }
            if (GUILayout.Button("Stop"))
            {
                ((SoundObjectPlaylist) target).StopMyCoroutine();
            }
            EditorGUI.EndDisabledGroup();
            DrawDefaultInspector();


        
        }
    }
}
