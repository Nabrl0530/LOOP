using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 変更点メモ : MonoBehaviour
{
    // 変更点

    // アニメーションを合わせる
    // PlayerのScaleを0.8に変更

    // 挙動ズレ
    // Yojinoboriアニメーションのループを外してAply(済)
    // InspectorのSecondClimbの時間を伸ばす
    // AnimationのClimb→Waitの矢印内の遷移を0%に変更,つまり補間を無くす
    // 状態をそのフレームでもどす。sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.WAITING);
}
