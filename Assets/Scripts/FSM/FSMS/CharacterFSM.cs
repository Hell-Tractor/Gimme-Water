using UnityEngine;

namespace AI.FSM {
    public class CharacterFSM : FSMBase {
        protected override void setUpFSM() {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.MoveStart, FSMStateID.Moving);
            idleState.AddMap(FSMTriggerID.ItemCollected, FSMStateID.ItemCollecting);
            _states.Add(idleState);

            MovingState movingState = new MovingState();
            movingState.AddMap(FSMTriggerID.MoveEnd, FSMStateID.Idle);
            movingState.AddMap(FSMTriggerID.ItemCollected, FSMStateID.ItemCollecting);
            _states.Add(movingState);

            ItemCollectingState itemCollectingState = new ItemCollectingState();
            itemCollectingState.AddMap(FSMTriggerID.ItemCollected, FSMStateID.Default);
            _states.Add(itemCollectingState);
        }
    }
}