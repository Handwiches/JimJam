Shader "Metkis/VHSShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _NoiseTex ("Noise", 2D) = "white" {}
        _OverlayTex ("Overlay", 2D) = "white" {}
        _Distortion ("Distortion", 2D) = "white" {}
        _OverlayRepeat ("Overlay Repeat", Range (-5, 5)) = 0.4
        _OverlaySpeed ("Overlay Speed", Range (-10, 10)) = 3
        _DistortionSpeed ("Distortion Speed", Range (-10, 10)) = 3

    }
 
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off Fog { Mode off }
 
            CGPROGRAM

            #pragma vertex vert alpha
            #pragma fragment frag alpha
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"
            #pragma target 3.0
 
            struct v2f
            {
                float4 pos      : SV_POSITION;
                float2 uv       : TEXCOORD0;
                float4 scr_pos  : TEXCOORD1;
                float2 uv2       : TEXCOORD2;


            };
 
            uniform sampler2D _MainTex;
                    sampler2D _NoiseTex;
                    sampler2D _OverlayTex;
                    sampler2D _Distortion;
                    uniform float4x4 _RotationMatrix;
                    uniform float4 _CenterRadius;
                    uniform float _Angle;
                    uniform float _OverlayRepeat;
                    uniform float _OverlaySpeed;
                    uniform float _DistortionSpeed;






 
            v2f vert(appdata_img v)
            {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = v.texcoord.xy ;
                o.scr_pos = ComputeScreenPos(o.pos);
                o.uv2 = v.texcoord.xy - _CenterRadius.xy;
                return o;
            }

 
            half4 frag(v2f i): SV_Target
            {
            	float3 hazard = i.scr_pos;
                half4 color = tex2D(_MainTex, i.uv);
                hazard.xy = i.uv.xy * (i.scr_pos.w * 2) + i.scr_pos.xy;
                float2 ps = (i.scr_pos.y +_Time *23)  * _ScreenParams.y / i.scr_pos.w  ;
                float2 ps2 =(i.scr_pos.y +_Time.xy *23)  * _ScreenParams.yx / i.scr_pos.w  ;

       			int pp = (int)ps.y  % 3; 
       			int pp2 = (int)ps2.y  % 5;


				float4 outcolor = float4(0, 0, 0, 1);
				if (pp == 1) outcolor.r = color.r -0.5;
   				else if (pp == 2) outcolor.g = color.g-0.5 ;
   				else outcolor.b = color.b -0.1;

   				if (pp2 == 1) outcolor.r = color.g -0.5;
   				else if (pp2 == 2) outcolor.g = color.r-0.5 ;
   				else outcolor.b = color.b -0.2;


   				fixed2 NoiseUV = (_ScreenParams.x/256, _ScreenParams.y/256);
   				color = color + (outcolor *0.3);




			fixed2 scrollUV = i.uv ;
            float timeFrac = _Time.xy* _OverlaySpeed;
            fixed xScrollValue = timeFrac * 2;
            fixed yScrollValue = timeFrac * 2;
            scrollUV += fixed2( xScrollValue, yScrollValue );

            fixed2 scrollUV2 = i.uv2 ;
            float timeFrac2 = _Time;
            fixed xScrollValue2 = timeFrac2 * -55;
            fixed yScrollValue2 = 1;
            scrollUV2 += fixed2( xScrollValue2, yScrollValue2 );

   				color = color *tex2D(_OverlayTex,scrollUV *_OverlayRepeat).rgba / (tex2D(_Distortion, i.uv2.y +-_Time.xy/3)) /((tex2D(_Distortion, scrollUV2))+0.1)/((tex2D(_Distortion, scrollUV2 *55)));





	float2 offset = i.uv2;
	float angle = 1.0 - length(offset / _CenterRadius.zw);
	angle = max (0, angle);
	angle = angle * angle * _Angle;
	float cosLength, sinLength;
	sincos (angle, sinLength, cosLength);


	float2 uvz;
	offset[1] = offset[1];
	uvz.x = cosLength * offset[0] - sinLength * offset[1];
	uvz.y = sinLength * offset[0] + cosLength * offset[1];
	uvz += _CenterRadius.xy;

    _CenterRadius.xy = _Time.y;

	half4 color2 = (tex2D(_MainTex, uvz) * 1.2 * (color +.1));
	return color2;

            }

           
 
            ENDCG
        }
    }
    FallBack "Diffuse"
}