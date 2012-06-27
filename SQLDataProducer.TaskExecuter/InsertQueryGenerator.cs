﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLDataProducer.Entities;
using System.Data.SqlClient;
using SQLDataProducer.DatabaseEntities.Entities;
using SQLDataProducer.Entities.ExecutionEntities;
using SQLDataProducer.Entities.Generators;

namespace SQLDataProducer.TaskExecuter
{
    public class InsertQueryGenerator
    {
        public string GenerateQueryForExecutionItems(ExecutionItemCollection executionItems)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SET NOCOUNT ON");
            sb.AppendLine("SET XACT_ABORT ON");
            sb.AppendLine();
            sb.AppendLine("BEGIN TRANSACTION");
            sb.AppendLine();

            sb.AppendLine("DECLARE @N bigint = <N>");
            sb.AppendLine();


            foreach (var item in executionItems)
            {
                // Skip tables with no columns
                if (item.TargetTable.Columns.Count == 0)
                {
                    continue;
                }

                bool hasIdentity = item.TargetTable.Columns.Any(col => col.IsIdentity);

                sb.AppendFormat("-- INSERT item {0} - {0}", item.Order, item.Description);

                if (item.ExecutionCondition != ExecutionConditions.None)
                {
                    sb.AppendLine();
                    sb.AppendFormat("IF @N {0} BEGIN", item.ExecutionCondition.ToCompareString(item.ExecutionConditionValue));
                }
                
                // Generate for each column
                sb.Append(GenerateInsertStatement(item));

                if (item.ExecutionCondition != ExecutionConditions.None)
                {
                    sb.AppendLine("END");
                    sb.AppendLine();
                }
                
                if (hasIdentity)
                {
                    sb.AppendLine();
                    sb.AppendFormat("DECLARE @i{0}_IDENTITY BIGINT = SCOPE_IDENTITY()", item.Order);
                }
                
                sb.AppendLine();
                sb.AppendFormat("-- DONE insert item {0}", item.Order);
                sb.AppendLine();
                sb.AppendLine();
                
                
            }
            sb.AppendLine("COMMIT");
            sb.AppendLine();

            return sb.ToString();
        }

        private string GenerateInsertStatement(ExecutionItem item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            
            sb.AppendFormat("INSERT {0}.{1} (", item.TargetTable.TableSchema, item.TargetTable.TableName);
            sb.AppendLine();
            foreach (var col in item.TargetTable.Columns.Where(x => x.IsIdentity == false))
            {
                sb.AppendFormat("\t{0}", col.ColumnName);
                sb.Append(col.OrdinalPosition == item.TargetTable.Columns.Count ? string.Empty : ", ");
                sb.AppendLine();
            }

            sb.Append(")");
            sb.AppendLine();
            
            sb.AppendFormat("<{0}VALUES>", item.Order);

            return sb.ToString();
        }


        /// <summary>
        /// Generate the declaration section of the sqlquery, including the values for the variables
        /// </summary>
        /// <param name="n">The serial number to use when creating the values for the variables</param>
        /// <param name="execItems">the executionItems to be included in the variable declarations</param>
        /// <returns></returns>
        public string GenerateFinalQuery(string baseQuery, IEnumerable<ExecutionItem> execItems, int n , SetCounter m)
        {
            string modified = baseQuery.Clone() as string;

            modified = modified.Replace("<N>", n.ToString());

            // Skip tables with no columns
            foreach (var item in execItems.Where(x => x.TargetTable.Columns.Count > 0))
            {
                
                StringBuilder sb = new StringBuilder();
                
                sb.AppendFormat("\t-- Item {0}, {1}.{2}", item.Order, item.TargetTable.TableSchema, item.TargetTable.TableName);
                sb.AppendLine();
                sb.AppendLine("VALUES");
                for (int rep = 1; rep <= item.RepeatCount; rep++)
                {
                    int rowGenerationNumber = m.GetNext();
                    sb.Append("\t");
                    sb.Append("(");
                    foreach (ColumnEntity col in item.TargetTable.Columns.Where(x => x.IsIdentity == false))
                    {
                        sb.Append(col.Generator.GenerateValue(rowGenerationNumber));
                        sb.Append(col.OrdinalPosition == item.TargetTable.Columns.Count ? string.Empty : ", ");
                    }
                    sb.Append(")");
                    sb.Append(item.RepeatCount == rep ? string.Empty : ", ");
                    sb.AppendLine();
                }
                // Find the place in the big string where this set of values belong and replace the placeholder with the actual values.
                string placeholderIdentifier = string.Format("<{0}VALUES>", item.Order);
                modified = modified.Replace(placeholderIdentifier, sb.ToString());
            }

            return modified;
        }
    }
}