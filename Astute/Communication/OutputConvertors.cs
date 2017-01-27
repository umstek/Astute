using System.Diagnostics.Contracts;
using Astute.Entity;

namespace Astute.Communication
{
    public static class OutputConvertors
    {
        [Pure]
        public static string CommandToString(Command command)
        {
            return command.ToString().ToUpper() + "#";
        }
    }
}