using UnityEngine;

namespace AI.FSM {

    public class MoveEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveEnd;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            Vector2 direction = fsm.GetComponent<CharacterStatus>().SpeedDirection;
            return direction.magnitude < 0.01f;
        }
    }

}