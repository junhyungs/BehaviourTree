using BehaviourTree;
using System.Collections.Generic;

public class GuardBehaviourTree : Tree
{
    public UnityEngine.Transform[] _wayPoints;

    public static float _speed = 2f;
    public static float _fovRange = 6f;
    public static float _attackRange = 1f;
    protected override Node SetUpTree()
    {
        //Node root = new TaskPatrol(transform, _wayPoints); //업캐스팅

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFovRange(transform),
                new TaksGoTarget(transform),
            }),

            new TaskPatrol(transform, _wayPoints)
        });

        return root;
    }
}
