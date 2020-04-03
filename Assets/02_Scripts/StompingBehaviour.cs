using UnityEngine;

public class StompingBehaviour : MonoBehaviour
{
    [SerializeField] Transform headPos;
    [SerializeField] Transform feetPos;
    float headCheckRad = .01f;
    float feetCheckRad = 0.5f;
    [SerializeField] LayerMask steppableObj;
    private bool isStomped;
    private bool isStepping;

    private void Update()
    {
        DestroyPlayer();
    }

    public void DestroyPlayer()
    {
        if (DeathDetected())
        {
            Destroy(this.gameObject);
        }
    }

    private bool DeathDetected()
    {
        isStomped = Physics2D.OverlapCircle(
            headPos.position,
            headCheckRad,
            steppableObj
            );
        isStepping = Physics2D.OverlapCircle(
            feetPos.position,
            feetCheckRad,
            steppableObj
            );
        return isStomped && isStepping;
    }


}
