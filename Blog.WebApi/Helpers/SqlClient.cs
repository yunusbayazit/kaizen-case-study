using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Helpers
{
    public interface ISqlClient
    {
        string ExecuteProcedureReturnString(string procName, params SqlParameter[] paramters);
        List<TData> ExecuteProcedureReturnData<TData>(string procName,
            params SqlParameter[] parameters);
    }

    public class SqlClient: ISqlClient
    {
        private IConfiguration _configuration;
        private IMapper _mapper;
        public SqlClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public string ExecuteProcedureReturnString(string procName,
            params SqlParameter[] paramters)
        {
            string result = "";
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
            {
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = procName;
                    if (paramters != null)
                    {
                        command.Parameters.AddRange(paramters);
                    }
                    sqlConnection.Open();
                    var ret = command.ExecuteScalar();
                    if (ret != null)
                        result = Convert.ToString(ret);
                }
            }
            return result;
        }

        public List<TData> ExecuteProcedureReturnData<TData>(string procName,
            params SqlParameter[] parameters)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("WebApiDatabase")))
            {
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.CommandText = procName;
                    if (parameters != null)
                    {
                        sqlCommand.Parameters.AddRange(parameters);
                    }
                    sqlConnection.Open();
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        var elements = new List<TData>(); ;
                        try
                        {
                            elements = _mapper.Map<IDataReader, List<TData>>(reader);
                        }
                        finally
                        {
                            while (reader.NextResult())
                            { }
                        }
                        return elements;
                    }
                }
            }
        }
    }
}
