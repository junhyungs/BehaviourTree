using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Node
{
    INode _root;

    public _Node(INode root)
    {
        _root = root;
    }   

    public void Execute()
    {
        _root.Evaluate();
    }
}
