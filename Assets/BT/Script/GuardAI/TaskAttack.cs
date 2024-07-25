using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskAttack : Node
{
    public TaskAttack(Transform transform)
    {

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        _state = NodeState.Running;
        return _state;
    }
}
