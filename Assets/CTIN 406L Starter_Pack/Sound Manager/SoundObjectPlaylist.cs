using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using Random = UnityEngine.Random;
namespace CTIN_406L_Starter_Pack.Sound_Manager
{
    public class SoundObjectPlaylist : MonoBehaviour
    {
         public enum LoopThroughSO
        {
            SinglePlay,
            LoopThrough
        }
        [Tooltip("Loop through the entire Scriptable Object")]
        public LoopThroughSO loop;
       
       
        public enum PlayType
        {
            Sequential,
            Random
        };
        [Tooltip("Sequential starts with the First SO to the Last")]
        public PlayType playType;
        [SerializeField] private AudioSource m_AudioSource;
        public List<MultipleSoundsObject> audioListSO;
        public MultipleSoundsObject currentSO;

        private bool isPlaying = true;
        private int clipIndex = 0;
        private int listSO = 0;


        // Start is called before the first frame update
        void Start()
        {
            CheckAudioSource();
        }

        public void CheckAudioSource()
        {
            if (gameObject.GetComponent<AudioSource>() != null)
            {
                m_AudioSource = gameObject.GetComponent<AudioSource>();
                m_AudioSource.playOnAwake = false;
            }
            else
            {
                gameObject.AddComponent<AudioSource>();
                m_AudioSource = gameObject.GetComponent<AudioSource>();
            }

            m_AudioSource.playOnAwake = false;

            currentSO = audioListSO[0];
        }

        // Update is called once per frame
        void Update()
        {
            CheckPlayType();
        }

        private void PlaySequentially()
        {
            if (!m_AudioSource.isPlaying)
            {
                if (currentSO != null)
                {
                    if (listSO >= audioListSO.Count)
                    {
                        listSO = 0;
                        m_AudioSource.Stop();
                        currentSO = null;
                        TMP_EditorCoroutine.StartCoroutine(DestroyAudioSource());
                        return;
                    }
                    currentSO = audioListSO[listSO];
                    currentSO.Play(m_AudioSource);
                    listSO++;
                }
                
            }
        }

        IEnumerator DestroyAudioSource()
        {
            yield return new WaitForSeconds(1.0f);
            DestroyImmediate(m_AudioSource);
            m_AudioSource = null;
        }

        private void StartPlayingRandom()
        {
            if (!m_AudioSource.isPlaying)
            {
                if (currentSO != null)
                {
                    if (clipIndex >= currentSO.myAudioClips.Length)
                    {
                        clipIndex = 0;
                        currentSO = audioListSO[Random.Range(0, audioListSO.Count)];
                        if (listSO >= audioListSO.Count)
                        {
                            listSO = 0;
                        }
                    }
                }
                currentSO.Play(m_AudioSource);
                clipIndex++;
            }
        }

        private void PlaySequentiallyLoop()
        {
            if (!m_AudioSource.isPlaying)
            {
                if (currentSO != null)
                {
                    if (listSO >= audioListSO.Count)
                    {
                        listSO = 0;
                    }
                    currentSO = audioListSO[listSO];
                    currentSO.Play(m_AudioSource);
                    listSO++;
                }
            }
            //Redundant Functionality
            // if (!m_AudioSource.isPlaying)
            // {
            //     if (currentSO != null)
            //     {
            //         if (clipIndex >= currentSO.myClips.Length)
            //         {
            //             clipIndex = 0;
            //             if (listSO >= audioListSO.Count)
            //             {
            //                 listSO = 0;
            //             }
            //             currentSO = audioListSO[listSO];
            //             currentSO.ResetClipIndex();
            //             listSO++;
            //         }
            //     }
            //     currentSO.Play(m_AudioSource);
            //     clipIndex++;
            // }
        }

        public void StartMyCoroutine()
        {
            isPlaying = true;
            ResetPlaylist();
            TMP_EditorCoroutine.StartCoroutine(MyCoroutine());
        }

        IEnumerator MyCoroutine()
        {
            while (isPlaying)
            {
                CheckPlayType();
                yield return new WaitForSeconds(1f);
            }
        }

        private void CheckPlayType()
        {
            if (m_AudioSource)
            {
                if (playType == PlayType.Random && loop == LoopThroughSO.LoopThrough)
                {
                    StartPlayingRandom();
                }

                if (playType == PlayType.Random && loop == LoopThroughSO.SinglePlay)
                {
                    PlaySequentially();
                }

                if (playType == PlayType.Sequential && loop == LoopThroughSO.SinglePlay)
                {
                    PlaySequentially();
                }

                if (playType == PlayType.Sequential && loop == LoopThroughSO.LoopThrough)
                {
                    PlaySequentiallyLoop();
                }
            }
        }

        public void StopMyCoroutine()
        {
            isPlaying = false;
            m_AudioSource.Stop();
            ResetPlaylist();
        }

        public void ResetPlaylist()
        {
            clipIndex = 0;
            listSO = 0;
            currentSO=audioListSO[listSO];
            currentSO.ResetClipIndex();
        }
    }
}

