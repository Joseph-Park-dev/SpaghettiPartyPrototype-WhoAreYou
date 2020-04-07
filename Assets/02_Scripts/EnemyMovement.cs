using UnityEngine;

public class EnemyMovement : PlayerMovement
{
    [SerializeField] private Vector3 targetPos;
    private Vector3 destination;

    private void Update()
    {
        targetPos = GameObject.FindGameObjectWithTag("PLAYER").transform.position;
        destination = targetPos - transform.position;
        Move(FacingDir(destination));
        //AnimateDeath();
    }

    private float FacingDir(Vector3 destination)
    {
        if (destination.x < 0)
            return -1;
        else
            return 1;
    }
}
