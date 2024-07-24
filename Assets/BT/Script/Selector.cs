using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Selector : Node //하위 노드가 하나라도 성공하면 성공을 반환. 실패 시 다음 하위 노드로 진행.
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

