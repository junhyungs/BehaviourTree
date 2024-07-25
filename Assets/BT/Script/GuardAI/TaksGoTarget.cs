using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaksGoTarget : Node
{
    private Transform _transform;

    public TaksGoTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if(Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardBehaviourTree._speed * Time.deltaTime);
            Rotation(target);
        }

        _state = NodeState.Running;
        return _state;
    }

    private void Rotation(Transform target)
    {
        Vector3 moveDirection = (target.position - _transform.position).normalized;

        Quaternion rotation = Quaternion.LookRotation(moveDirection);

        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, 10f * Time.deltaTime); 
    }
}
