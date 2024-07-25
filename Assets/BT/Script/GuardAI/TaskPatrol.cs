using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskPatrol : Node
{
    //wayPoints
    private Transform _transform;
    private Transform[] _wayPoints;

    private Animator _animator;


    //Patrol
    private int _currentWayPointIndex = 0;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = true;

    public TaskPatrol(Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _wayPoints = wayPoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;

            if(_waitCounter >= _waitTime)
            {
                _animator.SetBool("Move", true);
                _waiting = false;
            }
        }
        else
        {
            Transform wayPoint = _wayPoints[_currentWayPointIndex];

            if (Vector3.Distance(_transform.position, wayPoint.position) < 0.01f)
            {
                _transform.position = wayPoint.position;
                _waitCounter = 0f;
                _waiting = true;
                _animator.SetBool("Move", false);

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length; //���� ����. �迭�� ũ�⸦ �ʰ����� �ʵ��� ��ȯ�ϱ� ���� ���� ������ ����.
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wayPoint.position, GuardBehaviourTree._speed * Time.deltaTime);
                Rotation(wayPoint);
            }
        }

        _state = NodeState.Running;
        return _state;
    }

    private void Rotation(Transform wayPoint)
    {
        Vector3 moveDirection = (wayPoint.position - _transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(moveDirection);

        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, 10f * Time.deltaTime);
    }
}
