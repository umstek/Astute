using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;
using NLog;

namespace Astute
{
    public static class Input
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IObservable<string> TcpInput { get; } =
            Observable.Create<string>(observer =>
                {
                    Exception exception = null;

                    try
                    {
                        // Create and start the listener.
                        var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);
                        listener.Start();

                        // Listen in an endless loop. 
                        while (true)
                        {
                            string value;

                            using (var networkStream = listener.AcceptTcpClient().GetStream())
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    networkStream.CopyTo(memoryStream);

                                    // TODO check encoding
                                    value = Encoding.UTF8.GetString(memoryStream.ToArray());
                                }
                            }

                            Logger.Info($"Received: {value}");
                            observer.OnNext(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                        Logger.Error(exception);
                    }
                    finally
                    {
                        if (exception != null)
                        {
                            Logger.Warn(exception);
                            observer.OnError(exception);
                        }
                        else
                        {
                            Logger.Info("Observable completed. ");
                            observer.OnCompleted();
                        }
                    }

                    return () => Logger.Info("Observable has unsubscribed. ");
                }
            );
    }
}