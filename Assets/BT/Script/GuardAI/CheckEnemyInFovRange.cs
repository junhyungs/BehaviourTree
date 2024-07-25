using BehaviourTree;
using UnityEngine;

public class CheckEnemyInFovRange : Node
{
    private Transform _transform;
    private Animator _animator;
    private static int _enemyLayer = LayerMask.GetMask("Enemy");

    public CheckEnemyInFovRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        object target = GetData("target");

        if(target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_transform.position,GuardBehaviourTree._fovRange, _enemyLayer);

            if(colliders.Length > 0)
            {
                _parent._parent.SetData("target", colliders[0].transform);
                _animator.SetBool("Move", true);
                _state = NodeState.Success;
                return _state;
            }

            _state = NodeState.Fail;
            return _state;
        }

        _state = NodeState.Success;
        return _state;
    }
}
