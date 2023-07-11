using System.Runtime.Serialization;

namespace Siddhupagal.Controllers
{
    [Serializable]
    internal class DbUpdateCocurrentException : Exception
    {
        public DbUpdateCocurrentException()
        {
        }

        public DbUpdateCocurrentException(string? message) : base(message)
        {
        }

        public DbUpdateCocurrentException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbUpdateCocurrentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}