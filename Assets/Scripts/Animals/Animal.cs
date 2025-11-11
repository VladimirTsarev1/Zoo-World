using Pool;

namespace Animals
{
    public abstract class Animal : BasePooledObject
    {
        // protected Rigidbody Rigidbody;
        // protected IMoveBehavior MoveBehavior;

        // public virtual void Initialize(IMoveBehavior moveBehavior)
        // {
        //     Rigidbody = GetComponent<Rigidbody>();
        //     MoveBehavior = moveBehavior;
        // }
        //
        // private void FixedUpdate()
        // {
        //     MoveBehavior?.Move(Rigidbody);
        // }
    }
}