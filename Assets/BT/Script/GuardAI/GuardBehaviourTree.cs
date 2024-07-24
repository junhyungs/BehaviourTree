using BehaviourTree;

public class GuardBehaviourTree : Tree
{
    public UnityEngine.Transform[] _wayPoints;

    public static float _speed = 2f;
    protected override Node SetUpTree()
    {
        Node root = new TaskPatrol(transform, _wayPoints);

        return root;
    }
}
