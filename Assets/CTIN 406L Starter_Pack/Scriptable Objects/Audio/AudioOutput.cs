using System;
using System.Collections;
using System.Collections.Generic;
using Hellmade.Sound;
using TMPro.EditorUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioOutput : MonoBehaviour
{
	private bool isPlaying = true;
	[SerializeField] private AudioSource m_AudioSource;
	public List<AudioEvent> audioListSO;
	public AudioEvent currentSO;

	public bool isRandom = false;
	public bool isLooping = false;
	public int x = 5;

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
			x = 6;
		    m_AudioSource.playOnAwake = false;
	    }
	    else
	    {
		    gameObject.AddComponent<AudioSource>();
		    m_AudioSource = gameObject.GetComponent<AudioSource>();
	    }
	    m_AudioSource.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
	    StartPlaying();
    }

    private void StartPlaying()
    {
	    if (!m_AudioSource.isPlaying)
	    {
		    if(isRandom)
				currentSO = audioListSO[Random.Range(0, audioListSO.Count)];
		    else
		    {
			    PlaySequentially();
		    }
			currentSO.Play(m_AudioSource);
	    }

    }

    private void PlaySequentially()
    {

    }

    public void StartMyCoroutine()
    {
	    isPlaying = true;
	    TMP_EditorCoroutine.StartCoroutine(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
	    while (isPlaying)
	    {
		    StartPlaying();
		    yield return new WaitForSeconds(0.5f);
	    }

    }

    public void StopMyCoroutine()
    {
	    isPlaying = false;
	    m_AudioSource.Stop();
    }
}
