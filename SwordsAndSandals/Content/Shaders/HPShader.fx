#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler inputTexture;
float precentage;

float4 MainPS(float2 coords : TEXCOORD0) : COLOR
{
    float4 original = tex2D(inputTexture, coords);
    if (1 - coords.y <= precentage && original.a != 0)
    {
        original.r = 0.5f;
    }
    return original;
}

technique BasicColorDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
        AlphaBlendEnable = TRUE;
        DestBlend = INVSRCALPHA;
        SrcBlend = SRCALPHA;
    }
};