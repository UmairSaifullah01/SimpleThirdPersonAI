using System;
using UnityEngine.Audio;
using UnityEngine;


namespace UMGS.SoundSystem
{


	[Serializable]
	public class Sound
	{

		public string      name;
		public SoundType   type = SoundType.SFX;
		public AudioClip   audioClip;
		public AudioClip[] audioClips;

		public               bool  loop           = false;
		public               bool  playRandomClip = false;
		public               bool  playOnAwake    = false;
		[Range(0, 1)] public float volume         = 1;

		[Range(0.1f, 3)] public float pitch = 1;

		[HideInInspector] public AudioSource source;

	}

	public enum SoundType
	{

		SFX,
		UI,
		Music

	}


}