namespace Minixer.Infrastructure.RemoteFramebuffer
{
    public interface IVncInputPolicy
    {
        void WriteKeyboardEvent(uint keysym, bool pressed);

        void WritePointerEvent(byte buttonMask, Point point);
    }
}