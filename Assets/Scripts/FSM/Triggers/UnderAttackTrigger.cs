using UnityEngine;

namespace AI.FSM {

    public class UnderAttackTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.UnderAttack;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return false;
        }
    }

}