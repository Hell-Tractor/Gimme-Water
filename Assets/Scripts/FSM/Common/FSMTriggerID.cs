namespace AI.FSM {

    /// <summary>
    /// 有限状态机条件枚举
    /// </summary>
    public enum FSMTriggerID {
        MoveEnd,
        MoveStart,
        ItemFound,
        ItemCollected,
        DizzyEnd,
        UnderAttack,
        AttackStart,
        AttackEnd,
    }

}