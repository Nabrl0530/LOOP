using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 変更点メモ : MonoBehaviour
{
    // 変更点

    ////// アニメーションを綺麗に合わせる
    // PlayerのScaleを0.7に変更
    // blockのScaleを0.8に変更
    // checkのオフセット.yを3に変更
    // CenterPlayerのInspectorのGo_Length_After_Climingを0.85に変更

    ////// 挙動ズレ
    // Yojinoboriアニメーションのループを外してAply(済)
    // CenterPlayerのInspectorのSecondClimbの時間を3.43に伸ばす
    // AnimationのClimb→Waitの矢印内の遷移を0%に変更,つまり補間を無くす
    // 状態をそのフレームでもどす。Player.cs393行目辺りのよじ登りワープ後 sc_state.Set_AnimationState(Player_State.e_PlayerAnimationState.WAITING);

    ////// クリアカメラ追加
    // miya_camera_move.csに// クリアカメラのコメントと共に追加
    // miya_secene内のClearCameraプレハブをヒエラルキーへ配置し、inspectorの「Follow」と「LookAt」にCenterPlayerをアタッチ
    // 該当箇所でmiya_camera_move.csのSet_ClearCamera()orSet_DefaultCamera()を呼び出す。
}
