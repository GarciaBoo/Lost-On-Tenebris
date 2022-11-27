using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LostOnTenebris
{
    // Source: https://samdriver.xyz/article/scriptable-render
    public class BlitRenderPass : ScriptableRenderPass
    {
        private string profilerTag;

        private Material materialToBlit;
        private RenderTargetIdentifier cameraColorTargetIdentifier;
        private RenderTargetHandle tmpTexture;

        public BlitRenderPass(string profilerTag, RenderPassEvent renderPassEvent, Material materialToBlit)
        {
            this.profilerTag = profilerTag;
            this.renderPassEvent = renderPassEvent;
            this.materialToBlit = materialToBlit;
        }

        public void Setup(RenderTargetIdentifier cameraColorTargetIdentifier)
        {
            this.cameraColorTargetIdentifier = cameraColorTargetIdentifier;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tmpTexture.id, cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();
            
            cmd.Blit(cameraColorTargetIdentifier, tmpTexture.Identifier(), materialToBlit, -1);
            cmd.Blit(tmpTexture.Identifier(), cameraColorTargetIdentifier);
            context.ExecuteCommandBuffer(cmd);
            
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tmpTexture.id);
        }
    }
}