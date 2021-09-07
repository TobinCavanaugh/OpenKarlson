Shader "AdvancedShaderDevelopment/OutlineShader"
{
    Properties
    {
        _MainColor("Main Color", Color)=(1,1,1,1)
        _MainTexture("Main Texture", 2D)="white"{}
        _OutlineColor("Outline Color", Color)=(1,1,1,1)
        _OutlineSize("OutlineSize", Range(1.0,1.5))=1.1
    }
    SubShader
    {
        //Pass
        //{
        //    CGPROGRAM
        //    #pragma vertex vert
        //    #pragma fragment frag
        //    #include "UnityCG.cginc"
        //    fixed4 _MainColor;
        //    sampler2D _MainTexture;
        //    float4 _MainTexture_ST;
        //    struct appdata
        //    {
        //        float4 vertex:POSITION;
        //        float2 uv:TEXCOORD0;
        //    };
        //    struct v2f
        //    {
        //        float4 clipPos:SV_POSITION;
        //        float2 uv:TEXCOORD0;
        //    };
        //    v2f vert (appdata v)
        //    {
        //        v2f o;
        //        o.clipPos=UnityObjectToClipPos(v.vertex);
        //        o.uv=(v.uv*_MainTexture_ST.xy)+_MainTexture_ST.zw;
        //        return o;
        //    }
        //    fixed4 frag (v2f i) : SV_Target
        //    {
        //        fixed4 col;
        //        col=tex2D(_MainTexture, i.uv)*_MainColor;
        //        return col;
        //    }
        //    ENDCG
        //}
        Pass
        {
            Cull Front
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            fixed4 _OutlineColor;
            float _OutlineSize;
            struct appdata
            {
                float4 vertex:POSITION;
            };
            struct v2f
            {
                float4 clipPos:SV_POSITION;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.clipPos=UnityObjectToClipPos(v.vertex*_OutlineSize);
                return o;
            }
            fixed4 frag (v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}