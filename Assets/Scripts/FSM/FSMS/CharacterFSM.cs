using UnityEngine;

namespace AI.FSM {
    public class CharacterFSM : FSMBase {
        protected override void setUpFSM() {
            base.setUpFSM();

            if(isServer) {
                IdleState idleState = new IdleState();
                idleState.AddMap(FSMTriggerID.MoveStart, FSMStateID.Moving);
                idleState.AddMap(FSMTriggerID.ItemFound, FSMStateID.ItemCollecting);
                idleState.AddMap(FSMTriggerID.AttackStart, FSMStateID.Attacking);
                idleState.AddMap(FSMTriggerID.UnderAttack, FSMStateID.Dizzy);
                _states.Add(idleState);

                MovingState movingState = new MovingState();
                movingState.AddMap(FSMTriggerID.MoveEnd, FSMStateID.Idle);
                movingState.AddMap(FSMTriggerID.ItemFound, FSMStateID.ItemCollecting);
                movingState.AddMap(FSMTriggerID.AttackStart, FSMStateID.Attacking);
                movingState.AddMap(FSMTriggerID.UnderAttack, FSMStateID.Dizzy);
                _states.Add(movingState);

                ItemCollectingState itemCollectingState = new ItemCollectingState();
                itemCollectingState.AddMap(FSMTriggerID.ItemCollected, FSMStateID.Default);
                itemCollectingState.AddMap(FSMTriggerID.UnderAttack, FSMStateID.Dizzy);
                _states.Add(itemCollectingState);

                AttackingState attackingState = new AttackingState();
                attackingState.AddMap(FSMTriggerID.AttackEnd, FSMStateID.Idle);
                attackingState.AddMap(FSMTriggerID.UnderAttack, FSMStateID.Dizzy);
                _states.Add(attackingState);

                DizzyState dizzyState = new DizzyState();
                dizzyState.AddMap(FSMTriggerID.DizzyEnd, FSMStateID.Idle);
                _states.Add(dizzyState);
            } else {
                IdleState idleState = new IdleState();
                _states.Add(idleState);
            }
        }
    }
}