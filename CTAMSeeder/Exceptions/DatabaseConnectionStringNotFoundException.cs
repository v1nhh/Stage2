using System;
using System.Runtime.Serialization;

namespace CTAMSeeder
{
    [Serializable]
    internal class DatabaseConnectionStringNotFoundException : Exception
    {
        public DatabaseConnectionStringNotFoundException() : base("Database connectionstring could not be found in appsettings.")
        {
        }

        public DatabaseConnectionStringNotFoundException(string message) : base(message)
        {
        }

        public DatabaseConnectionStringNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DatabaseConnectionStringNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}