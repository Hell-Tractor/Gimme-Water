using UnityEngine;

namespace AI.FSM {

    public class AttackEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.AttackEnd;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return !Input.GetButton("Fire");
        }
    }

}