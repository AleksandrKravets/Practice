using System.Threading.Tasks;

namespace Framework.Commands
{
    public class ExecuteWithObjectResponseCommand<TResult> : ExecuteWithResponseValueCommand<TResult> where TResult : class
    {
        public ExecuteWithObjectResponseCommand(StoredProcedure storedProcedure, string connectionString)
            : base(storedProcedure, connectionString)
        {
        }

        protected async override Task<TResult> ExecuteProcedureAsync()
        {
            using (var reader = await _command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                    return null;

                reader.Read();
                var result = reader.ReadObject<TResult>();
                reader.Close();

                return result;
            }
        }
    }
}
