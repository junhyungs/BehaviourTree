using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _ActionNode : INode
{
    Func<INode.BTNodeState> _onUpdate;

    public _ActionNode(Func<INode.BTNodeState> onUpdate)
    {
        _onUpdate = onUpdate;
    }

    public INode.BTNodeState Evaluate()
    {
        if(_onUpdate == null)
        {
            return INode.BTNodeState.Fail;
        }
        else
        {
            return _onUpdate.Invoke();
        }

    }
}
