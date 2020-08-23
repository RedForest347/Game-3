using RangerV;
using System.Numerics;

public class Door : ComponentBase, ICustomAwake
{
    public float final_pos = -1;
    public float step = 0.03f;
    public float start_pos_y;
    public float pos = 0; // позиция (отклонение) относительно начальной
    bool init = false;

    public void OnAwake()
    {
        if (!init)
        {
            start_pos_y = gameObject.transform.position.y;
            init = true;
        }
    }
}
