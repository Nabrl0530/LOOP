// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33942,y:33201,varname:node_4013,prsc:2|emission-4325-OUT,alpha-7321-G;n:type:ShaderForge.SFN_Tex2d,id:6709,x:33034,y:33539,ptovrint:False,ptlb:node_6709,ptin:_node_6709,varname:node_6709,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:53957f5ef3f593d4c97bda35b4c1fdd0,ntxv:0,isnm:False|UVIN-9223-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:3139,x:32799,y:32679,ptovrint:False,ptlb:node_3139,ptin:_node_3139,varname:node_3139,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b3682246f3e77b240a4a051c0bdf2bff,ntxv:0,isnm:False|UVIN-2617-UVOUT;n:type:ShaderForge.SFN_Panner,id:2617,x:32516,y:32684,varname:node_2617,prsc:2,spu:-0.2,spv:0.2|UVIN-5977-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:5977,x:32315,y:32642,varname:node_5977,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:7261,x:32711,y:32872,ptovrint:False,ptlb:node_7261,ptin:_node_7261,varname:node_7261,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1845842,c2:0.4117647,c3:0,c4:1;n:type:ShaderForge.SFN_Vector1,id:9374,x:32711,y:33009,varname:node_9374,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:9968,x:33061,y:32919,varname:node_9968,prsc:2|A-3139-RGB,B-7261-RGB,C-9374-OUT;n:type:ShaderForge.SFN_TexCoord,id:330,x:32399,y:33537,varname:node_330,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:9339,x:32399,y:33433,varname:node_9339,prsc:2,v1:1,v2:5;n:type:ShaderForge.SFN_Multiply,id:7273,x:32623,y:33503,varname:node_7273,prsc:2|A-9339-OUT,B-330-UVOUT;n:type:ShaderForge.SFN_Panner,id:9223,x:32822,y:33503,varname:node_9223,prsc:2,spu:-0.1,spv:0.5|UVIN-7273-OUT;n:type:ShaderForge.SFN_Color,id:9088,x:33034,y:33714,ptovrint:False,ptlb:node_9088,ptin:_node_9088,varname:node_9088,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3241379,c2:1,c3:0,c4:1;n:type:ShaderForge.SFN_Vector1,id:5069,x:33034,y:33864,varname:node_5069,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:9375,x:33261,y:33700,varname:node_9375,prsc:2|A-6709-RGB,B-9088-RGB,C-5069-OUT;n:type:ShaderForge.SFN_Tex2d,id:7254,x:33071,y:33110,ptovrint:False,ptlb:node_7254,ptin:_node_7254,varname:node_7254,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:74e48a2b56ff23049b39d1ca94e8c1bd,ntxv:0,isnm:False|UVIN-423-UVOUT;n:type:ShaderForge.SFN_Panner,id:423,x:32842,y:33249,varname:node_423,prsc:2,spu:-0.1,spv:0.5|UVIN-7366-OUT;n:type:ShaderForge.SFN_Multiply,id:7366,x:32643,y:33249,varname:node_7366,prsc:2|A-4633-OUT,B-1609-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1609,x:32419,y:33283,varname:node_1609,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector2,id:4633,x:32419,y:33179,varname:node_4633,prsc:2,v1:1,v2:5;n:type:ShaderForge.SFN_Add,id:4325,x:33716,y:33162,varname:node_4325,prsc:2|A-9968-OUT,B-9375-OUT,C-8496-OUT,D-7095-OUT;n:type:ShaderForge.SFN_Color,id:9192,x:33071,y:33301,ptovrint:False,ptlb:node_9192,ptin:_node_9192,varname:node_9192,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.6095334,c2:1,c3:0.4852941,c4:1;n:type:ShaderForge.SFN_Vector1,id:5169,x:33071,y:33450,varname:node_5169,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:8496,x:33456,y:33369,varname:node_8496,prsc:2|A-9192-RGB,B-6709-R,C-2323-OUT;n:type:ShaderForge.SFN_Power,id:2323,x:33249,y:33189,varname:node_2323,prsc:2|VAL-3139-R,EXP-9374-OUT;n:type:ShaderForge.SFN_Multiply,id:7095,x:33297,y:33066,varname:node_7095,prsc:2|A-7254-RGB,B-9192-RGB;n:type:ShaderForge.SFN_Tex2d,id:7321,x:33657,y:33454,ptovrint:False,ptlb:node_7321,ptin:_node_7321,varname:node_7321,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7c41026b743c70a44b25751cdac7c43b,ntxv:0,isnm:False;proporder:6709-3139-7261-9088-7254-9192-7321;pass:END;sub:END;*/

Shader "Shader Forge/6-3men" {
    Properties {
        _node_6709 ("node_6709", 2D) = "white" {}
        _node_3139 ("node_3139", 2D) = "white" {}
        _node_7261 ("node_7261", Color) = (0.1845842,0.4117647,0,1)
        _node_9088 ("node_9088", Color) = (0.3241379,1,0,1)
        _node_7254 ("node_7254", 2D) = "white" {}
        _node_9192 ("node_9192", Color) = (0.6095334,1,0.4852941,1)
        _node_7321 ("node_7321", 2D) = "white" {}
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
            Blend SrcAlpha OneMinusSrcAlpha
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
            uniform sampler2D _node_6709; uniform float4 _node_6709_ST;
            uniform sampler2D _node_3139; uniform float4 _node_3139_ST;
            uniform float4 _node_7261;
            uniform float4 _node_9088;
            uniform sampler2D _node_7254; uniform float4 _node_7254_ST;
            uniform float4 _node_9192;
            uniform sampler2D _node_7321; uniform float4 _node_7321_ST;
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
                float4 node_9637 = _Time + _TimeEditor;
                float2 node_2617 = (i.uv0+node_9637.g*float2(-0.2,0.2));
                float4 _node_3139_var = tex2D(_node_3139,TRANSFORM_TEX(node_2617, _node_3139));
                float node_9374 = 2.0;
                float2 node_9223 = ((float2(1,5)*i.uv0)+node_9637.g*float2(-0.1,0.5));
                float4 _node_6709_var = tex2D(_node_6709,TRANSFORM_TEX(node_9223, _node_6709));
                float2 node_423 = ((float2(1,5)*i.uv0)+node_9637.g*float2(-0.1,0.5));
                float4 _node_7254_var = tex2D(_node_7254,TRANSFORM_TEX(node_423, _node_7254));
                float3 emissive = ((_node_3139_var.rgb*_node_7261.rgb*node_9374)+(_node_6709_var.rgb*_node_9088.rgb*1.0)+(_node_9192.rgb*_node_6709_var.r*pow(_node_3139_var.r,node_9374))+(_node_7254_var.rgb*_node_9192.rgb));
                float3 finalColor = emissive;
                float4 _node_7321_var = tex2D(_node_7321,TRANSFORM_TEX(i.uv0, _node_7321));
                fixed4 finalRGBA = fixed4(finalColor,_node_7321_var.g);
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
