using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LostOnTenebris
{
    public class OutlineFeature : ScriptableRendererFeature
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
        private OutlineRenderPass renderPass;
        
        public override void Create()
        {
            renderPass = new OutlineRenderPass("Custom Blit Pass", settings.WhenToInsert, settings.MaterialToBlit);
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