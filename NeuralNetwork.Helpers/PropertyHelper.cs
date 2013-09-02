using System;
using System.Linq.Expressions;
using System.Reflection;

namespace NeuralNetwork.Helpers
{
    public static class PropertyHelper
    {
        public static string GetName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var lambda = propertyExpression as LambdaExpression;
            MemberExpression memberExpression;
            var body = lambda.Body as UnaryExpression;
            if (body != null)
            {
                var unaryExpression = body;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            if (memberExpression != null)
            {
                var propertyInfo = (PropertyInfo)memberExpression.Member;
                return propertyInfo.Name;
            }

            return string.Empty;
        }
    }
}
