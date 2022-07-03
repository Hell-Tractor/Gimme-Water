using UnityEngine;

namespace AI.FSM {
    public class CharacterFSM : FSMBase {
        protected override void setUpFSM() {
            base.setUpFSM();

            IdleState idleState = new IdleState();
            idleState.AddMap(FSMTriggerID.MoveStart, FSMStateID.Moving);
            _states.Add(idleState);

            MovingState movingState = new MovingState();
            movingState.AddMap(FSMTriggerID.MoveEnd, FSMStateID.Idle);
            _states.Add(movingState);
        }
    }
}