using UnityEngine;

namespace AI.FSM {

    public class MoveEndTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.MoveEnd;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return input.magnitude < 0.01f;
        }
    }

}