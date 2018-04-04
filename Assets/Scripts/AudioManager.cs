using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public Sound[] sounds;

	// Use this for initialization
	void Start () {
		foreach(Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
		}
	}
	
	public void PlaySound(string soundName)
	{
		try
		{
			Sound s = Array.Find(sounds, sound => sound.name == soundName);
			s.source.Play();
		} 
		catch (Exception e)
		{
			Debug.LogWarning("Error playing sound: " + soundName + "\n " + "Are you missing an audiofile?");
		}
	}
}