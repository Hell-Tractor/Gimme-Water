using UnityEngine;

namespace AI.FSM {

    public class ItemFoundTrigger : FSMTrigger {
        protected override void init() {
            this.TriggerID = FSMTriggerID.ItemFound;
        }

        public override bool HandleTrigger(FSMBase fsm) {
            Collider2D collider = fsm.GetComponent<Collider2D>();
            CharacterStatus characterStatus = fsm.GetComponent<CharacterStatus>();
            if (collider != null && characterStatus != null) {
                Collider2D[] colliders = new Collider2D[1];
                Physics2D.GetContacts(collider, new ContactFilter2D() {
                    layerMask = LayerMask.GetMask("Item")
                }, colliders);
                characterStatus.ItemCanCollect = colliders[0]?.GetComponent<IItem>();
            }
            return characterStatus.ItemCanCollect != null;
        }
    }

}