using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : MonoBehaviour
{
    public Transform player;
    public List<Transform> waypoints = new();
    public float StartingWaitingTime = 10f;
    public float TimeToForgetPlayer = 2f;
    public float HitRange = 1f;
    public int Damage = 2;
    
    private NavMeshAgent _agent;
    private FieldOfView _fov;
    private Health _health;
    private Health _playerHealth;
    private float _currentWaitingTime;
    private float _timeSawPlayer;

    private int _currentWaypoint = 0;
    private State _currentPatrolState;
    private HealthState _currentHealthState;
    
    private Animator _animator;

    private enum State
    {
        OnRoute,
        ChasingPlayer,
        Idle
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentWaitingTime = StartingWaitingTime;
        _currentHealthState = HealthState.Alive;
        _timeSawPlayer = 0;
        _currentPatrolState = State.Idle;
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _fov = GetComponent<FieldOfView>();
        _health = GetComponent<Health>();
        _playerHealth = player.GetComponent<Health>();
        if (waypoints.Count != 0)
        {
            SetNextWaypoint(_currentWaypoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckFollowPlayer(); 
        if (waypoints.Count != 0)
        {
            CheckFollowWayPoints();
        }
    }

    private void UpdateAnimation()
    {
        switch (_currentPatrolState)
        {
            case State.OnRoute:
                _animator.SetTrigger("Start Walking");
                break;
            case State.ChasingPlayer:
                _animator.SetTrigger("Chase");
                break;
            case State.Idle:
                _animator.SetTrigger("Stop Walking");
                break;
        }
    }

    private void HurtPlayerInRange()
    {
        if (Vector3.Distance(transform.position, player.position) <= HitRange)
        {
            _animator.SetTrigger("Attack");
            _playerHealth.TakeDamage(Damage);
            if (_playerHealth.IsDead())
            {
                GameManager.Instance.KillPlayer();
            }
            _animator.SetTrigger("Chase");
        }
    }

    private void CheckFollowPlayer()
    {
        if (_fov.canSeePlayer)
        {
            _timeSawPlayer = Time.time;
            _currentPatrolState = State.ChasingPlayer;
            _agent.destination = player.position;
            HurtPlayerInRange();
        }
        else if (_timeSawPlayer - Time.time >= TimeToForgetPlayer)
        {
            _currentPatrolState = State.Idle;
            _currentWaypoint = (_currentWaypoint + 1) % waypoints.Count;
            SetNextWaypoint(_currentWaypoint);
        }
    }

    private bool HasWaitAtWaypointTimerEnded()
    {
        _currentWaitingTime -= Time.deltaTime;
        return _currentWaitingTime <= 0;
    }

    private bool HasAgentArrived()
    {
        return _agent.remainingDistance <= _agent.stoppingDistance;
    }
    
    private void CheckFollowWayPoints()
    {
        if (HasAgentArrived())
        {
            UpdateAnimation();
            if (HasWaitAtWaypointTimerEnded())
            {
                _currentPatrolState = State.Idle;
                _currentWaypoint = (_currentWaypoint + 1) % waypoints.Count;
                SetNextWaypoint(_currentWaypoint);
                UpdateAnimation();   
            }
        }
    }

    private void SetNextWaypoint(int wayPointIndex)
    {
        if (_currentPatrolState == State.Idle)
        {
            _currentWaitingTime = StartingWaitingTime;
            _currentPatrolState = State.OnRoute;
            var nextDestination = waypoints[wayPointIndex].position;
            _agent.destination = nextDestination;   
        }
    }
}

public enum HealthState
{
    Alive,
    Dead
}
