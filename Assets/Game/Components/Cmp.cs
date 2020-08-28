using RangerV;
using UnityEngine;

public class Cmp : ComponentBase, ICustomAwake
{
    //public Image image;
    public Animator animator;

    public void OnAwake()
    {
        //image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }
}
