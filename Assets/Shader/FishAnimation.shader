Shader "Custom/FishAnimation" 
{
    Properties 
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _Color("阴影的颜色（只需调整透明度，谁家的影子都是黑色的)",color)=(1,1,1,0.23)
		_ColumnCounts ("列数", Float) = 4
		_RowCounts ("行数", Float) = 4
		_TotleNum("精灵个数", float) = 11
		_Speed ("播放速度", Float) = 200
    }
   
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Lighting Off 
        Cull Off 
        Fog { Mode Off } 
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 110
        
        Pass 
        {
 			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            sampler2D _MainTex;
			uniform fixed _ColumnCounts;
			uniform fixed _RowCounts;
			uniform fixed _Speed;
			uniform fixed _TotleNum;
			uniform float4 _Color;
            struct v2f
            {
	            float4 pos:POSITION;
	            float2 texcoord:TEXCOORD0;
            };
            
            v2f vert(appdata_full i)
            {
	            v2f v;
	            v.pos=mul(UNITY_MATRIX_MVP,i.vertex);
	            v.texcoord=i.texcoord;
	            return v;
            }
            
            float4 frag(v2f i):SV_Target
            {
				float totalSpriteCount = _ColumnCounts * _RowCounts;
				float rowAvgPercent = 1 / _ColumnCounts;
				float columnAvgPercent = 1 / _RowCounts;        
				float SpriteIndex = fmod(_Time.x * _Speed,_TotleNum);        
				SpriteIndex = floor(SpriteIndex);        
				float columnIndex = fmod(SpriteIndex,_ColumnCounts);
				float rowIndex = SpriteIndex / _ColumnCounts;
				rowIndex = _RowCounts - 1 - floor(rowIndex);
				float2 spriteUV = i.texcoord;
				spriteUV.x = (spriteUV.x + columnIndex) * rowAvgPercent;
				spriteUV.y = (spriteUV.y + rowIndex) * columnAvgPercent;            
				float4 col = tex2D(_MainTex,spriteUV);
				return col*_Color; 
            }
            ENDCG
        }
    }
}