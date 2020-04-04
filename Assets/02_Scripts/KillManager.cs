using UnityEngine;

public class KillManager : MonoBehaviour
{
    [SerializeField] Transform headPos;
    [SerializeField] Transform feetPos;
    float headCheckRad = .01f;
    float feetCheckRad = 0.5f;
    [SerializeField] LayerMask steppableObj;
    private bool isStomped;
    private bool isStepping;

    public bool DeathDetected(GameObject target)
    {
        bool isStomped = Physics2D.OverlapCircle(
            target.transform.GetChild(0).position,
            headCheckRad,
            steppableObj
            );
        isStepping = Physics2D.OverlapCircle(
            target.transform.GetChild(1).position,
            feetCheckRad,
            steppableObj
            );
        return isStomped && isStepping;
    }
}
