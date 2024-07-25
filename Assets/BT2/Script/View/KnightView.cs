using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KnightView : MonoBehaviour
{
    //Node
    private _Node _node;

    [SerializeField]
    private Transform[] _wayPoints;
    private Animator _animator;

    //Enemy    
    private GameObject _enemy;
    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    //EnemyLayer
    private int _enemyLayer;

    //Patrol
    private bool isWaiting = true;
    private int _currentWayPointIndex = 0;
    private float _walkSpeed = 2f;
    private float _waitCounter = 0f;
    private float _waitTime = 1f;

    //CheckEnemy
    private float _radius = 6f;

    //AttackRange
    private float _attackRange = 1f;

    //Attack
    private float _attackCounter = 0f;
    private float _attackTime = 1f;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _enemyLayer = LayerMask.GetMask("Enemy");
        _node = new _Node(SetUpNode());
    }


    void Update()
    {
        _node.Execute();
    }

    INode SetUpNode()
    {
        var AttackNodeList = new List<INode>();
        AttackNodeList.Add(new _ActionNode(CheckEnemyAttackRange));
        AttackNodeList.Add(new _ActionNode(Attack));

        var AttackSequenceNode = new _Sequence(AttackNodeList);

        var CheckNodeList = new List<INode>();
        CheckNodeList.Add(new _ActionNode(CheckEnemy));
        CheckNodeList.Add(new _ActionNode(MoveToTarget));

        var CheckSequenceNode = new _Sequence(CheckNodeList);

        List<INode> firstSelectorNode = new List<INode>();
        firstSelectorNode.Add(AttackSequenceNode);
        firstSelectorNode.Add(CheckSequenceNode);
        firstSelectorNode.Add(new _ActionNode(Patrol));

        var firstNode = new _Selector(firstSelectorNode);

        return firstNode;
    }

    private INode.BTNodeState Patrol()
    {
        if(_wayPoints == null || _wayPoints.Length == 0)
        {
            return INode.BTNodeState.Fail;
        }

        if (isWaiting)
        {
            _waitCounter += Time.deltaTime;

            if(_waitCounter >= _waitTime)
            {
                _animator.SetBool("Move", true);
                isWaiting = false;
            }
        }
        else
        {
            Transform wayPoint = _wayPoints[_currentWayPointIndex];

            if(Vector3.Distance(transform.position, wayPoint.position) < 0.01f)
            {
                transform.position = wayPoint.position;
                _waitCounter = 0f;
                isWaiting = true;
                _animator.SetBool("Move", false);

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length;

                return INode.BTNodeState.Success;
            }
            else
            {
                Move(wayPoint);
                Rotation(wayPoint);
            }
        }

        return INode.BTNodeState.Running;
    }

    private INode.BTNodeState CheckEnemy()
    {
        if(_enemy == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _radius, _enemyLayer);

            if(colliders.Length > 0)
            {
                _enemy = colliders[0].gameObject;
                _animator.SetBool("Move", true);
                return INode.BTNodeState.Success;
            }

            return INode.BTNodeState.Fail;
        }

        return INode.BTNodeState.Success;
    }
    private INode.BTNodeState MoveToTarget()
    {
        if(_enemy == null)
        {
            return INode.BTNodeState.Fail;
        }

        Transform target = _enemy.transform;

        if(Vector3.Distance(transform.position,target.position) > 0.01f)
        {
            Move(target);
            Rotation(target);
        }

        return INode.BTNodeState.Running;
    }

    private INode.BTNodeState CheckEnemyAttackRange()
    {
        if(_enemy == null)
        {
            return INode.BTNodeState.Fail;
        }

        Transform target = _enemy.transform;

        if(Vector3.Distance(transform.position, target.position) <= _attackRange)
        {
            _animator.SetBool("Move", false);
            _animator.SetBool("Attack", true);

            return INode.BTNodeState.Success;
        }

        return INode.BTNodeState.Fail;
    }

    private INode.BTNodeState Attack()
    {
        if(_enemy == null)
        {
            return INode.BTNodeState.Fail;
        }

        Transform target = _enemy.transform;

        if(target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;

        if(_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();

            if(enemyIsDead)
            {
                _enemy = null;
                _animator.SetBool("Attack", false);
                _animator.SetBool("Move", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        return INode.BTNodeState.Running;
    }

    
    private void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _walkSpeed * Time.deltaTime);
    }

    private void Rotation(Transform target)
    {
        Vector3 rotationDir = (target.position - transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(rotationDir);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
    }

}
