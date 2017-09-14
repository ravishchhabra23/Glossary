
namespace GlossaryLogging
{
    public interface ILogger
    {
        void WriteLog(string message, bool isError);
    }
}
