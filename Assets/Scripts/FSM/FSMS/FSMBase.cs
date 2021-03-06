using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace AI.FSM {

/// <summary>
/// <para>有限状态机</para>
/// <para>子类需要调用父类Start, Update方法，调用并重写setUpFSM方法</para>
/// </summary>
public class FSMBase : NetworkBehaviour {
    /// <summary>
    /// 初始化状态机，子类需要调用base.setUpFSM()
    /// </summary>
    protected virtual void setUpFSM() {
        _states = new List<FSMState>();
        _enabledTriggers = new HashSet<FSMTriggerID>();
    }
    /// <summary>
    /// 初始化其他组件
    /// </summary>
    protected virtual void init() {
    }
    protected void loadDefaultState() {
        // 加载默认状态
        _currentState = _defaultState = _states.Find(s => s.StateID == defaultStateID);
        _currentState.OnStateEnter(this);
        currentStateID = defaultStateID.ToString();
    }
    public void Start() {
        init();
        setUpFSM();
        loadDefaultState();
    }
    public void Update() {
        // 更新状态
        _currentState.Reason(this);
        _currentState.OnStateStay(this);
    }
    public void FixedUpdate() {
        _currentState.OnStateFixedStay(this);
    }
    /// <summary>
    /// 切换当前状态至目标状态
    /// </summary>
    /// <param name="targetStateID">目标状态ID</param>
    public void changeActiveState(FSMStateID targetStateID) {
        _currentState.OnStateExit(this);
        _currentState = targetStateID == FSMStateID.Default ? _defaultState : _states.Find(s => s.StateID == targetStateID);
        currentStateID = _currentState.StateID.ToString();
        _currentState.OnStateEnter(this);
    }

    public void SetTrigger(FSMTriggerID triggerID) {
        _enabledTriggers.Add(triggerID);
    }

    public bool GetTrigger(FSMTriggerID triggerID) {
        if (_enabledTriggers.Contains(triggerID)) {
            _enabledTriggers.Remove(triggerID);
            return true;
        }
        return false;
    }

    // [Command]
    // public void SetAnimatorTrigger(string trigger) {
    //     this.animator?.SetTrigger(trigger);
    // }
    // [Command]
    // public void ResetAnimatorTrigger(string trigger) {
    //     this.animator?.ResetTrigger(trigger);
    // }

    [Tooltip("默认状态"), Header("状态设置")]
    public FSMStateID defaultStateID;
    protected FSMState _defaultState;
    protected List<FSMState> _states;
    protected FSMState _currentState;
    protected HashSet<FSMTriggerID> _enabledTriggers;
    public string currentStateID;
    public Animator animator;
}

}