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

        public virtual NodeState Evaluate() => NodeState.Fail; //fail�� ��ȯ�ϴ� ������ �����Լ�

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;

            if (_dataContext.TryGetValue(key, out value)) //���� ������ ���� ����
            {
                return value;
            }

            Node node = _parent; //Ž���� ���� ��弳��

            while (node != null) //��尡 null�� �ƴҶ� ���� ��������� ���� ã�´�.
            {
                value = node.GetData(key);

                if (value != null)
                    return value;

                node = node._parent; //���� �θ� ��� ����.
            }

            return null; //���������� ���� ã�����ߴٸ� null�� ����.
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


