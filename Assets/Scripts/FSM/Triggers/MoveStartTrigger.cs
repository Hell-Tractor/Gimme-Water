using UnityEngine;

namespace AI.FSM {

    public class MoveStartTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveStart;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            Vector2 direction = fsm.GetComponent<CharacterStatus>().Direction;
            return direction.magnitude > 0.01f;
        }
    }

}