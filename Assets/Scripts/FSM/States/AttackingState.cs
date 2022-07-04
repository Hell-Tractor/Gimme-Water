using UnityEngine;

namespace AI.FSM {

    public class AttackingState : FSMState {
        private MovingState _movingState;
        private Shooter _shooter;
        protected override void init() {
            this.StateID = FSMStateID.Attacking;
            _movingState = new MovingState();
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);
            _movingState.OnStateEnter(fsm);

            _shooter = fsm.GetComponent<Shooter>();
            _shooter?.startShooting();
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);
            _movingState.OnStateStay(fsm);
        }

        public override void OnStateFixedStay(FSMBase fsm) {
            base.OnStateFixedStay(fsm);
            _movingState.OnStateFixedStay(fsm);
        }

        public override void OnStateExit(FSMBase fsm) {
            base.OnStateExit(fsm);
            _movingState.OnStateExit(fsm);

            _shooter?.stopShooting();
        }
    }

}