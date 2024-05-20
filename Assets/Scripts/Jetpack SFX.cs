using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackSFX : MonoBehaviour
{
    Rigidbody2D rb = null;
    public AudioSource jetpack;
    float desiredEnginePitch = 0.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        update_engine();
    }

    private void update_engine()
    {
        float velocityMagnitude = rb.velocity.y;
        float DesiredEngineVolume = velocityMagnitude * 0.5f;
        DesiredEngineVolume = Mathf.Clamp(DesiredEngineVolume, 0.2f, 0.1f);
        jetpack.volume = Mathf.Lerp(jetpack.volume, DesiredEngineVolume, Time.deltaTime * 10);
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2.0f);
        jetpack.pitch = Mathf.Lerp(jetpack.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }
}
