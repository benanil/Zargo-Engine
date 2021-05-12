using ImGuiNET;
using System.Numerics;
using System;

namespace ZargoEngine.Editor
{
    public class GameViewWindow 
    {
        private bool isOpen = true;

        private Game window;

        private bool _hovered;
        public bool Hovered{
            get{
                return _hovered;
            }
            private set{
                _hovered = value;
            }
        }

        private bool _focused;
        public bool Focused
        {
            get{
                return _focused;
            }
            private set{
                _focused = value;
            }
        }

        private OpenTK.Mathematics.Vector2i _scale;
        public OpenTK.Mathematics.Vector2i Scale{
            get{
                return _scale;
            }
            set{
                // value changed
                if (value != _scale && Scale != OpenTK.Mathematics.Vector2i.Zero && Scale.X != 0 && Scale.Y != 0)
                {
                    _scale = value;
                    Debug.Log("value Changed: " + _scale);
                    
                    Program.MainGame.frameBuffer.invalidate(_scale);
                }
            }
        }

        public static GameViewWindow instance;

        Debug.SlowDebugger slowDebugger = new Debug.SlowDebugger(1);

        public GameViewWindow(Game window)
        {
            instance = this;
            this.window = window;
        }

        float cooldown;
        const float cooldownTime = .4f;

        public unsafe void Render()
        {
            ImGui.Begin("Game Window", ref isOpen,ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);

            Vector2 windowSize = GetlargestSizeForViewport();
            Vector2 windowPos = GetCenteredPositionForView(windowSize);

            int textureID = window.GetFrameBuffer().GetTextureId();

            ImGui.SetCursorPos(windowPos);
            if (cooldown <= 0){
                ImGui.Image((IntPtr)textureID, windowSize, new Vector2(0, 1), new Vector2(1, 0));
                cooldown = cooldownTime;
            }
            cooldown -= Time.DeltaTime;

            Focused = ImGui.IsWindowFocused();
            Hovered = ImGui.IsWindowHovered();

            Scale = new OpenTK.Mathematics.Vector2i((int)ImGui.GetWindowWidth(), (int)ImGui.GetWindowHeight());

            /*
            var entity = Program.MainGame.firstObject;
            
            if (entity != null)
            {
                ImGuizmo.SetOrthographic(false);
                ImGuizmo.SetDrawlist();
                ImGuizmo.SetRect(ImGui.GetWindowPos().X, ImGui.GetWindowPos().Y, ImGui.GetWindowWidth(), ImGui.GetWindowHeight());
            
                ImGuizmo.Manipulate(ref Camera.main.ViewMatrix.Row0.X, ref Camera.main.projectionMatrix.Row0.X,OPERATION.TRANSLATE,
                                    MODE.LOCAL,ref entity.transform.Translation.Row0.X);
            }
            */
            ImGui.End();
        }

        private Vector2 GetlargestSizeForViewport()
        {
            var windowSize = ImGui.GetContentRegionAvail();

            windowSize.X -= ImGui.GetScrollX();
            windowSize.Y += ImGui.GetScrollY();

            float aspectWidth = windowSize.X;
            float aspectHeight = aspectWidth / window.AspectRatio();

            if (aspectWidth > windowSize.Y)
            {
                aspectHeight = windowSize.Y;
                aspectWidth = aspectHeight * window.AspectRatio();
            }

            return new Vector2(aspectWidth,aspectHeight);
        }

        private static Vector2 GetCenteredPositionForView(Vector2 aspectSize)
        {
            var windowSize = ImGui.GetContentRegionAvail();

            windowSize.X -= ImGui.GetScrollX();
            windowSize.Y += ImGui.GetScrollY();

            float viewportX = (windowSize.X / 2.0f) - (aspectSize.X / 2.0f);
            float viewportY = (windowSize.Y / 2.0f) - (aspectSize.Y / 2.0f);

            return new Vector2(viewportX + ImGui.GetCursorPosX(),
                               viewportY + ImGui.GetCursorPosY());
        }
    }
}
