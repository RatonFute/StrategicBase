using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Dead
}

public class Enemy : Entity
{
    private NavMeshAgent _agent;

    private EnemyState _currentState;

    private GameObject _currentTarget;

    [SerializeField] private GameObject _bulletPrefab;

    private bool _inRange;
    private float _attackRange;
    private bool _isAttacking;
    private float _attackSpeed;

    public static event Action<GameObject> OnDied;
    protected void SetState(EnemyState newState)
    {
        if (_currentState == newState)
            return;

        _currentState = newState;
        EnterState(newState);
    }

    private void EnterState(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.Idle:
                _agent.isStopped = true;
                break;
            case EnemyState.Chasing:
                _agent.isStopped = false;
                break;
            case EnemyState.Attacking:
                _agent.isStopped = true;
                break;
            case EnemyState.Dead:
                _agent.enabled = false;
                Died();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    private IEnumerator UpdateState()
    {
        while (true)
        {
            switch (_currentState)
            {
                case EnemyState.Idle:
                    yield return UpdateIdleState();
                    break;
                case EnemyState.Chasing:
                    yield return UpdateChasingState();
                    break;
                case EnemyState.Attacking:
                    yield return UpdateAttackingState();
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator UpdateIdleState()
    {
        if (!_currentTarget)
            DetectTarget();
        else
            SetState(EnemyState.Chasing);

        yield return null;
    }

    private IEnumerator UpdateChasingState()
    {
        if (!_currentTarget)
        {
            SetState(EnemyState.Idle);
            yield return null;
        }

        DetectTarget();

        //TODO play animation

        if (_agent.destination != _currentTarget.transform.position)
        {
            _agent.SetDestination(_currentTarget.transform.position);
        }

        if (!_inRange)
        {
            CheckInRange();
        }
    }

    private IEnumerator UpdateAttackingState()
    {
        CheckInRange();
        DetectTarget();

        if (!_currentTarget || !_inRange)
        {
            SetState(EnemyState.Chasing);
            yield break;
        }

        if (!_isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;
        GameObject bulletGO = ObjectPoolManager.Instance.GetObjectFromPool(_bulletPrefab);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.Damage = _currentAttack;
        yield return new WaitForSeconds(_attackSpeed);
        _isAttacking = false;
    }

    private void DetectTarget()
    { 
        _currentTarget = null;
    }

    private void CheckInRange()
    {
        if (!_currentTarget)
            return;

        float distanceToTarget = Vector3.Distance(transform.position, _currentTarget.transform.position) - _currentTarget.GetComponentInChildren<CapsuleCollider>().radius;

        if (distanceToTarget <= _attackRange)
        {
            _inRange = true;
            SetState(EnemyState.Attacking);
        }
        else
        {
            _inRange = false;
        }
    }

    protected override void Died()
    {
        OnDied?.Invoke(gameObject);
        gameObject.SetActive(false);
    }

}
