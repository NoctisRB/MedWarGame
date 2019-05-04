using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio; //ToDouble

public class troopScript : MonoBehaviour
{
    [SerializeField]
    private float _attack = 0;

    [SerializeField]
    private float _hp = 0;

    [SerializeField]
    private float _attackRangeBase = 0;

    [SerializeField]
    private float _attackRangeTroop = 0;

    [SerializeField]
    private float _alertRange = 0;

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

    private bool _canAttack = true;

    public enum TroopType
    {
        Dwarf,
        Elve,
        Orc,
        Wizard
    };

    [SerializeField]
    private GameObject _orcProjectile;

    [SerializeField]
    private GameObject _elveProjectile;

    [SerializeField]
    private GameObject _wizardProjectile;

    [HideInInspector]
    public AudioManager _audioManager; //ToDouble

    public enum State
    {
        Idle,
        MoveTo,
        Attack
    };

    [SerializeField]
    private State _currentState;

    [SerializeField]
    private TroopType _troopType;

    private GameObject[] _enemies;
    private GameObject[] _enemyBases;

    // Start is called before the first frame update
    void Start()
    {
        _destination = new Vector3(10000.0f, 100000.0f, 100000.0f);
        _agent = this.GetComponent<NavMeshAgent>();
        ChangeState(State.Idle);
        _audioManager = FindObjectOfType<AudioManager>();
        switch (Random.Range(0,3))
        {
            case 1:
                _audioManager.Play("Spawn");
                break;
            case 2:
                _audioManager.Play("Spawn2");
                break;
            case 3:
                _audioManager.Play("Spawn3");
                break;
            default:
                _audioManager.Play("Spawn");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_hp <= 0)
        {
            this.gameObject.SetActive(false);
        }


        _enemies = FindEnemies();
        _enemyBases = FindEnemyBase();

        if (_currentState == State.Idle)
        {
            foreach (var enemy in _enemies)
            {
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < _attackRangeTroop)
                {
                    _target = enemy;
                    ChangeState(State.Attack);
                    break;
                }
                if (Vector3.Distance(enemy.transform.position, this.transform.position) < _alertRange)
                {
                    _target = enemy;
                    ChangeState(State.MoveTo);
                    break;
                }
            }
            foreach (var enemyBase in _enemyBases)
            {
                if (Vector3.Distance(enemyBase.transform.position, this.transform.position) < _attackRangeBase)
                {
                    _target = enemyBase;
                    ChangeState(State.Attack);
                    break;
                }
                if (Vector3.Distance(enemyBase.transform.position, this.transform.position) < _alertRange)
                {
                    _target = enemyBase;
                    ChangeState(State.MoveTo);
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
                if (Vector3.Distance(enemy.transform.position, this.transform.position) > _attackRangeTroop)
                {
                    break;
                }
            }
            
            if (_canAttack)
            {
                Hurt(_target);
                Invoke("ResetHurt", 1.0f);
                _canAttack = false;
            }


        }
        else if (_currentState == State.MoveTo)
        {
            if (_target != null)
            {
                _agent.destination = _target.transform.position;

                foreach (var enemy in _enemies)
                {
                    if (Vector3.Distance(enemy.transform.position, this.transform.position) < _attackRangeTroop)
                    {
                        _target = enemy;
                        ChangeState(State.Attack);
                        break;
                    }
                }
                foreach (var enemyBase in _enemyBases)
                {
                    if (Vector3.Distance(enemyBase.transform.position, this.transform.position) < _attackRangeBase)
                    {
                        _target = enemyBase;
                        ChangeState(State.Attack);
                        break;
                    }
                }

            }
            else { ChangeState(State.Idle); }
            
        }

    }

    private GameObject[] FindEnemies()
    {
        var Enemies = GameObject.FindGameObjectsWithTag("Enemy");
       
        return Enemies;
    }
    private GameObject[] FindEnemyBase()
    {
        var EnemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        return EnemyBases;
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
                _animator.SetBool("attack", false);
                _animator.SetBool("run", false);
                _animator.SetBool("idle", true);
                _agent.speed = 0;
                break;
            case State.MoveTo:
                _animator.SetBool("attack", false);
                _animator.SetBool("idle", false);
                _animator.SetBool("run", true);
                _agent.speed = _speed;
                _agent.destination = _destination;
                break;
            case State.Attack:
                _animator.SetBool("idle", false);
                _animator.SetBool("run", false);
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

    public void MoveTo(GameObject target)
    {
        
        SetDestination(target.transform.position);
        _target = target;
        ChangeState(State.MoveTo);
    }

    private void Hurt(GameObject enemy)
    {
        if (enemy==null)
        {
            _target = null;
            return;
        }
        if (enemy.activeSelf)
        {
            switch (_troopType)
            {
                case TroopType.Dwarf:
                    break;
                case TroopType.Elve:
                    GameObject Elveprojectile = Instantiate(_elveProjectile, this.transform.position, Quaternion.identity);
                    Elveprojectile.GetComponent<ProjectileShot>().MoveTo((_target.transform.position - this.transform.position).normalized);
                    break;
                case TroopType.Orc:
                    GameObject Orcprojectile = Instantiate(_orcProjectile, this.transform.position, Quaternion.identity);
                    Orcprojectile.GetComponent<ProjectileShot>().MoveTo((_target.transform.position - this.transform.position).normalized);
                    break;
                case TroopType.Wizard:
                    GameObject Wizardprojectile = Instantiate(_wizardProjectile, this.transform.position, Quaternion.identity);
                    Wizardprojectile.GetComponent<ProjectileShot>().MoveTo(( _target.transform.position - this.transform.position).normalized);
                    break;
                default:
                    break;
            }

            if (enemy.tag == "EnemyBase")
            {
                enemy.GetComponent<treeScript>().SetHP(-_attack);
            }
            else
            {
                enemy.GetComponent<enemyTroopScript>().SetHP(-_attack);
            }
        }
        else {
            ChangeState(State.Idle);
            _target = null;
        }
        
    }
    private void ResetHurt()
    {
        _canAttack = true;
    }
}