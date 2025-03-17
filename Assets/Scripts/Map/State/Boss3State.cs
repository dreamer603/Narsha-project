using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;

public class Boss3State : StructureState
{
    public override void OnSpawn()
    {
        StartCoroutine(MoveStructure(2, _duration, 1f));
        MapManager.Instance.poolManager.SetActive(false);
    }

    public override void OnDie()
    {
        
    }
}
