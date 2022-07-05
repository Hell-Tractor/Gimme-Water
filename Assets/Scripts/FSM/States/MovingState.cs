using UnityEngine;

namespace AI.FSM {

    public class MovingState : FSMState {
        private CharacterStatus _characterStatus = null;
        
        protected override void init() {
            this.StateID = FSMStateID.Moving;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);
            _characterStatus = fsm.GetComponent<CharacterStatus>();
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

        }

        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateFixedStay(fsm);

            Rigidbody2D rb = fsm.GetComponent<Rigidbody2D>();
            if (rb != null && _characterStatus != null) {
                rb.velocity = _characterStatus.Direction * _characterStatus.Speed;
            }
        }

        public override void OnStateExit(FSMBase fsm) {
            base.OnStateExit(fsm);
            _characterStatus = null;
        }
    }

}
