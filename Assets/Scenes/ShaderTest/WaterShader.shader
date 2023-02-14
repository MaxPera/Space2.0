Shader "Unlit/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "normal" {}
        _WaterColor("Color", Color) = (1,1,1,1)
        _Speed("Water Speed", float) = 1
        _Amount("Wave Amount", float) = 5
        _Distance("Wave Distance", float) = 0.1
        _Alpha ("Alpha", range(0.0,1.0)) = 1.0
        _SpecularColor("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess" , Float) = 10
    }
    SubShader
    {
        Tags 
        { 
            "RenderType"="Transparent" 
            "Queue" = "Transparent"
            "IgnoreProjector"="True"
        }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        /*GrabPass
        {
            "_BackgroundTexture"
        }*/


        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float3 worldNormal : TEXCOORD1;
                float4 vertex : SV_POSITION;              
                float4 color : COLOR0;
                float3 normal : NORMAL;
                float4 grabPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _NormalMap;
            float4 _MainTex_ST;
            float4 _WaterColor;
            float _Speed;
            float _Amount;
            float _Distance;
            float _Alpha;
            uniform float4 _LightColor0;
            uniform float4 _SpecularColor;
            uniform float _Shininess;
            sampler2D _BackgroundTexture;

            v2f vert (appdata v)
            {
                v2f o;

                v.vertex.y += sin(_Time.z * _Speed + v.vertex.x * _Amount) * _Distance;
                //v.vertex.z += sin(_Time.z * _Speed + v.vertex.y * _Amount) * _Distance;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                o.grabPos = ComputeGrabScreenPos(o.vertex);

                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, i.vertex));
                i.worldNormal = normalize(mul(unity_ObjectToWorld, i.worldNormal));
                i.normal = UnpackNormal(tex2D(_NormalMap,i.uv));

                float NdotL = dot(i.worldNormal, lightDir);             
                // sample the texture
                fixed4 col;
                col.rgb = _WaterColor * _LightColor0.rgb * NdotL;
                col.a = _Alpha;
                
                float3 reflectedDir = reflect(-lightDir, i.worldNormal);
                float rv = max(0.0, dot(reflectedDir, viewDir));

                float specularAmount = pow(rv, _Shininess);

                float specularLight = _SpecularColor.rgb * _LightColor0.rgb * specularAmount;

                col.rgb += specularLight;
                
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                fixed4 bgcolor = tex2Dproj(_BackgroundTexture, i.grabPos);
                bgcolor += col;

                return col;
            }

            ENDCG
        }
    }
}
