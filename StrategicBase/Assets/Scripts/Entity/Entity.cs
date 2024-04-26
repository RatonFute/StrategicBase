using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected EntitySO _stats;

    protected Rigidbody2D _rb;

    protected float _currentHealthPoint;
    protected float _currentAttack;
    protected float _currentSpeed;
    protected float _currentAttackRange;

    [HideInInspector] public bool IsDead;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        InitStats();
        IsDead = false;
    }

    private void InitStats()
    {
        _currentHealthPoint = _stats.HealthPoint; 
        _currentAttack = _stats.Attack;
        _currentSpeed = _stats.Speed;
        _currentAttackRange = _stats.AttackRange;
    }

    private void Move(Vector2 dir)
    {
        //play animation
        _rb.velocity = dir * _currentSpeed * Time.deltaTime;
    }

    private void MoveForward()
    {
        //play animation
        _rb.velocity = transform.forward * _currentSpeed * Time.deltaTime;
    }

    protected void TakeDamage(float damage)
    {
        if(IsDead) return;
        _currentHealthPoint -= damage;
        if(_currentHealthPoint <= 0)
        {
            //play animation
            IsDead = true;
        }
    }
}
