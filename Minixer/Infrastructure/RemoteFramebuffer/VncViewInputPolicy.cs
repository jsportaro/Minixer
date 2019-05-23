using System.Diagnostics;

namespace Minixer.Infrastructure.RemoteFramebuffer
{
    public sealed class VncViewInputPolicy : IVncInputPolicy
    {
        public VncViewInputPolicy(RfbProtocol rfb)
        {
            Debug.Assert(rfb != null);
        }

        public void WriteKeyboardEvent(uint keysym, bool pressed)
        {
        }

        public void WritePointerEvent(byte buttonMask, Point point)
        {
        }
    }
}