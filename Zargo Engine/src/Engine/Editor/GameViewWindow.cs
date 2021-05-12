using ImGuiNET;
using System.Numerics;
using System;

namespace ZargoEngine.Editor
{
    public class GameViewWindow
    {
        public static GameViewWindow instance { get; private set;}
        public bool Hovered { get; private set; }
        public bool Focused { get; private set; }

        private bool isOpen = true;

        private readonly Game window;

        public GameViewWindow(Game window)
        {
            instance = this;
            this.window = window;
        }

        public unsafe void Render()
        {
            ImGui.Begin("Game Window", ref isOpen, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse);

            Hovered = ImGui.IsWindowHovered();
            Focused = ImGui.IsWindowFocused();

            Vector2 windowSize = GetlargestSizeForViewport();
            Vector2 windowPos = GetCenteredPositionForView(windowSize);

            int textureID = window.GetFrameBuffer().GetTextureId();

            ImGui.SetCursorPos(windowPos);

            ImGui.Image((IntPtr)textureID, windowSize, new Vector2(0, 1), new Vector2(1, 0));

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

            return new Vector2(aspectWidth, aspectHeight);
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