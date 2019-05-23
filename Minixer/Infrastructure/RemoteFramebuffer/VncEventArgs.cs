using Minixer.Infrastructure.RemoteFramebuffer.Encodings;
using System;

namespace Minixer.Infrastructure.RemoteFramebuffer
{
    public class VncEventArgs : EventArgs
    {
        public VncEventArgs(EncodedRectangle updater)
        {
            DesktopUpdater = updater;
        }

        /// <summary>
        /// Gets the IDesktopUpdater object that will handling re-drawing the desktop.
        /// </summary>
        public EncodedRectangle DesktopUpdater { get; }
    }
}