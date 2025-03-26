using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Party : MonoBehaviour
{
    public Player[] partyMembers;
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

    /// <summary>
    /// 파티 이동용 업데이트
    /// 도착하면 반환값
    /// </summary>
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
                foreach (Player member in partyMembers)
                {
                    if (member != null)
                    {
                        member.animator.SetBool("isMove", false);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 파티 이동용 Ai네비게이션 사용
    /// </summary>
    /// <param name="nextPartyPoint"></param>
    public void MoveParty(Transform nextPartyPoint)
    {
        if (nextPartyPoint != null)
        {
            isMoving = true;
            agent.isStopped = false;
            agent.SetDestination(nextPartyPoint.position);
            foreach (Player member in partyMembers)
            {
                if (member != null)
                {
                    member.animator.SetBool("isMove", true);
                }
            }
        }
    }
    
    /// <summary>
    /// 파티맴버들에게 전달받은 타겟 재전달
    /// </summary>
    /// <param name="target"></param>
    public void GetTarget(MonsterBase target)
    {
        foreach (Player member in partyMembers)
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
