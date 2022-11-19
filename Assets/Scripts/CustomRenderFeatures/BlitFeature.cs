using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LostOnTenebris
{
    // Source: https://samdriver.xyz/article/scriptable-render
    public class BlitFeature : ScriptableRendererFeature
    {
        [Serializable]
        public class FeatureSettings
        {
            public bool IsEnabled = true;
            public RenderPassEvent WhenToInsert = RenderPassEvent.AfterRendering;
            public Material MaterialToBlit;
        }

        public FeatureSettings settings = new FeatureSettings();

        private RenderTargetHandle renderTextureHandle;
        private BlitRenderPass renderPass;
        
        public override void Create()
        {
            renderPass = new BlitRenderPass("Custom Blit Pass", settings.WhenToInsert, settings.MaterialToBlit);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!settings.IsEnabled) return;

            RenderTargetIdentifier cameraColorTargetIdentifier = renderer.cameraColorTarget;
            renderPass.Setup(cameraColorTargetIdentifier);
            
            renderer.EnqueuePass(renderPass);
        }
    }
}