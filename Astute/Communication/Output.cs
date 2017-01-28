using System;
using System.Net.Sockets;
using System.Reactive;
using System.Text;
using NLog;
using static Astute.Configuration.Configuration;

namespace Astute.Communication
{
    public static class Output
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     Sends observed stream to server via TCP.
        /// </summary>
        public static IObserver<string> TcpOutput { get; } =
            Observer.Create<string>(stringOutput =>
                {
                    try
                    {
                        Logger.Trace($"Sending: {stringOutput}");
                        using (var client = new TcpClient(Config.SendToIP, Config.SendToPort))
                        {
                            var data = Encoding.UTF8.GetBytes(stringOutput);
                            client.GetStream().Write(data, 0, data.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }, exception => Logger.Error(exception), () => Logger.Info("Observable sequence completed. ")
            );
    }
}