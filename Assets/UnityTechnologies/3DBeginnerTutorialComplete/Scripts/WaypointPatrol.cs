using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    public bool isStunned = false;

    public ParticleSystem hitParticles;
    public AudioSource hitSound;

    private bool wasStunned = false;

    int m_CurrentWaypointIndex;

    void Start ()
    {
        navMeshAgent.SetDestination(waypoints[0].position);

        if (hitParticles != null)
            hitParticles.Stop();
    }

    void Update ()
    {
        // SOUND TRIGGER
        if (isStunned && !wasStunned)
        {
            if (hitSound != null)
            {
                hitSound.Play();
            }
        }

        wasStunned = isStunned;

        // PARTICLES
        if (hitParticles != null)
        {
            if (isStunned)
            {
                if (!hitParticles.isPlaying)
                    hitParticles.Play();
            }
            else
            {
                if (hitParticles.isPlaying)
                    hitParticles.Stop();
            }
        }

        // 🚫 STOP MOVEMENT
        if (isStunned)
        {
            navMeshAgent.isStopped = true;
            return;
        }
        else
        {
            navMeshAgent.isStopped = false;
        }

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}