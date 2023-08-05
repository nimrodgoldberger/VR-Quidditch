using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreArea : Targetable
{
    [SerializeField] private GameObject particleEffectPrefab;
    [SerializeField] private int team;
    [SerializeField] GameObject scoreManager;
    void OnTriggerEnter(Collider otherCollider)
    {
        if(otherCollider.gameObject.name == "Quaffle")
        {
            ParticleSystem particleSystemInstance = Instantiate(particleEffectPrefab, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            StartCoroutine(StopParticleEffect(particleSystemInstance));
        }
        scoreManager.GetComponent<ScoreManager>().AddScore(10, team);
    }

    IEnumerator StopParticleEffect(ParticleSystem particleSystem)
    {
        // Wait for the specified duration.
        yield return new WaitForSeconds(3f);

        // Stop the particle system.
        particleSystem.Stop();

        // Optionally, you can destroy the GameObject after stopping the particle system.
        Destroy(particleSystem.gameObject, particleSystem.main.duration);
    }
}

