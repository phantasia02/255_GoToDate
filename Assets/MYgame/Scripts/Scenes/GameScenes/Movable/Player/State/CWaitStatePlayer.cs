using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CWaitStatePlayer : CPlayerStateBase
{
    public override EMovableState StateType() { return EMovableState.eWait; }

    public CWaitStatePlayer(CMovableBase pamMovableBase) : base(pamMovableBase)
    {

    }

    protected override void InState()
    {
        m_MyPlayerMemoryShare.m_MyPlayer.AnimatorStateCtl.SetCurState(CAnimatorStateCtl.EState.eIdle);
        //m_MyPlayerMemoryShare.m_bDown = false;
    }

    protected override void updataState()
    {
        base.updataState();
        UpdateSpeed();
    }

    protected override void OutState()
    {

    }


    public override void MouseDrag()
    {

    }

}
