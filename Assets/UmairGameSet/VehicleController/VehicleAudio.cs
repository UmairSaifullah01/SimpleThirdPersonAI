using UMGS.SoundSystem;
using UnityEngine;


namespace UMGS.Vehicle
{


	[System.Serializable]
	public class VehicleAudio
	{

		[Header("Pitch Parameter")] public float       flatoutSpeed = 20.0f;
		[Range(0.0f, 3.0f)]         public float       minPitch     = 0.7f;
		[Range(0.0f, 0.1f)]         public float       pitchSpeed   = 0.05f;
		[Header("Clips")]           public AudioClip   rolling;
		public                             AudioClip   impact, skid;
		[Header("Root")] public            Transform   audioContainer;
		private                            AudioSource engineSource;
		private                            AudioSource othersSource;
		ControlInput                                   _input;

		AudioSource skidSource, impactSource;

		public void Initialize(ControlInput input)
		{
			_input            = input;
			engineSource      = rolling.Play(SoundType.SFX, audioContainer);
			engineSource.loop = true;
			othersSource      = SoundExtensions.CreateAudioSource("OtherSource", audioContainer, SoundType.SFX);
		}

		public void DoUpdate(float speed)
		{
			engineSource.pitch = Mathf.Clamp(Mathf.Lerp(engineSource.pitch, minPitch + 0.5f * _input.throttle + Mathf.Abs(speed) / flatoutSpeed, pitchSpeed), minPitch, 2);
		}

		public void ImpactAudio(Vector3 atPoint)
		{
			// PlayOneTime(impact, atPoint, 0.5f, 1);
			if (!impactSource)
				impactSource = CreateAudioSource(atPoint);
			PlayIfNoPlaying(impactSource, impact, atPoint, 0.5f, 1);
		}

		public void SkidAudio(Vector3 atPoint, float volume)
		{
			if (!skidSource)
				skidSource = CreateAudioSource(atPoint);
			PlayIfNoPlaying(skidSource, skid, atPoint, volume * 0.2f, 1);
		}

		void PlayIfNoPlaying(AudioSource source, AudioClip clip, Vector3 position, float volume, float pitch)
		{
			source.transform.position = position;
			if (source.isActiveAndEnabled && !source.isPlaying)
			{
				source.clip         = clip;
				source.volume       = volume;
				source.pitch        = pitch;
				source.dopplerLevel = 0.0f; // Doppler causes artifacts as for positioning the audio source
				source.Play();
			}
		}

		void PlayOneTime(AudioClip clip, Vector3 position, float volume, float pitch)
		{
			if (pitch < 0.01f || volume < 0.01f) return;
			AudioSource source = Object.Instantiate(othersSource, position, Quaternion.identity, othersSource.transform.parent);
			if (source.isActiveAndEnabled)
			{
				source.clip         = clip;
				source.volume       = volume;
				source.pitch        = pitch;
				source.dopplerLevel = 0.0f; // Doppler causes artifacts as for positioning the audio source
				source.Play();
			}

			Object.Destroy(source.gameObject, clip.length / pitch);
		}

		AudioSource CreateAudioSource(Vector3 position)
		{
			AudioSource source = Object.Instantiate(othersSource, position, Quaternion.identity, othersSource.transform.parent);
			return source;
		}

	}


}