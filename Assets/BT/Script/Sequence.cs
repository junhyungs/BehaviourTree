using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node//모두 성공해야 성공을 반환(성공해야 다음 하위 노드로 이동). 하나라도 실패 시 실패를 반환.
    {
        public Sequence() : base() { }  
        public Sequence(List<Node> children) : base(children) { }



        public override NodeState Evaluate() //Evaluate 재정의
        {
            bool anyChildIsRunning = false; //현재 실행중이냐?

            foreach(Node node in _children) //List에 담겨있는 노드를 가져옴.
            {
                switch (node.Evaluate()) //Evaluate를 통해 각 노드의 State를 가져온다.
                {
                    case NodeState.Fail:
                        _state = NodeState.Fail;
                        return _state;
                    case NodeState.Success: //성공하면 다음노드로
                        continue;
                    case NodeState.Running: //실행중이면 다음 노드로
                        anyChildIsRunning = true; //대신 이 bool 값을 true로 만들어 뭔가 foreach가 종료되었을 때 다르게 처리할 수 있도록 함.
                        continue;
                    default:
                        _state = NodeState.Success; //기본적으로 성공을 반환
                        return _state;
                }
            }

            _state = anyChildIsRunning ? NodeState.Running : NodeState.Success; //Running에 대한 처리.

            return _state;
        }
    }
}

