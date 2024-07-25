using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Selector : INode
{
    private List<INode> _childNodeList;

    public _Selector(List<INode> childNodeList)
    {
        _childNodeList = childNodeList;
    }

    public INode.BTNodeState Evaluate()
    {
        if(_childNodeList == null)
        {
            return INode.BTNodeState.Fail;
        }

        foreach(var childNode in  _childNodeList)
        {
            switch (childNode.Evaluate())
            {
                case INode.BTNodeState.Running:
                    return INode.BTNodeState.Running;
                case INode.BTNodeState.Success:
                    return INode.BTNodeState.Success;
            }
        }

        return INode.BTNodeState.Fail;
    }
}
