using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed;
    NavMeshAgent agent;
    //float moveSpeed;
    // Start is called before the first frame update
    void Start()
    { 
        agent = GetComponent<NavMeshAgent>();
        moveSpeed = agent.speed; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < 15.0f)
        {
            agent.destination = target.position;
            agent.speed = moveSpeed;
        }
        else
        {
            agent.speed = 0;
        }
    }
}
