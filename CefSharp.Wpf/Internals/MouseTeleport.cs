using System;
using System.Diagnostics;
using CefSharp.Structs;

namespace CefSharp.Wpf.Internals
{
    public class MouseTeleport
    {
        public Rect teleportingRect { get; private set; }
        public Rect originalRect { get; private set; }
        public int xOffset { get; private set; }
        public int yOffset { get; private set; }

        private Stopwatch stopwatch;

        public bool isActive {
            get
            {
                return originalRect.Width > 0;
            }
        }

        private int mouseUpCounter;

        public void Update(int xOffset, int yOffset, Rect originalRect, Rect teleportingRect)
        {
            // Console.WriteLine("Update called");
            this.originalRect = originalRect;
            this.teleportingRect = teleportingRect;
            this.xOffset = xOffset;
            this.yOffset = yOffset;

            if (this.originalRect.Y < this.teleportingRect.Y + this.teleportingRect.Height)
            {
                var newY = this.teleportingRect.Y + this.teleportingRect.Height;
                this.originalRect = new Rect(originalRect.X, newY, originalRect.Width, originalRect.Y + originalRect.Height - newY);
            }
        }

        public void Reset()
        {
            // Console.WriteLine("Reset called");
            originalRect = new Rect();
            xOffset = 0;
            yOffset = 0;
            mouseUpCounter = 0;
            stopwatch = new Stopwatch();
        }

        public bool ShouldClickPropagate()
        {
            return ++mouseUpCounter > 1;
        }

        public bool IsInsideOriginalRect(int x, int y)
        {
            return x >= originalRect.X &&
                   x < originalRect.X + originalRect.Width &&
                   y >= originalRect.Y &&
                   y < originalRect.Y + originalRect.Height;
        }

        public bool IsInsideTeleportingRect(int x, int y)
        {
            return x >= teleportingRect.X &&
                   x < teleportingRect.X + teleportingRect.Width &&
                   y >= teleportingRect.Y &&
                   y < teleportingRect.Y + teleportingRect.Height;
        }

        public Point GetAdjustedMouseCoords(int x, int y)
        {
            return IsInsideTeleportingRect(x, y) ? new Point(x + xOffset, y + yOffset) : new Point(x, y);
        }
    }
}
