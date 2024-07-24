using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = SetUpTree(); //최초의 노드를 가져온다.
        }

        protected void Update()
        {
            if(_root != null) //루트가 널이 아니라면 매 프레임마다 행동을 업데이트 한다.
                _root.Evaluate();   
        }

        protected abstract Node SetUpTree();
    }
}

