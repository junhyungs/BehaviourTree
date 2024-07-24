using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : Node//��� �����ؾ� ������ ��ȯ(�����ؾ� ���� ���� ���� �̵�). �ϳ��� ���� �� ���и� ��ȯ.
    {
        public Sequence() : base() { }  
        public Sequence(List<Node> children) : base(children) { }



        public override NodeState Evaluate() //Evaluate ������
        {
            bool anyChildIsRunning = false; //���� �������̳�?

            foreach(Node node in _children) //List�� ����ִ� ��带 ������.
            {
                switch (node.Evaluate()) //Evaluate�� ���� �� ����� State�� �����´�.
                {
                    case NodeState.Fail:
                        _state = NodeState.Fail;
                        return _state;
                    case NodeState.Success: //�����ϸ� ��������
                        continue;
                    case NodeState.Running: //�������̸� ���� ����
                        anyChildIsRunning = true; //��� �� bool ���� true�� ����� ���� foreach�� ����Ǿ��� �� �ٸ��� ó���� �� �ֵ��� ��.
                        continue;
                    default:
                        _state = NodeState.Success; //�⺻������ ������ ��ȯ
                        return _state;
                }
            }

            _state = anyChildIsRunning ? NodeState.Running : NodeState.Success; //Running�� ���� ó��.

            return _state;
        }
    }
}

