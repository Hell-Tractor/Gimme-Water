using UnityEngine;

namespace AI.FSM {

    public class DizzyState : FSMState {
        private float _timer;
        private int _lastPressedButton;
        private CharacterStatus _characterStatus;
        
        protected override void init() {
            this.StateID = FSMStateID.Dizzy;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);

            fsm.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            _characterStatus = fsm.GetComponent<CharacterStatus>();
            _characterStatus.PlayOnHitSFX();
            _timer = _characterStatus?.DizzyTime ?? 3;
            _lastPressedButton = 1;
        }

        public override void OnStateStay(FSMBase fsm) {
            base.OnStateStay(fsm);

            _timer -= Time.deltaTime;

            if (Input.GetButtonDown("DizzySpeedUp" + (_lastPressedButton ^ 1))) {
                _timer -= _characterStatus?.DizzyTimeReduceAmount ?? 0.2f;
                _lastPressedButton ^= 1;
            }

            if (_timer <= 0) {
                fsm.SetTrigger(FSMTriggerID.DizzyEnd);
            }
        }
    }

}