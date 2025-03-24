using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Party : MonoBehaviour
{
    public PlayerBase[] partyMembers;
    private NavMeshAgent agent;
    void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
    void Start()
    {
        //partyMembers = new PlayerBase[3];
        foreach (PlayerBase member in partyMembers)
        {
            if(member!=null)
            Instantiate(member, this.transform);
        }
    }

    void Update()
    {
        if (agent.pathPending)
        {
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                agent.ResetPath();
                agent.isStopped = true;
            }
        }
    }

    public void MoveParty(Transform nextPartyPoint)
    {
        agent.SetDestination(nextPartyPoint.position);
        if (nextPartyPoint != null)
        {
            agent.SetDestination(nextPartyPoint.position);
        }
    }
    
    public void GetTarget(MonsterBase target)
    {
        foreach (PlayerBase member in partyMembers)
        {
            if (member != null)
            {
                member.GetTarget(target);
            }
        }

    }
}
