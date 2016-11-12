using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Linq;
using System.Text;

namespace Astute
{
    public static class Input
    {
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

                            observer.OnNext(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        exception = ex;
                    }
                    finally
                    {
                        if (exception != null)
                            observer.OnError(exception);
                        else
                            observer.OnCompleted();
                    }

                    return () => Console.WriteLine("Observer has unsubscribed");
                }
            );
    }
}