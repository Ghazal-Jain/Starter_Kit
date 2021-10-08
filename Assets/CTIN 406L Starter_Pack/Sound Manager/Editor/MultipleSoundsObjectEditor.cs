using UnityEditor;
using UnityEngine;

namespace CTIN_406L_Starter_Pack.Sound_Manager.Editor
{
    [CustomEditor(typeof(MultipleSoundsObject), true)]
    public class MultipleSoundsObjectEditor : UnityEditor.Editor
    {
        string gameobjectName = "-> Single Sound Object Preview <-";
        [SerializeField] private AudioSource play;


        public void OnEnable()
        {
            play = EditorUtility.CreateGameObjectWithHideFlags
            (gameobjectName, HideFlags.DontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
			
            ((MultipleSoundsObject) target).SetDefaultParameters();
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
                ((MultipleSoundsObject) target).Play(play);
                play.name = "-> "+ MultipleSoundsObject.nowPlaying + " <-";
            }
            if (GUILayout.Button("Stop"))
            {
                play.Stop();
            }
            

            EditorGUI.EndDisabledGroup();
            DrawDefaultInspector();
            if (GUILayout.Button("Reset Volume & Pitch"))
            {
                ((MultipleSoundsObject) target).Reset();
            }
        }
    }
}