using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

namespace ZargoEngine.Rendering
{
    public class FrameBuffer : IDisposable
    {
        private int fboID;

        private Texture texture;

        public FrameBuffer(int width,int height)
        {
            invalidate(new Vector2i(width, height));
        }

        public void invalidate(Vector2i scale)
        { 
            fboID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboID);

            texture = new Texture(scale.X,scale.Y);

            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, texture.texID, 0);

            int rbo = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,rbo);

            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, scale.X, scale.Y);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, rbo);

            if (GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer) != FramebufferErrorCode.FramebufferComplete)
            {
                Debug.LogError("frame buffer failed");
            }
            Unbind();
        }

        public void Bind()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, fboID);
        }

        public static void Unbind()
        { 
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

        public int GetTextureId()
        {
            return texture.texID;
        }

        public void Dispose()
        {
            GL.DeleteFramebuffer(fboID);
            GC.SuppressFinalize(this);
        }
    }
}
