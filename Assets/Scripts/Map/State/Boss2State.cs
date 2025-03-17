using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;

public class Boss2State : StructureState
{
    public override void OnSpawn()
    {
        StartCoroutine(MoveStructure(1, _duration, 1f));
        IsStructure = true;
        MapManager.Instance.poolManager.SetActive(false);
    }

    public override void OnDie()
    {
        StartCoroutine(MoveStructure(1, _reDuration, -100f));
        IsStructure = false;
        MapManager.Instance.poolManager.SetActive(true);
    }
}
