using UnityEngine;
using RangerV;

[Component("GameGeneral/RigidbodyCmp")]
public class RigidbodyCmp : ComponentBase, ICustomAwake
{
    public Rigidbody rigidbody;

    public void OnAwake()
    {
        /*rigidbody = gameObject.GetComponent<Rigidbody>();

        if (rigidbody == null)
            Debug.LogError("на сущности " + gameObject.name + " нет компонента rigidbody");*/
    }
}
