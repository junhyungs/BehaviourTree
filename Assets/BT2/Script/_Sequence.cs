using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Sequence : INode
{
    private List<INode> _childNodeList;
    int _currentChild;

    public _Sequence(List<INode> childNodeList)
    {
        _childNodeList = childNodeList;
        _currentChild = 0;
    }

    public INode.BTNodeState Evaluate()
    {
        if (_childNodeList == null || _childNodeList.Count == 0)
            return INode.BTNodeState.Fail;

        foreach(var childNode in _childNodeList)
        {
            switch (childNode.Evaluate())
            {
                case INode.BTNodeState.Running:
                    return INode.BTNodeState.Running;
                case INode.BTNodeState.Success:
                    continue;
                case INode.BTNodeState.Fail:
                    return INode.BTNodeState.Fail;
            }
        }

        return INode.BTNodeState.Success;
    }
}
