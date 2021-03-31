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

        private IDictionary<string, IList<int>> PrimaryKeys { get; set; }

        private string PrimaryKey { get; set; }

        private int Limit = 100;

        public GenericRepository(IDatabase database, string tableName, IDictionary<string, IList<int>> primaryKeys)
        {
            Database = database;
            TableName = tableName;
            PrimaryKeys = primaryKeys;
        }

        public GenericRepository(IDatabase database, string tableName, string primaryKey)
        {
            Database = database;
            TableName = tableName;
            PrimaryKey = primaryKey;
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

        private static string GetFieldModelType<T>(string field)
        {
            T obj = default(T);
            obj = Activator.CreateInstance<T>();
            var properties = obj.GetType().GetProperties().First(prop =>
            {
                var jsonProperty = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                if (jsonProperty is null)
                {
                    throw new Exception($"{prop.Name} doesn't contain JsonProperty to read");
                }

                return jsonProperty.Name == field;
            });

            return properties.PropertyType.Name;
        }

        public IEnumerable<T> GetBatch()
        {
            Database.Command.CommandText = $"SELECT TOP {Limit} * FROM {TableName} WHERE chaveprimaria LIKE '%BRASIL%'";
            IList<T> result = new List<T>();


            using (IDataReader reader = Database.Command.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(DataReaderToModel<T>(reader));
                }
            }
            Database.Command.ExecuteNonQuery();

            return result;
        }

        public Task<IEnumerable<T>> Find(string filters)
        {
            throw new NotImplementedException();
        }

        public Task<T> FindById(string id)
        {
            if (id.Length < 50)
            {
                for (var i = 0; i <= (50 - id.Length); i++) id += " ";
            }

            string whereClause = !string.IsNullOrEmpty(PrimaryKey) ? $"{PrimaryKey}='{id}" : IdToFilter(id);

            Database.Command.CommandText = $"SELECT * FROM {TableName} WHERE {whereClause}";
            using (IDataReader reader = Database.Command.ExecuteReader())
            {
                while (reader.Read())
                    return Task.FromResult(DataReaderToModel<T>(reader));
            }

            T obj = default(T);
            obj = Activator.CreateInstance<T>();

            return Task.FromResult(obj);
        }

        public virtual Task<bool> DeleteById(string id)
        {
            string whereClause = !string.IsNullOrEmpty(PrimaryKey) ? $"{PrimaryKey}='{id}'" : IdToFilter(id);

            Database.Command.CommandText = $"DELETE FROM {TableName} WHERE {whereClause}";

            var result = Database.Command.ExecuteNonQuery();
            return Task.FromResult(result > 0);
        }

        private string IdToFilter(string id)
        {
            string finalFilter = "";
            foreach (var item in PrimaryKeys)
            {
                if (string.IsNullOrEmpty(item.Key)) continue;

                string substr = id.Substring(item.Value[0], item.Value[1]);
                string fieldType = GetFieldModelType<T>(item.Key).ToLower();

                if (fieldType is "string" || fieldType is "datetime")
                {
                    substr = $"='{substr}'";
                }
                else
                {
                    substr = $"={substr}";
                }

                if (string.IsNullOrEmpty(finalFilter))
                {
                    finalFilter += $"{item.Key}{substr}";
                }
                else
                {
                    finalFilter += $" AND {item.Key}{substr}";
                }
            }

            return finalFilter;
        }

    }
}