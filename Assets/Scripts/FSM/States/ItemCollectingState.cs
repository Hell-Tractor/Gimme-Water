using UnityEngine;

namespace AI.FSM {

    public class ItemCollectingState : FSMState {
        protected override void init() {
            this.StateID = FSMStateID.ItemCollecting;
        }

        public override void OnStateEnter(FSMBase fsm) {
            base.OnStateEnter(fsm);

            CharacterStatus _characterStatus = fsm.GetComponent<CharacterStatus>();
            if (_characterStatus != null) {
                _characterStatus.ItemCanCollect.collectByCharacter(_characterStatus);
                _characterStatus.ItemCanCollect = null;
            }
        }
    }

}