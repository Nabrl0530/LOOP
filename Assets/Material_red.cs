using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Material_red : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //変更したい色
        Color setColor = new Color(0.0f, 0f, 0f);

        //Aをコピー
        //GameObject targetB = Instantiate(targetA);

        //対象のシェーダー情報を取得
        Shader sh = this.GetComponent<MeshRenderer>().material.shader;

        //取得したシェーダーを元に新しいマテリアルを作成
        Material mat = new Material(sh);

        //作成したマテリアルの色を変更
        mat.color = setColor;

        //対象オブジェクトに割り当てる
        this.GetComponent<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
