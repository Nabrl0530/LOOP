// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33578,y:32819,varname:node_4013,prsc:2|emission-4024-OUT,alpha-5131-OUT;n:type:ShaderForge.SFN_Tex2d,id:478,x:32911,y:32811,ptovrint:False,ptlb:node_478,ptin:_node_478,varname:node_478,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ee3c17fb367aaec499f148773e073c81,ntxv:0,isnm:False|UVIN-1505-UVOUT;n:type:ShaderForge.SFN_Panner,id:1505,x:32742,y:32848,varname:node_1505,prsc:2,spu:0.1,spv:0|UVIN-3637-OUT;n:type:ShaderForge.SFN_TexCoord,id:5763,x:32354,y:32868,varname:node_5763,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:7792,x:32354,y:32737,varname:node_7792,prsc:2,v1:3,v2:1;n:type:ShaderForge.SFN_Multiply,id:3637,x:32531,y:32759,varname:node_3637,prsc:2|A-7792-OUT,B-5763-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:4653,x:32911,y:33052,ptovrint:False,ptlb:node_4653,ptin:_node_4653,varname:node_4653,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ee3c17fb367aaec499f148773e073c81,ntxv:0,isnm:False|UVIN-9872-UVOUT;n:type:ShaderForge.SFN_Panner,id:9872,x:32673,y:33098,varname:node_9872,prsc:2,spu:0.25,spv:0|UVIN-3637-OUT;n:type:ShaderForge.SFN_Multiply,id:4024,x:33136,y:32928,varname:node_4024,prsc:2|A-478-RGB,B-4653-RGB,C-6506-RGB,D-2449-OUT;n:type:ShaderForge.SFN_Color,id:6506,x:32866,y:33238,ptovrint:False,ptlb:node_6506,ptin:_node_6506,varname:node_6506,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3610548,c2:1,c3:0.3382353,c4:1;n:type:ShaderForge.SFN_Vector1,id:2449,x:32918,y:33387,varname:node_2449,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:7888,x:33218,y:33251,ptovrint:False,ptlb:node_7888,ptin:_node_7888,varname:node_7888,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:facbe10c4f45cf044aee8ceafbad2367,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:5619,x:33208,y:33128,varname:node_5619,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:5131,x:33423,y:33189,varname:node_5131,prsc:2|A-5619-OUT,B-7888-R;proporder:478-4653-6506-7888;pass:END;sub:END;*/

Shader "Shader Forge/6-3guangci" {
    Properties {
        _node_478 ("node_478", 2D) = "white" {}
        _node_4653 ("node_4653", 2D) = "white" {}
        _node_6506 ("node_6506", Color) = (0.3610548,1,0.3382353,1)
        _node_7888 ("node_7888", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _node_478; uniform float4 _node_478_ST;
            uniform sampler2D _node_4653; uniform float4 _node_4653_ST;
            uniform float4 _node_6506;
            uniform sampler2D _node_7888; uniform float4 _node_7888_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_730 = _Time + _TimeEditor;
                float2 node_3637 = (float2(3,1)*i.uv0);
                float2 node_1505 = (node_3637+node_730.g*float2(0.1,0));
                float4 _node_478_var = tex2D(_node_478,TRANSFORM_TEX(node_1505, _node_478));
                float2 node_9872 = (node_3637+node_730.g*float2(0.25,0));
                float4 _node_4653_var = tex2D(_node_4653,TRANSFORM_TEX(node_9872, _node_4653));
                float3 emissive = (_node_478_var.rgb*_node_4653_var.rgb*_node_6506.rgb*1.0);
                float3 finalColor = emissive;
                float4 _node_7888_var = tex2D(_node_7888,TRANSFORM_TEX(i.uv0, _node_7888));
                fixed4 finalRGBA = fixed4(finalColor,(1.5*_node_7888_var.r));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
