Shader "Unlit/CutOutShader"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
               Zwrite On
            ColorMask 0
        }
    }
}
