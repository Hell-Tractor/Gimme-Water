using UnityEngine;

namespace AI.FSM {

    public class ItemFoundTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.ItemFound;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return false;
        }
    }

}