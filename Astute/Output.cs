using System;
using System.Net.Sockets;
using System.Reactive;
using System.Text;

namespace Astute
{
    public static class Output
    {
        public static IObserver<string> TcpOutput { get; } =
            Observer.Create<string>(stringOutput =>
                {
                    try
                    {
                        using (var client = new TcpClient("127.0.0.1", 6000))
                        {
                            var data = Encoding.UTF8.GetBytes(stringOutput);
                            client.GetStream().Write(Encoding.ASCII.GetBytes(stringOutput), 0, data.Length);
                        }
                    }
                    catch (Exception)
                    {
                        // TODO Log exception
                        // throw;
                    }
                }
            );
    }
}