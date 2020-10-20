using UnityEngine;

public class TurretRotatingTank : MonoBehaviour, ITurretRotatingEnemy
{
    Transform turret;

    public virtual void Awake()
    {
        turret = gameObject.transform.GetChild(0);
    }

    public void TurretRotate(Vector3 target)
    {
        Vector3 targetNew = new Vector3(target.x, turret.position.y, target.z);
        Vector3 direction = targetNew - turret.position;
        turret.transform.forward = direction;
    }

}
