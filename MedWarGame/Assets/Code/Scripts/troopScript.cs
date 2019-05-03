using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class troopScript : MonoBehaviour
{
    [SerializeField]
    private float _attack = 0;

    [SerializeField]
    private float _hp = 0;

    [SerializeField]
    private float _attackRange = 0;

    [SerializeField]
    private float _speed = 0;

    [SerializeField]
    private float _cost = 0;

    [SerializeField]
    private float _deployRange = 0;

    private NavMeshAgent _agent;
    private GameObject _target;
    private Vector3 _destination;

    
    private enum State
    {
        Idle,
        MoveTo,
        Attack
    };

    [SerializeField]
    private State _currentState; 

    private GameObject[] _enemies;

    // Start is called before the first frame update
    void Start()
    {
        _agent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp<= 0)
        {
            this.gameObject.SetActive(false);
        }

        _enemies = FindEnemies();

        if (_currentState == State.Idle)
        {
            foreach (var enemy in _enemies)
            {
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < _attackRange)
                {
                    _target = enemy;
                    ChangeState(State.Attack);
                    break;
                }
            }
        }
        else if (_currentState == State.Attack)
        {
            if (_target == null)
            {
                ChangeState(State.Idle);
            }
            foreach (var enemy in _enemies)
            {
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < _attackRange)
                {
                    break;  
                }
                ChangeState(State.Idle);
            }
        }

    }

    private GameObject[] FindEnemies()
    {
        var Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return Enemies;
    }

    private void ChangeState(State nextState)
    {

        switch (_currentState)
        {
            case State.Idle:
                break;
            case State.MoveTo:
                break;
            case State.Attack:
                _target = null;
                break;
            default:
                break;
        }

        switch (nextState)
        {
            case State.Idle:
                _currentState = nextState;
                _agent.speed = 0;
                break;
            case State.MoveTo:
                _agent.speed = _speed;
                _agent.destination = _target.transform.position;
                _currentState = nextState;
                break;
            case State.Attack:
                _currentState = nextState;
                break;
            default:
                break;
        }

    }


    public void SetDestination(Vector3 dest)
    {
        _destination = dest;
    }

    public void SetHP(float hp)
    {
        _hp += hp;
    }

    public float GetDeployRange()
    {
        return _deployRange;


}
