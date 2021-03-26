using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BifrostSenderCtree.Domain.Interfaces.Infrastructure;
using BifrostSenderCtree.Domain.Interfaces.Models;
using BifrostSenderCtree.Domain.Interfaces.Repositories;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BifrostSenderCtree.Infra.Repositories
{
    public class GenericRepository<T> : IBaseRepository<T> where T : IBaseModel, new()
    {
        private IDatabase Database { get; }
        
        private string TableName { get; }

        private string PrimaryKeys { get; set; }
        
        public GenericRepository(IDatabase database, string tableName, IList<string> primaryKeys)
        {
            Database = database;
            TableName = tableName;
            PrimaryKeys = string.Join("+", primaryKeys);
        }
        
        public GenericRepository(IDatabase database, string tableName, string primaryKey)
        {
            Database = database;
            TableName = tableName;
            PrimaryKeys = primaryKey;
        }

        private static T DataReaderToModel<T>(IDataReader dr)
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                var jsonProperty = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (jsonProperty is null)
                {
                    throw new Exception($"{prop.Name} doesn't contain JsonProperty to bind");
                }
                
                var currentValue = dr[jsonProperty.Name];
                
                if (!object.Equals(currentValue, DBNull.Value))
                {
                    prop.SetValue(obj, currentValue, null);
                }
            }

            return obj;
        }

        public IEnumerable<T> GetCursor()
        {
            Database.Command.CommandText = $"SELECT * FROM {TableName}";
            using (IDataReader reader = Database.Command.ExecuteReader())
            {
                while (reader.Read())
                    yield return DataReaderToModel<T>(reader);
            }
        }

        public Task<IEnumerable<T>> Find(string filters)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(string id)
        {
            Database.Command.CommandText = $"SELECT * FROM {TableName} WHERE {PrimaryKeys}=\"{id}\"";
            using (IDataReader reader = Database.Command.ExecuteReader())
            {
                while (reader.Read())
                    return Task.FromResult(DataReaderToModel<T>(reader));
            }

            return Task.FromResult(Activator.CreateInstance<T>());
        }

        public Task<bool> DeleteById(string id)
        {
            Database.Command.CommandText = $"DELETE FROM {TableName} WHERE {PrimaryKeys}={id}";
            return Task.FromResult(Database.Command.ExecuteNonQuery() > 0);
        }
        
    }
}