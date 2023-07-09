using System.Threading;

namespace WebKafkaTry.Reader.Kakfa
{
    public interface INoteConsumer
    {
        void LaunchConsume(CancellationToken cancellationToken);
    }
}
