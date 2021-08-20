using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace KONTAKTOR.DA.Infrastructure
{
    public static class DapperCustomExtensions
    {
        private static PostgresAdapter DefaultAdapter = new PostgresAdapter();

        private static ISqlAdapter GetFormatter(IDbConnection connection)
        {
            return DefaultAdapter;
        }

        public static Task<TKey> InsertSingleAsync<T, TKey>(this IDbConnection connection, T entityToInsert, Expression<Func<T, TKey>> keyFunc, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null) where T : class
        {
            Type type = typeof(T);
            sqlAdapter ??= GetFormatter(connection);

            IfEnumerableThrowNotSupported<T>(type);

            string tableName = GetTableName(type);
            StringBuilder sb = new StringBuilder((string)null);
            List<PropertyInfo> properties = TypePropertiesCache(type);
            List<PropertyInfo> keyProps = KeyPropertiesCache(type).ToList();
            List<PropertyInfo> computedProps = ComputedPropertiesCache(type);
            List<PropertyInfo> propsToInsert = properties.Except(keyProps.Union(computedProps)).ToList();
            for (int index = 0; index < propsToInsert.Count; ++index)
            {
                PropertyInfo propertyInfo = propsToInsert[index];
                sqlAdapter.AppendColumnName(sb, propertyInfo.Name);
                if (index < propsToInsert.Count - 1)
                    sb.Append(", ");
            }
            var propsParamsNames = CreatePropToInsertList(propsToInsert);

            var propKey = GetPropertyInfo<T, TKey>(entityToInsert, keyFunc);
            if (keyProps.Single().Name != propKey.Name)
            {
                throw new Exception($"{propKey.Name} не явялется единственным ключом для класса {type.FullName}");
            }

            string sql = $@" INSERT INTO {tableName} ({sb}) "
                         + $" VALUES ({propsParamsNames}) "
                         + $" RETURNING \"{( keyProps.Single().Name) }\" ";

            return connection.QuerySingleAsync<TKey>(sql, entityToInsert, transaction, commandTimeout);
        }

        private static PropertyInfo GetPropertyInfo<TSource, TProperty>(
            TSource source,
            Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }


        private static StringBuilder CreatePropToInsertList(List<PropertyInfo> propsToInsert)
        {
            StringBuilder propsParamsNames = new StringBuilder((string)null);
            for (int index = 0; index < propsToInsert.Count; ++index)
            {
                PropertyInfo propertyInfo = propsToInsert[index];
                propsParamsNames.AppendFormat("@{0}", (object)propertyInfo.Name);
                if (index < propsToInsert.Count - 1)
                    propsParamsNames.Append(", ");
            }

            return propsParamsNames;
        }



        public static Task<T> InsertSingleSelfAsync<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null) where T : class
        {
            Type type = typeof(T);
            sqlAdapter = sqlAdapter ?? GetFormatter(connection);
            IfEnumerableThrowNotSupported<T>(type);
            string tableName = GetTableName(type);
            List<PropertyInfo> propsToInsert = PropsToInsert(type);
            StringBuilder sb = TableColumns(sqlAdapter, propsToInsert);
            var propsParamsNames = CreatePropToInsertList(propsToInsert);

            string sql = $@" INSERT INTO {tableName} ({sb}) "
                         + $" VALUES ({propsParamsNames}) "
                         + " RETURNING *";

            return connection.QuerySingleAsync<T>(sql, entityToInsert, transaction, commandTimeout);
        }

        static StringBuilder TableColumns(ISqlAdapter sqlAdapter, List<PropertyInfo> propsToInsert)
        {
            StringBuilder sb = new StringBuilder(null);

            for (int index = 0; index < propsToInsert.Count; ++index)
            {
                PropertyInfo propertyInfo = propsToInsert[index];
                sqlAdapter.AppendColumnName(sb, propertyInfo.Name);

                if (index < propsToInsert.Count - 1)
                    sb.Append(", ");
            }

            return sb;
        }

        static List<PropertyInfo> PropsToInsert(Type type)
        {
            List<PropertyInfo> properties = TypePropertiesCache(type);
            List<PropertyInfo> keyProps = KeyPropertiesCache(type).ToList();
            List<PropertyInfo> computedProps = ComputedPropertiesCache(type);

            return properties.Except(keyProps.Union(computedProps)).ToList();
        }

        public static Task InsertListSelfAsync<T>(this IDbConnection connection, IEnumerable<T> entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null) where T : class
        {
            Type type = typeof(T);
            sqlAdapter = sqlAdapter ?? GetFormatter(connection);
            IfEnumerableThrowNotSupported<T>(type);
            string tableName = GetTableName(type);
            List<PropertyInfo> propsToInsert = PropsToInsert(type);
            StringBuilder sb = TableColumns(sqlAdapter, propsToInsert);
            var propsParamsNames = CreatePropToInsertList(propsToInsert);

            string sql = $@"INSERT INTO {tableName} ({sb}) 
                            VALUES ({propsParamsNames})";

            return connection.ExecuteAsync(sql, entityToInsert, transaction, commandTimeout);
        }

        private static void IfEnumerableThrowNotSupported<T>(Type type) where T : class
        {
            if (type.IsArray)
            {
                throw new NotSupportedException("Метод не поддерживает вставку коллекций");
            }

            if (type.IsGenericType)
            {
                TypeInfo typeInfo = type.GetTypeInfo();
                IEnumerable<Type> implementedInterfaces = typeInfo.ImplementedInterfaces;

                if (implementedInterfaces.Any<Type>(ti =>
                {
                    if (ti.IsGenericType)
                        return ti.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                    return false;
                }) || typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    throw new NotSupportedException("Метод не поддерживает вставку коллекций");
                }
            }
        }


        private static string GetTableName(Type type)
        {
            var attr = type.GetCustomAttribute<TableAttribute>();
            return attr?.Name ?? (type.Name + "s");
        }

        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> KeyProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> ComputedProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();

        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out var source))
                return source.ToList();
            PropertyInfo[] array = (type.GetProperties()).Where(IsWriteable).ToArray();
            TypeProperties[type.TypeHandle] = array;
            return array.ToList();
        }

        private static bool IsWriteable(PropertyInfo pi)
        {
            List<object> objectList = ((IEnumerable<object>)pi.GetCustomAttributes(typeof(WriteAttribute), false)).AsList<object>();
            if (objectList.Count != 1)
                return true;
            return ((WriteAttribute)objectList[0]).Write;
        }

        private static List<PropertyInfo> KeyPropertiesCache(Type type)
        {
            if (KeyProperties.TryGetValue(type.TypeHandle, out var cached))
                return cached.ToList<PropertyInfo>();
            List<PropertyInfo> found = TypePropertiesCache(type);
            List<PropertyInfo> result = found.Where((p => (p.GetCustomAttributes(true)).Any(a => a is KeyAttribute))).ToList();
            if (result.Count == 0)
            {
                PropertyInfo propertyInfo = found.Find(p => string.Equals(p.Name, "id", StringComparison.CurrentCultureIgnoreCase));
                if (propertyInfo != null)
                {
                    object[] customAttributes = propertyInfo.GetCustomAttributes(true);
                    if (!customAttributes.Any(a => a is ExplicitKeyAttribute))
                        result.Add(propertyInfo);
                }
            }
            KeyProperties[type.TypeHandle] = result;
            return result;
        }

        private static List<PropertyInfo> ComputedPropertiesCache(Type type)
        {
            IEnumerable<PropertyInfo> source;
            if (ComputedProperties.TryGetValue(type.TypeHandle, out source))
                return source.ToList();
            List<PropertyInfo> list = TypePropertiesCache(type).Where(p => p.GetCustomAttributes(true).Any(a => a is ComputedAttribute)).ToList();
            ComputedProperties[type.TypeHandle] = list;
            return list;
        }
    }
}
