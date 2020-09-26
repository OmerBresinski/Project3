using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{
    public Transform agentObject;
    public Vector3 agentPosition;
    public NavMeshAgent navAgent;
    public Vector3[] trophyPositions;
    public Vector3 trophyDestination; 
    public ThirdPersonCharacter character;
    void Start()
    {
        navAgent.updateRotation = false;
        agentObject = GetComponent<Transform>();
        initializeTrophyPositions();
        setNewDestination();
    }

    void Update()
    {
        agentPosition = agentObject.position;
        handleDestinationReached();
        if (navAgent.remainingDistance > navAgent.stoppingDistance)
            character.Move(navAgent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }

    void initializeTrophyPositions()
    {
        trophyPositions = new Vector3[5];
        trophyPositions[0] = GameObject.FindWithTag("GoldTrophy").transform.position;
        trophyPositions[1] = GameObject.FindWithTag("PlatinumTrophy").transform.position;
        trophyPositions[2] = GameObject.FindWithTag("DiamondTrophy").transform.position;
        trophyPositions[3] = GameObject.FindWithTag("EmeraldTrophy").transform.position;
        trophyPositions[4] = GameObject.FindWithTag("CopperTrophy").transform.position;
    }

    void setNewDestination()
    {
        trophyDestination = trophyPositions[Random.Range(0, trophyPositions.Length)];
        navAgent.SetDestination(trophyDestination);
    }

    void handleDestinationReached()
    {
        if (Mathf.Abs(agentPosition.x - trophyDestination.x) < 1 && Mathf.Abs(agentPosition.z - trophyDestination.z) < 1)
        {
            setNewDestination();
        }
    }
}
