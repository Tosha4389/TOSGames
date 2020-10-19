using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void DecreaseHp(int damage);

    void DestroyGO();
}
