﻿// Copyright 2012-2014 Peter Henell

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using SQLDataProducer.Entities.DatabaseEntities.Collections;
using System.Xml.Serialization;
using SQLDataProducer.Entities;
using System.Linq;
using System.Text;
using SQLDataProducer.Entities.ExecutionEntities;
using System.Xml.Linq;
using System.Collections.Generic;


namespace SQLDataProducer.Entities.DatabaseEntities
{
    public class TableEntity : EntityBase
    {

        public TableEntity(string tableSchema, string tableName)
        {
            TableName = tableName;
            TableSchema = tableSchema;
            _columns = new ColumnEntityCollection();
        }

        bool _hasIdentityColumn = false;
        public bool HasIdentityColumn
        {
            get
            {
                return _hasIdentityColumn;
            }
            private set
            {
                if (_hasIdentityColumn != value)
                {
                    _hasIdentityColumn = value;
                    OnPropertyChanged("HasIdentityColumn");
                }
            }
        }


        ColumnEntityCollection _columns;
        public IEnumerable<ColumnEntity> Columns
        {
            get
            {
                return _columns;
            }
        }

        string _tableSchema;
        public string TableSchema
        {
            get
            {
                return _tableSchema;
            }
            private set
            {
                if (_tableSchema != value)
                {
                    _tableSchema = value;
                }
            }
        }

        string _tableName;
        public string TableName
        {
            get
            {
                return _tableName;
            }
            private set
            {
                if (_tableName != value)
                {
                    _tableName = value;
                }
            }
        }


        public override string ToString()
        {
            return FullName;// string.Format("TableName = '{0}', TableSchema = '{1}', Columns = '{2}', HasIdentityColumn = '{3}', HasWarning = '{4}', WarningText = '{5}'", this.TableName, this.TableSchema, this.Columns, this.HasIdentityColumn, this.HasWarning, this.WarningText);
        }

        public string FullName
        {
            get
            {
                return string.Format("{0}.{1}", TableSchema, TableName); 
            }
        }
       
        internal TableEntity Clone()
        {
            // TODO: Clone using the same entity as the LOAD/SAVE functionality
            var t = new TableEntity(this.TableSchema, this.TableName);
            t.HasIdentityColumn = this.HasIdentityColumn;
            t._columns = _columns.Clone();
            t.RefreshWarnings();
            return t;
        }

       
        private bool _hasWarning = false;
        /// <summary>
        /// This Item have some kind of warning that might cause problems during execution
        /// </summary>
        public bool HasWarning
        {
            get
            {
                return _hasWarning;
            }
            private set
            {
                _hasWarning = value;
                OnPropertyChanged("HasWarning");
            }
        }

        private string _warningText = string.Empty;
        /// <summary>
        /// Contains warning text if the this item have a warning that might cause problems during execution.
        /// </summary>
        public string WarningText
        {
            get
            {
                return _warningText;
            }
            private set
            {
                _warningText = value;
                OnPropertyChanged("WarningText");
            }
        }


        ///// <summary>
        /////  Execution item where this table is used.
        ///// </summary>
        //ExecutionNode _parentExecutionItem;
        //public ExecutionNode ParentExecutionItem
        //{
        //    get
        //    {
        //        return _parentExecutionItem;
        //    }
        //    set
        //    {
        //        if (_parentExecutionItem != value)
        //        {
        //            _parentExecutionItem = value;
        //        }
        //    }
        //}

        public void RefreshWarnings()
        {
            foreach (var col in Columns)
            {
                col.RefreshWarningStatus();
            }
            HasWarning = Columns.Any(col => col.HasWarning);
            WarningText = HasWarning ? "One of the columns have a warning" : string.Empty;
        }



        public TableEntity AddColumn(ColumnEntity columnEntity)
        {
            _columns.Add(columnEntity);
            if (columnEntity.IsIdentity)
                HasIdentityColumn = true;

            if (columnEntity.HasWarning)
                HasWarning = true;

            return this;
        }

        public void AddColumns(IEnumerable<ColumnEntity> columnsToAdd)
        {
            foreach (var column in columnsToAdd)
            {
                AddColumn(column);
            }
        }

        //public void ResetGeneratorCounters()
        //{
        //    foreach (var col in Columns)
        //    {
        //        col.Generator.ResetCounter();
        //    }
        //}
    }


}
