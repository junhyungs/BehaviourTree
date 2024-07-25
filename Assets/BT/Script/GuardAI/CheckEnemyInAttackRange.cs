using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayer = LayerMask.GetMask("Enemy");

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object targetObject = GetData("target");

        if(targetObject == null)
        {
            _state = NodeState.Fail;
            return _state;
        }

        Transform target = (Transform)targetObject;

        if (Vector3.Distance(_transform.position, target.position) <= GuardBehaviourTree._attackRange)
        {
            _animator.SetBool("Attack", true);
            _animator.SetBool("Move", false);

            _state = NodeState.Success;
            return _state;
        }

        _state = NodeState.Fail;
        return _state;
    }
}
