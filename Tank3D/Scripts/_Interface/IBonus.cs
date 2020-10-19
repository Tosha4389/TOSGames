using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusTypes
{
    hp,
    shield,
    money,
    damage
}

public interface IBonus
{
    //BonusTypes currentBonusType { get; }
    //int increaseValue { get; }
    //float speed { get; }
    //float speedLights { get; }

    void BonusEffect();
}
