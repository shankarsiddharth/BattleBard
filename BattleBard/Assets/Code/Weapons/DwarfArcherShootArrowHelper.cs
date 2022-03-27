using UnityEngine;

public class DwarfArcherShootArrowHelper : MonoBehaviour
{
    DwarfArcher parent;

    private void Start()
    {
        parent = GetComponentInParent<DwarfArcher>();
    }

    public void ShootArrow()
    {
        parent.CreateArrow();
    }
}
