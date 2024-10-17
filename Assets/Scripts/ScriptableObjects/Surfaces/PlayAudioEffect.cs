using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects.Surfaces
{
    public class PlayAudioEffect : ScriptableObject
    {
        public AudioSource audioSourcePrefab;
        public List<AudioClip> audioClips;
        [FormerlySerializedAs("VolumeRange")] [Tooltip("Values are clamped to 0-1")]
        public Vector2 volumeRange = new Vector2(0f, 1f);
    }
}