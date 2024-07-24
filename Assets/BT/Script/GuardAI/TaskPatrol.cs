using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskPatrol : Node
{
    //wayPoints
    private Transform _transform;
    private Transform[] _wayPoints;

    //Patrol
    private int _currentWayPointIndex = 0;
    private float _waitTime = 1f;
    private float _waitCounter = 0f;
    private bool _waiting = false;

    public TaskPatrol(Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _wayPoints = wayPoints;
    }

    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitTime += Time.deltaTime;

            if(_waitCounter >= _waitTime)
            {
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

                _currentWayPointIndex = (_currentWayPointIndex + 1) % _wayPoints.Length; //모듈로 연산. 배열의 크기를 초과하지 않도록 순환하기 위해 모듈로 연산을 적용.
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wayPoint.position, GuardBehaviourTree._speed * Time.deltaTime);
                _transform.LookAt(wayPoint.position);
            }
        }

        _state = NodeState.Running;
        return _state;
    }
}
