﻿// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/waterfallStyle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor ("Base color", COLOR)  = ( .54, .95, .99, 0.5) 
        _Color("Colour", Color) = (0,1,0,1)
        _AnimationSpeed("Animation speed", Range(0,3)) = 0
        _waveAmplitude("Amp range", Range(0,10)) = 0
        _Transparency("Transparency", Range(0.0,1)) = 0.99
        _FallPlacement("Fall Placement", float) = 0
        _FallLength("Fall Length", int) = 4
        _isX("Is edge position at x", int) = 0

       
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Cull Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"
            uniform float4 _BaseColor;  

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 tangent : TANGENT;  
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 position : SV_POSITION;
                float3 normal : TEXCOORD1;
                float4 viewToPixel : TEXCOORD2;
                float4 normalInterpolator : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float4 posWorld : TEXCOORD5;
                float displ : TEXCOORD6;
                float transparencyFromFall : TEXCOORD7;
                
                
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _Color;
            float _AnimationSpeed;
            float _waveAmplitude;
            float _Transparency;
            float4 _edgePosition;
            float _FallPlacement;
            float _FallLength;
            float _isX;
            
            

            
            v2f vert (appdata v)
            {   
                v2f o;
                float3 objectOrigin = mul(unity_ObjectToWorld, float4(0.0,1.0,0.0,0.0) );
                float3 worldPos = mul (unity_ObjectToWorld, v.vertex).xyz;
                half3 offsets = half3(0,0,0);

                float3 placement = _isX*(worldPos.x-objectOrigin.x)+(1-_isX)*(worldPos.z-objectOrigin.z);
                float displacement = ((_waveAmplitude*sin(_Time.y*_AnimationSpeed + (worldPos.x+worldPos.z)*1000)/2300+_waveAmplitude*sin(_Time.y*_AnimationSpeed + (worldPos.x-worldPos.z)*500)/1000)-0.002);
                
                float fall = (0-step(0.3, 1 + placement)); //the last part goes from -1 to 0 and should output 0.1 to 5
                fall = step(_FallPlacement, 1 + placement)/(30/_FallLength);
                o.transparencyFromFall = 1-step(_FallPlacement-0.95, placement);

                v.vertex.z += displacement - fall;

                o.position = UnityObjectToClipPos(v.vertex);
                
                o.displ = displacement;

                o.normal = mul(
                    transpose((float3x3)unity_WorldToObject),
                    v.normal);

                return o;
            }
            
            //inspired by  https://github.com/danielzeller/Lowpoly-Water-Unity/blob/master/Assets/Shaders/FlatShadedWaterWithEdgeBlend.shader
         half4 calculateBaseColor(v2f input)  
         {
         
            float3 ambientLighting = 
               UNITY_LIGHTMODEL_AMBIENT.rgb * _BaseColor.rgb;

            ambientLighting = float3(0.2,0.6+60*input.displ,0.8+(20*input.displ));


            return half4(ambientLighting, .9);
         }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col = calculateBaseColor(i);
                float tft = i.transparencyFromFall;
                col.a = step(0.0,i.normal.y)*_Transparency*tft;
                return col;


            }
            ENDCG
        }
    }
}

     