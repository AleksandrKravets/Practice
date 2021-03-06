﻿using System.Threading.Tasks;

namespace Framework.Commands
{
    public class ExecuteWithReturnValueCommand : ExecuteWithResponseValueCommand<int>
    {
        public ExecuteWithReturnValueCommand(StoredProcedure storedProcedure, string connectionString)
            : base(storedProcedure, connectionString)
        {
        }

        protected async override Task<int> ExecuteProcedureAsync()
        {
            var rowsAffected = await _command.ExecuteNonQueryAsync();
            return rowsAffected;
        }

        protected override Task PostExecution()
        {
            FillProcedureWithReturnValue();
            return base.PostExecution();
        }
    }
}
