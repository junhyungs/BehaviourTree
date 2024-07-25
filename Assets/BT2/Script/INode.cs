using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INode
{
    public enum BTNodeState
    {
        Running,
        Success,
        Fail
    }

    public BTNodeState Evaluate();
}
