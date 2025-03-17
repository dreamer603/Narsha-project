using System;
using System.Collections;
using UnityEngine;

namespace Map
{
    public class StructureController : MonoBehaviour
    {
        // 보스 구조물 오브젝트 ([0]: fence / [1]: castle / [2]: portal)
        public GameObject[] bossObj; 

        public StructureState currentState;
        private StructureState _balltanState;
        private StructureState _boss2State;
        private StructureState _boss3State;
        
        public void Awake()
        {
            _balltanState = gameObject.AddComponent<BalltanState>();
            _boss2State = gameObject.AddComponent<Boss2State>();
            _boss3State = gameObject.AddComponent<Boss3State>();
        }

        public void UpdateState()
        {
        
             if (GameDataManager.Instance.KillScore >= 300 || Input.GetKeyDown(KeyCode.G))
             {
                 currentState = _balltanState;
                 currentState.OnSpawn();
                 _boss3State.OnSpawn();
             }

             /*if ()
             {
                 currentState = _boss2State;
                 currentState.OnSpawn();
             }

             if ()
             {
                 currentState = _boss3State;
                 _boss3State.OnSpawn();
             }*/
             
        }
        
    }
}
