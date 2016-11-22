using Astute.Entity;

namespace Astute.Communication
{
    public static class OutputConvertors
    {
        public static string CommandToString(Command command)
        {
            return command.ToString().ToUpper() + "#";
        }
    }
}