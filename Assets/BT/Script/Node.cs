using System.Collections.Generic;

namespace BehaviourTree
{
    public enum NodeState
    {
        Running,
        Success,
        Fail
    }

    public class Node
    {
        protected NodeState _state;

        public Node _parent;

        protected List<Node> _children = new List<Node>();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            _parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }

        private void Attach(Node node)
        {
            node._parent = this;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.Fail; //fail을 반환하는 간단한 가상함수

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value)) //값이 있으면 값을 리턴
            {
                return value;
            }

            Node node = _parent; //탐색할 최초 노드설정

            while (node != null) //노드가 null이 아닐때 까지 재귀적으로 값을 찾는다.
            {
                value = node.GetData(key);

                if (value != null)
                    return value;

                node = node._parent; //다음 부모 노드 설정.
            }

            return null; //마지막까지 값을 찾지못했다면 null을 리턴.
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                return true;
            }

            Node node = _parent;

            while (node != null)
            {
                bool cleared = node.ClearData(key);

                if (cleared)
                    return true;

                node = node._parent;
            }

            return false;
        }

    }
}


