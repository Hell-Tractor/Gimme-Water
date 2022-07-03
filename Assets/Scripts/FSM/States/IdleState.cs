using UnityEngine;

namespace AI.FSM {

    public class IdleState : FSMState {
        protected override void init() {
            this.StateID = FSMStateID.Idle;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);

            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = Vector2.zero;
            }
        }
    }

}
