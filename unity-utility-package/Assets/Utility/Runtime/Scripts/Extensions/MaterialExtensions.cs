using UnityEngine;

namespace MrWatts.Internal.Utilities
{
    public static class MaterialExtensions
    {
        private static Color clippingBorderColor = new(1, 0.92f, 0.016f, 1);

        public static void ApplyOpaqueRenderingMode(this Material material)
        {
            material.renderQueue = -1;
            material.DisableKeyword("_ALPHATEST_ON");
            material.DisableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            material.SetInt("_ZWrite", 1);
            material.SetOverrideTag("RenderType", string.Empty);
        }

        public static void ApplyFadeRenderingMode(this Material material)
        {
            material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            material.DisableKeyword("_ALPHATEST_ON");
            material.EnableKeyword("_ALPHABLEND_ON");
            material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            material.SetInt("_ZWrite", 0);
            material.SetOverrideTag("RenderType", "Transparent");
        }

        public static void EnableClipping(this Material material)
        {
            material.EnableKeyword("_CLIPPING_BORDER");

            material.SetFloat("_ClippingBorderWidth", 0.03f);
            material.SetColor("_ClippingBorderColor", clippingBorderColor * 6);

            material.SetFloat("_CullMode", 0);
        }
    }
}