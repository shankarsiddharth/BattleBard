Shader "Test/Outline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0

        // Outline
        _OutlineThickness ("Outline Thickness", Range(0,1)) = 0.1
        _EdgeColor ("Edge Color", color) = (1,1,1,1)
        _AlhpaThreshold ("Aloha Threshold", Range(0,1)) = 0.5
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"

            "RenderPipeline" = "HighDefinitionRenderingPipeline"
        }

        HLSLINCLUDE
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            
            TEXTURE2D( _MainTex);
            SAMPLER(sampler_MainTex); //
            float4 _Color;
            float4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
            float _UIMaskSoftnessX;
            float _UIMaskSoftnessY;

            CBUFFER_START(UnityPerMaterial)
            // Outline
            float _OutlineThickness;
            float4 _EdgeColor;
            float _AlhpaThreshold;
            CBUFFER_END
        ENDHLSL

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"

            HLSLPROGRAM 
            #pragma vertex vert
            #pragma fragment frag

            struct Attributes
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS   : SV_POSITION;
                float4 color    : COLOR;
                float2 uv[5]  : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                // OUT.vertex = UnityObjectToClipPos(v.vertex);
                OUT.positionCS = TransformObjectToHClip(IN.vertex);

                OUT.color = IN.color * _Color;
                OUT.uv[0] = IN.texcoord;
                OUT.uv[1] = IN.texcoord + float2(0, _OutlineThickness);
                OUT.uv[2] = IN.texcoord + float2(_OutlineThickness, 0);
                OUT.uv[3] = IN.texcoord + float2(0, -_OutlineThickness);
                OUT.uv[4] = IN.texcoord + float2(-_OutlineThickness, 0);
                
                return OUT;
            }

            float4 frag(Varyings IN) : SV_Target
            {
                half4 color = IN.color * (SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv[0]) + _TextureSampleAdd);

                 #ifdef UNITY_UI_CLIP_RECT
                 half2 m = saturate((_ClipRect.zw - _ClipRect.xy - abs(IN.mask.xy)) * IN.mask.zw);
                 color.a *= m.x * m.y;
                 #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif

                float4 original = color; 
                float alpha = original.a;
                float p1 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv[1]).a;  
                float p2 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv[2]).a;  
                float p3 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv[3]).a;  
                float p4 = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv[4]).a;  
                alpha = p1 + p2 + p3 + p4 + alpha;  
                alpha /= 5; 

                if (alpha < _AlhpaThreshold) 
                {
                    original.rgb = _EdgeColor.rgb; 
                }
                else
                {
                    original.a = 0;
                } 
                return float4(0, 0, 0, 1);
                return original;
            }
        ENDHLSL
        }
    }
}
