using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace MiddleGames.Engine.Rendering
{
    public class Camera
    {
        public enum CameraRenderMode
        { 
            Orthographic, Perspective
        }

        // Size of the viewport
        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        // Near = the closest a vertex can be before not being rendered
        public float NearPlane { get; set; }
        // Far = rendering distance basically. if vertex is too far away, dont render.
        public float FarPlane { get; set; }
        // Fov is how much is seen on screen sort of.
        public float FOV { get; set; }

        // The matrix containing information for projecting the worldview matrix
        public Matrix4 Projection { get; set; }
        // A matrix containing info about how to move around the world around the camera... sort of
        // this is normally set as a player's worldview
        public Matrix4 WorldView { get; set; }

        public Camera(CameraRenderMode renderMode,float fov, int ViewportWidth , int ViewportHeight, float far = 10000, float near = .1f)
        {
            this.ViewportHeight = ViewportHeight;
            this.ViewportWidth = ViewportWidth;

            NearPlane = near;
            FarPlane = far;

        }

        public void ReCalculate()
        { 
            Projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), ViewportWidth / ViewportHeight, NearPlane, FarPlane);
            WorldView = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
        }

        // Returns the camera's viewing matrix
        public Matrix4 Matrix()
        {
            return Projection * WorldView;
        }

        public void UseViewport()
        {
            GL.Viewport(0, 0, ViewportWidth, ViewportHeight);
        }
    }
}