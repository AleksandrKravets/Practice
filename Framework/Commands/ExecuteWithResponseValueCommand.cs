﻿using System.Threading.Tasks;

namespace Framework.Commands
{
    public abstract class ExecuteWithResponseValueCommand<TResult> : ExecutorCommandBase
    {
        public ExecuteWithResponseValueCommand(StoredProcedure storedProcedure, string connectionString)
            : base(storedProcedure, connectionString)
        {
        }

        public async virtual Task<TResult> ExecuteAsync()
        {
            await PreExecution();
            TResult result = await ExecuteProcedureAsync();
            await PostExecution();
            return result;
        }

        protected abstract Task<TResult> ExecuteProcedureAsync();
    }
}
