using Microsoft.AspNetCore.SignalR;
using Minixer.Infrastructure.RemoteFramebuffer;
using Minixer.Infrastructure.RemoteFramebuffer.Encodings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Minixer.Hubs
{
    
    public class Update
    {
        public Rectangle Rectangle { get; set; }
        public Pixel[] PixelData { get; set; }
    }

    public class ViewerHub : Hub
    {
        private static ConcurrentDictionary<string, VncClient> openVirtualMachines = new ConcurrentDictionary<string, VncClient>();

        public async Task OpenVirtualMachine(int machineId)
        {
            var vncClient = new VncClient();

            vncClient.VncUpdate += VncUpdate;
            vncClient.Connect("45.33.84.168", 0);
            vncClient.Initialize(32, 24);
            vncClient.StartUpdates();

            await Task.CompletedTask;
        }

        public ChannelReader<Update> OpenVirtualMachineChannel(
            int machineId)
        {
            
            var channel = Channel.CreateUnbounded<Update>();

            _ = GetUpdates(channel.Writer, new CancellationToken());



            return channel.Reader;
        }

        private async Task GetUpdates(
        ChannelWriter<Update> writer,
        CancellationToken cancellationToken)
        {
            try
            {
                var vncClient = new VncClient();
                var updates = 0;
                ConcurrentQueue<Update> cq = new ConcurrentQueue<Update>();
                BufferBlock<Update> buffer = new BufferBlock<Update>();
                vncClient.VncUpdate += (s, e) =>
                {
                    //Debug.WriteLine("Got new update");
                    var update = new Update()
                    {
                        PixelData = e.DesktopUpdater.PixelData,
                        Rectangle = e.DesktopUpdater.UpdateRectangle
                    };

                    buffer.Post(update);

                    //Debug.Write("Posted new update");
                };
                vncClient.Connect("45.33.84.168", 0);
                vncClient.Initialize(32, 24);
                vncClient.StartUpdates();
                vncClient.WriteKeyboardEvent(1, true);

                // Check the cancellation token regularly so that the server will stop
                // producing items if the client disconnects.
                cancellationToken.ThrowIfCancellationRequested();
                //

                // Use the cancellationToken in other APIs that accept cancellation
                // tokens so the cancellation can flow down to them.
                //await Task.Delay(delay, cancellationToken);
                Stopwatch sw = new Stopwatch();
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Check the cancellation token regularly so that the server will stop
                    // producing items if the client disconnects.
                    sw.Start();
                    Debug.WriteLine("Sending update");
                    var item = await buffer.ReceiveAsync();
                    if (!writer.TryWrite(item))
                    {

                    }
                    sw.Stop();

                    Debug.WriteLine($"Sent update #{updates} in {sw.Elapsed}");

                    updates++;

                    sw.Reset();
                    // Use the cancellationToken in other APIs that accept cancellation
                    // tokens so the cancellation can flow down to them.
                    //await Task.Delay(250, cancellationToken);

                }

            }
            catch (Exception ex)
            {
                writer.TryComplete(ex);
            }

            writer.TryComplete();
        }


        private void VncUpdate(object sender, VncEventArgs e)
        {
            var rectangle = e.DesktopUpdater.UpdateRectangle;
            Task.Run(() => Clients.All.SendAsync("IncomingPixesl",
                rectangle.X,
                rectangle.Y,
                rectangle.Height,
                rectangle.Width));
            
            //e.DesktopUpdater.Draw(desktop);
            //Invalidate(desktopPolicy.AdjustUpdateRectangle(e.DesktopUpdater.UpdateRectangle));

            //if (state != RuntimeState.Connected) return;

            //// Make sure the next screen update is incremental
            //vnc.FullScreenRefresh = false;
        }

        public async Task FramebufferUpdateRequest()
        {
            VncClient connectionInfo = null;

            if (!openVirtualMachines.TryGetValue(Context.ConnectionId, out connectionInfo))
            {
                throw new Exception("Nope");
            }

            //await connectionInfo.FramebufferUpdateRequest();
            await Task.CompletedTask;
        }

        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
