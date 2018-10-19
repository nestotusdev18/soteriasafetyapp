using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Repository
{
    public class DapperQueryPartsGenerator<TEntity> : IDapperQueryPartsGenerator<TEntity> where TEntity : class
    {
        private PropertyInfo[] PropertyInfos;
        private string[] PropertiesNames;
        private string TypeName;
        private string TableName;
        private string CharacterParameter;
        public DapperQueryPartsGenerator(char characterParameter = '@')
        {
            var type = typeof(TEntity);
            this.CharacterParameter = characterParameter.ToString();
            PropertyInfos = type.GetProperties();
            PropertiesNames = PropertyInfos.Where(a => !IsComplexType(a)).Select(a => a.Name).ToArray();
            TypeName = type.Name;

            var customAttributes = typeof(TEntity).GetTypeInfo().GetCustomAttributes<Dapper.Contrib.Extensions.TableAttribute>();
            if (customAttributes.Count() > 0)
                TableName = customAttributes.First().Name;
        }
        public string GeneratePartInsert(string identityField = null)
        {
            var result = string.Empty;
            var stringBuilder = new StringBuilder($"INSERT INTO {TableName} (");
            var propertiesNamesDef = PropertiesNames.Where(a => a != identityField).ToArray();
            string camps = string.Join(",", propertiesNamesDef);
            stringBuilder.Append($"{camps}) VALUES (");
            string[] parametersCampsCol = propertiesNamesDef.Select(a => $"{CharacterParameter}{a}").ToArray();
            string campsParameter = string.Join(",", parametersCampsCol);
            stringBuilder.Append($"{campsParameter})");
            result = stringBuilder.ToString();
            return result;
        }
        public string GenerateSelect()
        {
            var result = string.Empty;
            var stringBuilder = new StringBuilder("SELECT ");
            string separator = $",{Environment.NewLine}";
            string selectPart = string.Join(separator, PropertiesNames);
            stringBuilder.AppendLine(selectPart);
            string fromPart = $"FROM {TableName}";
            stringBuilder.Append(fromPart);
            result = stringBuilder.ToString();
            return result;
        }
        public string GenerateDelete(object parameters)
        {
            ParameterValidator.ValidateObject(parameters, nameof(parameters));
            var where = GenerateWhere(parameters);
            var result = $"DELETE FROM {TableName} {where} ";
            return result;
        }
        public string GenerateUpdate(object pks)
        {
            ParameterValidator.ValidateObject(pks, nameof(pks));
            var pksFields = pks.GetType().GetProperties().Select(a => a.Name).ToArray();
            var stringBuilder = new StringBuilder($"UPDATE {TableName} SET ");
            var propertiesNamesDef = PropertiesNames.Where(a => !pksFields.Contains(a)).ToArray();
            var propertiesSet = propertiesNamesDef.Select(a => $"{a} = {CharacterParameter}{a}").ToArray();
            var strSet = string.Join(",", propertiesSet);
            var where = GenerateWhere(pks);
            stringBuilder.Append($" {strSet} {where} ");
            var result = stringBuilder.ToString();
            return result;
        }
        public string GenerateSelect(object fieldsFilter)
        {
            ParameterValidator.ValidateObject(fieldsFilter, nameof(fieldsFilter));
            var initialSelect = GenerateSelect();
            var where = GenerateWhere(fieldsFilter);
            var result = $" {initialSelect} {where}";
            return result;
        }
        private string GenerateWhere(object filtersPKs)
        {
            ParameterValidator.ValidateObject(filtersPKs, nameof(filtersPKs));
            var filtersPksFields = filtersPKs.GetType().GetProperties().Select(a => a.Name).ToArray();
            if (!filtersPksFields?.Any() ?? true) throw new ArgumentException($"El parameter filtersPks isn't valid. This parameter must be a class type", nameof(filtersPKs));
            var propertiesWhere = filtersPksFields.Select(a => $"{a} = {CharacterParameter}{a}").ToArray();
            var strWhere = string.Join(" AND ", propertiesWhere);
            var result = $" WHERE {strWhere} ";
            return result;
        }
        private bool IsComplexType(PropertyInfo propertyInfo)
        {
            bool result;
            result = (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String") || propertyInfo.PropertyType.IsInterface;
            return result;
        }
    }
}
