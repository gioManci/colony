using UnityEngine;
using System.Collections.Generic;

namespace Colony.Sounds {

[RequireComponent(typeof(AudioSource))]
public class SoundEffects : MonoBehaviour {

	public enum Sound {
		BeeMove,
		ButtonClicked,
		Attacked
	}

	public AudioClip BeeMoveSound;
	public AudioClip ButtonClickedSound;
	public AudioClip AttackedSound;

	private AudioSource source;
	private Dictionary<Sound, AudioClip> sounds;

	// Make this class a singleton
	public static SoundEffects Instance { get; private set; }

	private SoundEffects() {}

	void Awake() {
		if (Instance != null && Instance != this)
			Destroy(gameObject);
		Instance = this;
	}

	void Start() {
		source = GetComponent<AudioSource>();
		sounds = new Dictionary<Sound, AudioClip>() {
			{ Sound.BeeMove, BeeMoveSound },
			{ Sound.ButtonClicked, ButtonClickedSound },
			{ Sound.Attacked, AttackedSound }
		};
	}

	public void Play(Sound snd, float volume = 1f) {
		if (!source.isPlaying)
			source.PlayOneShot(sounds[snd], volume);
	}

	public void PlayButtonSound() {
		Play(Sound.ButtonClicked);
	}
}

}