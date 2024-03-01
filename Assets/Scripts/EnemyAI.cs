using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public List<Transform> patrolPoints;

    private NavMeshAgent _navMeshAgent;

    public PlayerController player;

    private bool _isPlayerNoticed;

    public float viewAngle;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        PickNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {      
        NoticePlayerUpdate();

        ChaseUpdate();

        PatrolUpdate();  
    }

    private void PickNewPatrolPoint()
    {      
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;             
    }

    private void PatrolUpdate()
    {
        if (_navMeshAgent.remainingDistance == 0)
        {
            PickNewPatrolPoint();
        }
    }
    private void NoticePlayerUpdate()
    {
        var direction = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, direction) < viewAngle)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
                else
                {
                    _isPlayerNoticed = false;
                }
            }
        }
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }
}
