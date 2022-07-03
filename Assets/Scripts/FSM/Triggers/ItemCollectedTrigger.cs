using UnityEngine;

namespace AI.FSM {

    public class ItemCollectedTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.ItemCollected;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            return fsm.GetComponent<CharacterStatus>()?.ItemCanCollect == null;
        }
    }

}