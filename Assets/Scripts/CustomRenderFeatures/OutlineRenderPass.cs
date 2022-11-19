using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LostOnTenebris
{
    // No momento completamente hardcoded
    public class OutlineRenderPass : ScriptableRenderPass
    {
        private string profilerTag;

        private Material materialToBlit;
        private RenderTargetIdentifier cameraColorTargetIdentifier;
        
        private RenderTargetHandle tmp_Silhouette;
        private RenderTargetHandle tmp_Blur_1;
        private RenderTargetHandle tmp_Blur_2;
        private RenderTargetHandle tmp_Final;

        public OutlineRenderPass(string profilerTag, RenderPassEvent renderPassEvent, Material materialToBlit)
        {
            this.profilerTag = profilerTag;
            this.renderPassEvent = renderPassEvent;
            this.materialToBlit = materialToBlit;
            
            tmp_Silhouette.Init("tmp_silhouette");
            tmp_Blur_1.Init("tmp_blur_1");
            tmp_Blur_2.Init("tmp_blur_2");
            tmp_Final.Init("tmp_final");
        }

        public void Setup(RenderTargetIdentifier cameraColorTargetIdentifier)
        {
            this.cameraColorTargetIdentifier = cameraColorTargetIdentifier;
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(tmp_Silhouette.id, cameraTextureDescriptor);
            cmd.GetTemporaryRT(tmp_Blur_1.id, cameraTextureDescriptor);
            cmd.GetTemporaryRT(tmp_Blur_2.id, cameraTextureDescriptor);
            cmd.GetTemporaryRT(tmp_Final.id, cameraTextureDescriptor);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();
            
            cmd.Blit(cameraColorTargetIdentifier, tmp_Silhouette.Identifier(), materialToBlit, 0);
            cmd.Blit(tmp_Silhouette.Identifier(), tmp_Blur_1.Identifier(), materialToBlit, 1);
            cmd.Blit(tmp_Blur_1.Identifier(), tmp_Blur_2.Identifier(), materialToBlit, 2);
            cmd.Blit(tmp_Blur_2.Identifier(), tmp_Final.Identifier(), materialToBlit, 3);
            cmd.Blit(tmp_Final.Identifier(), cameraColorTargetIdentifier);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tmp_Silhouette.id);
            cmd.ReleaseTemporaryRT(tmp_Blur_1.id);
            cmd.ReleaseTemporaryRT(tmp_Blur_2.id);
            cmd.ReleaseTemporaryRT(tmp_Final.id);
        }
    }
}