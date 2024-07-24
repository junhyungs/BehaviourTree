using BehaviourTree;

public class GuardBehaviourTree : Tree
{
    public UnityEngine.Transform[] _wayPoints;
    public static float _speed = 2f;
    public float Speed { get { return _speed; } }

    protected override Node SetUpTree()
    {
        Node root = new TaskPatrol(transform, _wayPoints, this); //업캐스팅

        return root;
    }
}
