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

    public float damage = 30;

    private PlayerHealth _playerHealth;

   




    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _playerHealth = player.GetComponent<PlayerHealth>();

        PickNewPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {      
        NoticePlayerUpdate();

        ChaseUpdate();

        AttackUpdate();

        PatrolUpdate();  
    }

    private void PickNewPatrolPoint()
    {      
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;             
    }

    private void PatrolUpdate()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
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

    private void AttackUpdate()
    {
        if (_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _playerHealth.DealDamage(damage * Time.deltaTime);

            }
        }
    }
}
