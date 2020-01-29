using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFilterBySound : GFilterByDistance
{
    [SerializeField] private float _max_dist = 0.3f;

    [SerializeField] private float _min_dist = 0.2f;

    private AudioFile _guardianWarningSound;

    public GFilterBySound(GameObject guardian,
                                AudioFile guardianWarningSound,
                                float max_dist,
                                float min_dist) : base(guardian, max_dist, min_dist)
    {
        _guardianWarningSound = guardianWarningSound;
    }
    public override void Apply()
    {

        Debug.Assert(_guardianData, "The guardian was not instantiated");
        // If player enters minimal range :
        float distance = _guardianData.ClosestVertexDistance;
        if (distance <= _max_dist)
        {
            // Modulate volume based on closest guardian vertice location
            _guardianWarningSound.source.volume = 1 - Mathf.Pow(distance / _max_dist, 2);
            if (!_guardianWarningSound.source.isPlaying)
            {
                AudioManager.PlaySFX(_guardianWarningSound.audioName);
            }
        }
        else
        {
            if (_guardianWarningSound.source.isPlaying)
                AudioManager.StopSFX(_guardianWarningSound.audioName);
        }
    }

}
