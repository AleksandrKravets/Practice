﻿using System.Data;

namespace Framework
{
    public class StoredProcedureParameter
    {
        public string ParameterName { get; private set; }
        public object ParameterValue { get; private set; }
        public ParameterDirection ParameterDirection { get; private set; }

        public StoredProcedureParameter(string parameterName, object parameterValue, ParameterDirection parameterDirection)
        {
            ParameterName = parameterName;
            ParameterValue = parameterValue;
            ParameterDirection = parameterDirection;
        }
    }
}
