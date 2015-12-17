/****************************** Module Header ******************************\
Module NamSin4U.FormsAppcs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;

namespace Test.NewSolution.Helpers
{
    /// <summary>
    /// Maps an expression like (m => m.SomeProperty) to the property name "SomeProperty"
    /// </summary>
    public static class PropertyNameHelper
    {
        /// <summary>
        /// Returns property name from expression to get typed lookups
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="property"></param>       
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            return Parse(property).Print();         
        }

        /// <summary>
        /// Parse the specified propertyPath.
        /// </summary>
        /// <param name="propertyPath">Property path.</param>
        public static ParsedExpression Parse(LambdaExpression propertyPath)
        {
            var toReturn = new ParsedExpression();

            var current = propertyPath.Body;
            while (current != null
                && current.NodeType != ExpressionType.Parameter)
            {
                current = ParseTo(current, toReturn);
            }

            return toReturn;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <returns>The property name.</returns>
        /// <param name="property">Property.</param>
        /// <typeparam name="TModel">The 1st type parameter.</typeparam>
        public static string GetPropertyName<TModel>(Expression<Func<object>> property)
        {
            var propertyName = string.Empty;

            if (property.Body.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = property.Body as MemberExpression;
                if (memberExpression != null)
                    propertyName = memberExpression.Member.Name;
            }
            else
            {
                var unary = property.Body as UnaryExpression;
                if (unary != null)
                {
                    var member = unary.Operand as MemberExpression;
                    if (member != null) propertyName = member.Member.Name;
                }
            }

            return propertyName;
        }

        /// <summary>
        /// Parses to.
        /// </summary>
        /// <returns>The to.</returns>
        /// <param name="current">Current.</param>
        /// <param name="toReturn">To return.</param>
        private static Expression ParseTo(Expression current, ParsedExpression toReturn)
        {
            // This happens when a value type gets boxed
            if (current.NodeType == ExpressionType.Convert || current.NodeType == ExpressionType.ConvertChecked)
            {
                return Unbox(current);
            }

            if (current.NodeType == ExpressionType.MemberAccess)
            {
                return ParseProperty(current, toReturn);
            }

            if (current is MethodCallExpression)
            {
                return ParseMethodCall(current, toReturn);
            }

            throw new ArgumentException(
                "Property expression must be of the form 'x => x.SomeProperty.SomeOtherProperty'");
        }

        /// <summary>
        /// Parses the method call.
        /// </summary>
        /// <returns>The method call.</returns>
        /// <param name="current">Current.</param>
        /// <param name="toReturn">To return.</param>
        private static Expression ParseMethodCall(Expression current, ParsedExpression toReturn)
        {
            var me = (MethodCallExpression) current;
            if (me.Method.Name != "get_Item"
                || me.Arguments.Count != 1)
            {
                throw new ArgumentException(
                    "Property expression must be of the form 'x => x.SomeProperty.SomeOtherProperty' or 'x => x.SomeCollection[0].Property'");
            }
            var argument = me.Arguments[0];
            argument = ConvertMemberAccessToConstant(argument);
            toReturn.PrependIndexed(argument.ToString());
            current = me.Object;
            return current;
        }

        /// <summary>
        /// Converts the member access to constant.
        /// </summary>
        /// <returns>The member access to constant.</returns>
        /// <param name="argument">Argument.</param>
        private static Expression ConvertMemberAccessToConstant(Expression argument)
        {
            if (argument.NodeType != ExpressionType.MemberAccess) return argument;

            try
            {
                var boxed = Expression.Convert(argument, typeof (object));
                var constant = Expression.Lambda<Func<object>>(boxed)
                    .Compile()
                    ();
                var constExpr = Expression.Constant(constant);

                return constExpr;
            }
            catch
            {
            }

            return argument;
        }

        /// <summary>
        /// Parses the property.
        /// </summary>
        /// <returns>The property.</returns>
        /// <param name="current">Current.</param>
        /// <param name="toReturn">To return.</param>
        private static Expression ParseProperty(Expression current, ParsedExpression toReturn)
        {
            var me = (MemberExpression) current;
            toReturn.PrependProperty(me.Member.Name);
            current = me.Expression;
            return current;
        }

        /// <summary>
        /// Unbox the specified current.
        /// </summary>
        /// <param name="current">Current.</param>
        private static Expression Unbox(Expression current)
        {
            var ue = (UnaryExpression) current;
            current = ue.Operand;
            return current;
        }   
    }

    /// <summary>
    /// Parsed expression.
    /// </summary>
    public class ParsedExpression
    {
        public interface INode
        {
            void AppendPrintTo(StringBuilder builder);
        }

        public class PropertyNode : INode
        {
            public PropertyNode(string propertyName)
            {
                PropertyName = propertyName;
            }

            public string PropertyName { get; private set; }

            public void AppendPrintTo(StringBuilder builder)
            {
                if (builder.Length > 0)
                    builder.Append(".");

                builder.Append(PropertyName);
            }
        }

        public class IndexedNode : INode
        {
            public IndexedNode(string indexValue)
            {
                IndexValue = indexValue;
            }

            public string IndexValue { get; private set; }

            public void AppendPrintTo(StringBuilder builder)
            {
                builder.AppendFormat("[{0}]", IndexValue);
            }
        }

        private readonly LinkedList<INode> _nodes = new LinkedList<INode>();

        protected LinkedList<INode> Nodes
        {
            get { return _nodes; }
        }

        protected void Prepend(INode node)
        {
            _nodes.AddFirst(node);
        }

        public void PrependProperty(string propertyName)
        {
            Prepend(new PropertyNode(propertyName));
        }

        public void PrependIndexed(string indexedValue)
        {
            Prepend(new IndexedNode(indexedValue));
        }

        public string Print()
        {
            var output = new StringBuilder();
            foreach (var node in Nodes)
            {
                node.AppendPrintTo(output);
            }
            return output.ToString();
        }
    }
}

