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
    bool isMoving = false;
    void Awake()
    {
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }
    void Start()
    {
       for (int i = 0; i < partyMembers.Length; i++)
        { if (partyMembers[i] != null)
            {     
                partyMembers[i] = Instantiate(partyMembers[i], this.transform);
            }
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
            if ((!agent.hasPath || agent.velocity.sqrMagnitude == 0f)&&isMoving)
            {
                agent.ResetPath();
                agent.isStopped = true;
                isMoving = false;
                DungeonController.Instance.Fight();
                MapManager.Instance.mapSpawner.DestroyMap();
                foreach (PlayerBase member in partyMembers)
                {
                    if (member != null)
                    {
                        member.animator.SetBool("isMove", false);
                    }
                }
            }
        }
    }

    public void MoveParty(Transform nextPartyPoint)
    {
        if (nextPartyPoint != null)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(nextPartyPoint.position);
            if (!agent.isOnNavMesh)
            {
                Debug.LogError("Agent is not on the NavMesh.");
            }

            if (agent.isStopped)
            {
                Debug.Log("Agent is stopped.");
            }
            foreach (PlayerBase member in partyMembers)
            {
                if (member != null)
                {
                    member.animator.SetBool("isMove", true);
                }
            }
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
            else
            {
                Debug.Log("No member in party");
            }
        }
    }
}
