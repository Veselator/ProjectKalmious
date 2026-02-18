Shader "Custom/LaserPlatformShader"
{
    Properties
    {
        _EdgeColor ("Edge Color", Color) = (1, 0, 0, 1)
        _CenterColor ("Center Color", Color) = (1, 1, 1, 1)
        _Radius ("Radius", Range(0, 0.5)) = 0.3
        _EdgeWidth ("Edge Width", Range(0, 0.5)) = 0.1
        _Smoothness ("Smoothness", Range(0.01, 0.5)) = 0.1
        _Brightness ("Brightness", Range(0, 5)) = 1.5

        _PulseSpeed ("Pulse Speed", Float) = 2.0
        _PulseAmount ("Pulse Amount", Range(0, 1)) = 0.3
        _NoiseScale ("Noise Scale", Float) = 10.0
        _NoiseAmount ("Noise Amount", Range(0, 0.5)) = 0.1
        _RotationSpeed ("Noise Rotation Speed", Float) = 1.0
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _EdgeColor;
            fixed4 _CenterColor;
            float _Radius;
            float _EdgeWidth;
            float _Smoothness;
            float _Brightness;
            float _PulseSpeed;
            float _PulseAmount;
            float _NoiseScale;
            float _NoiseAmount;
            float _RotationSpeed;

            float noise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);
                float2 uv = i.uv - center;

                float dist = length(uv);

                float angle = atan2(uv.y, uv.x);
                float2 noiseUV = float2(angle * _NoiseScale, dist * _NoiseScale);
                noiseUV.x += _Time.y * _RotationSpeed;
                float noiseValue = noise(noiseUV);
                dist += noiseValue * _NoiseAmount;

                float pulse = sin(_Time.y * _PulseSpeed) * _PulseAmount + 1.0;

                float distFromEdge = abs(dist - _Radius);
                float t = smoothstep(_EdgeWidth - _Smoothness, _EdgeWidth + _Smoothness, distFromEdge);

                fixed4 color = lerp(_CenterColor, _EdgeColor, t);
                color.rgb *= _Brightness * pulse;

                float outerFade = 1.0 - smoothstep(_Radius + _EdgeWidth, _Radius + _EdgeWidth + _Smoothness, dist);
                float innerFade = smoothstep(_Radius - _EdgeWidth - _Smoothness, _Radius - _EdgeWidth, dist);
                color.a *= outerFade * innerFade;

                return color;
            }
            ENDCG
        }
    }

    FallBack "Sprites/Default"
}