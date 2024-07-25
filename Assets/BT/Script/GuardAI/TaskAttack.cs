using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using TMPro;

public class TaskAttack : Node
{
    private Transform _lastTarget;
    private EnemyManager _enemyManager;
    private Animator _animator;

    private float _attackTime = 1f;
    private float _attackCouter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }

        _attackCouter += Time.deltaTime;
        if(_attackCouter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();

            if (enemyIsDead)
            {
                ClearData("target");
                _animator.SetBool("Attack", false);
                _animator.SetBool("Move", true);
            }
            else
            {
                _attackCouter = 0f;
            }
        }

        _state = NodeState.Running;
        return _state;
    }
}
