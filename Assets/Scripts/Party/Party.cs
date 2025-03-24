using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public PlayerBase[] partyMembers;
    void Start()
    {
        partyMembers = new PlayerBase[3];
    }

    void Update()
    {
        
    }
}
