using UnityEngine;

namespace AI.FSM {

    public class AttackStartTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.AttackStart;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            var shooter = fsm.GetComponent<Shooter>();
            return shooter != null && shooter.shooting;
        }
    }

}