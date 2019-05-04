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

    [SerializeField]
    private Animator _animator;

    
    public enum State
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
        _destination = new Vector3(10000.0f, 100000.0f, 100000.0f);
        _agent = this.GetComponent<NavMeshAgent>();
        ChangeState(State.Idle);
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
                
                if (Vector3.Distance(_destination, this.transform.position) < _attackRange)
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
                if (Vector3.Distance(enemy.transform.position, this.transform.position) > _attackRange)
                {
                    break;  
                }
                
                
            }
        }
        else if (_currentState == State.MoveTo)
        {
            foreach (var enemy in _enemies)
            {
                
                if (Vector3.Distance(_destination, this.transform.position) < _attackRange)
                {
                    
                    _target = enemy;
                    ChangeState(State.Attack);
                    break;
                }
                else if (Vector3.Distance(_destination, this.transform.position) > _attackRange)
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

    public void ChangeState(State nextState)
    {

        switch (_currentState)
        {
            case State.Idle:
                _animator.SetBool("idle", false);
                break;
            case State.MoveTo:
                _animator.SetBool("run", false);
                break;
            case State.Attack:
                _animator.SetBool("attack", false);
                _target = null;
                break;
            default:
                break;
        }

        switch (nextState)
        {
            case State.Idle:
                _animator.SetBool("idle", true);
                _agent.speed = 0;
                break;
            case State.MoveTo:
                _animator.SetBool("run", true);
                _agent.speed = _speed;
                _agent.destination = _destination;         
                break;
            case State.Attack:
                _animator.SetBool("attack", true);
                _agent.speed = 0;             
                break;
            default:
                break;
        }

        _currentState = nextState;

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

    public float GetCost()
    {
        return _cost;
    }

    public void MoveTo(Vector3 pos)
    {
        Debug.Log("MOVE");
        SetDestination(pos);
        ChangeState(State.MoveTo);
    }

}
