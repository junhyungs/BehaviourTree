using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : Node //���� ��尡 �ϳ��� �����ϸ� ������ ��ȯ. ���� �� ���� ���� ���� ����.
    {
        public Selector() : base() { }  
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {

            foreach (Node node in _children) 
            {
                switch (node.Evaluate()) 
                {
                    case NodeState.Fail:
                        continue;
                    case NodeState.Success: 
                        _state = NodeState.Success;
                        return _state;
                    case NodeState.Running: 
                        _state = NodeState.Running;
                        return _state;
                    default:
                        continue;
                }
            }

            _state = NodeState.Fail;

            return _state;
        }
    }
}

