using UniRx;
using UnityEngine;

namespace DyrdaIo.FirstPersonController
{
    [RequireComponent(typeof(AudioSource))]
    public class CharacterMovementAudio : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject characterSignalsInterfaceTarget;
        private ICharacterSignals _characterSignals;
        private AudioSource _audioSource;

        [Header("Sounds")]
        [SerializeField] private AudioClip[] footstepSounds;
        [SerializeField] private AudioClip[] jumpedSounds;
        [SerializeField] private AudioClip[] landedSounds;

        private void PlayAudioClip(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }

        private void Awake()
        {
            _characterSignals = characterSignalsInterfaceTarget.GetComponent<ICharacterSignals>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _characterSignals.Stepped
                .SelectAlternating(footstepSounds)
                .Subscribe(clip => PlayAudioClip(clip))
                .AddTo(this);

            _characterSignals.Jumped
                .SelectAlternating(footstepSounds)
                .Subscribe(clip => PlayAudioClip(clip))
                .AddTo(this);

            _characterSignals.Landed
                .SelectAlternating(footstepSounds)
                .Subscribe(clip => PlayAudioClip(clip))
                .AddTo(this);
        }
    }
}