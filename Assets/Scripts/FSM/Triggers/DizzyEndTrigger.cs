using UnityEngine;

namespace AI.FSM {

    public class DizzyEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.DizzyEnd;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return false;
        }
    }

}