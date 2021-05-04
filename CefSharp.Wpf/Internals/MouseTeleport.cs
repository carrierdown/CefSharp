using CefSharp.Structs;

namespace CefSharp.Wpf.Internals
{
    public class MouseTeleport
    {
        public Point teleportUpperLeftCorner { get; private set; }
        public Point teleportLowerRightCorner { get; private set; }
        public int xOffset { get; private set; }
        public int yOffset { get; private set; }

        public void Update(int xOffset, int yOffset, Point teleportUpperLeftCorner, Point teleportLowerRightCorner)
        {
            this.teleportUpperLeftCorner = teleportUpperLeftCorner;
            this.teleportLowerRightCorner = teleportLowerRightCorner;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
        }

        public void Reset()
        {
            teleportUpperLeftCorner = new Point();
            teleportLowerRightCorner = new Point();
            xOffset = 0;
            yOffset = 0;
        }

        private bool IsInsideTeleportArea(int x, int y)
        {
            return x >= this.teleportUpperLeftCorner.X &&
                   x < this.teleportLowerRightCorner.X &&
                   y >= this.teleportUpperLeftCorner.Y &&
                   y < this.teleportLowerRightCorner.Y;
        }

        public Point GetAdjustedMouseCoords(int x, int y)
        {
            return IsInsideTeleportArea(x, y) ? new Point(x + xOffset, y + yOffset) : new Point(x, y);
        }
    }
}
