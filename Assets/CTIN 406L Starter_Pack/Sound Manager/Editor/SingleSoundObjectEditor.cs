using UnityEditor;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Sound_Manager.Editor
{
    [CustomEditor(typeof(SingleSoundObject), true)]
    public class SingleSoundObjectEditor : UnityEditor.Editor
    {
        string gameobjectName = "-> Single Sound Object Preview <-";
        [SerializeField] private AudioSource play;


        public void OnEnable()
        {
            play = EditorUtility.CreateGameObjectWithHideFlags
            (gameobjectName, HideFlags.None,
                typeof(AudioSource)).GetComponent<AudioSource>();
			
            ((SingleSoundObject) target).SetDefaultParameters();
        }

        public void OnDisable()
        {
            if (play.gameObject)
            {
                DestroyImmediate(play.gameObject);
                
            }
        }

        public override void OnInspectorGUI()
        {


            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Play"))
            {
                ((SingleSoundObject) target).Play(play);
                play.name = "-> "+ SingleSoundObject.nowPlaying + " <-";
            }
            if (GUILayout.Button("Stop"))
            {
                play.Stop();
            }
            

            EditorGUI.EndDisabledGroup();
            DrawDefaultInspector();
            if (GUILayout.Button("Reset Volume & Pitch"))
            {
                ((SingleSoundObject) target).Reset();
            }
        }
    }
}