using System;
using System.Collections.Generic;
using Mirror;

namespace AI.FSM {

/// <summary>
/// 有限状态机条件类
/// </summary>
public abstract class FSMState {
    public FSMState() {
        init();
        _map = new Dictionary<FSMTriggerID, FSMStateID>();
        _triggers = new List<FSMTrigger>();
    }

    /// <summary>
    /// 子类必须初始化StateID
    /// </summary>
    protected abstract void init();
    
    /// <summary>
    /// 为当前状态添加映射，由状态机调用
    /// </summary>
    /// <param name="triggerID">条件ID</param>
    /// <param name="stateID">满足条件后转移的新状态ID</param>
    public void AddMap(FSMTriggerID triggerID, FSMStateID stateID) {
        _map.Add(triggerID, stateID);
        Type triggerType = Type.GetType("AI.FSM." + triggerID + "Trigger");
        FSMTrigger trigger = Activator.CreateInstance(triggerType) as FSMTrigger;
        _triggers.Add(trigger);
    }
    /// <summary>
    /// 判断是否满足条件并切换状态
    /// </summary>
    /// <param name="fsm">所要切换的状态机</param>
    public void Reason(FSMBase fsm) {
        foreach (FSMTrigger i in _triggers) {
            if (fsm.GetTrigger(i.TriggerID) || i.HandleTrigger(fsm)) {
                fsm.changeActiveState(_map[i.TriggerID]);
                return;
            }
        }
    }
    
    public virtual void OnStateEnter(FSMBase fsm) {
        // if (fsm.isLocalPlayer)
            // fsm.SetAnimatorTrigger(StateID.ToString() + "Trigger");
        if (fsm.animator != null) {
            fsm.GetComponent<NetworkAnimator>().SetTrigger(StateID.ToString() + "Trigger");
            // fsm.animator.SetTrigger(StateID.ToString() + "Trigger");
        }
    }
    public virtual void OnStateStay(FSMBase fsm) {}
    public virtual void OnStateFixedStay(FSMBase fsm) {}
    public virtual void OnStateExit(FSMBase fsm) {
        // if (fsm.isLocalPlayer)
            // fsm.ResetAnimatorTrigger(StateID.ToString() + "Trigger");
        if (fsm.animator != null) {
            fsm.GetComponent<NetworkAnimator>().ResetTrigger(StateID.ToString() + "Trigger");
            // fsm.animator.ResetTrigger(StateID.ToString() + "Trigger");
        }
    }
    
    public FSMStateID StateID { get; set; }
    private List<FSMTrigger> _triggers;
    private Dictionary<FSMTriggerID, FSMStateID> _map;
}

}