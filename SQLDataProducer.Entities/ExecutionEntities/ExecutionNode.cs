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


using SQLDataProducer.Entities.DatabaseEntities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SQLDataProducer.Entities.ExecutionEntities
{
    public class ExecutionNode : EntityBase, IEquatable<ExecutionNode>
    {
        private readonly ExecutionNode _parent;
        public ExecutionNode Parent
        {
            get
            {
                return _parent;
            }
        }

        ObservableCollection<ExecutionNode> _children;
        public IEnumerable<ExecutionNode> Children
        {
            get
            {
                return _children;
            }
        }

        private readonly int _nodeId;
        public int NodeId
        {
            get
            {
                return _nodeId;
            }
        }

        int _level;
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged("Level");
                }
            }
        }

        private string _nodeName;
        public string NodeName
        {
            get
            {
                return _nodeName;
            }
            set
            {
                if (_nodeName != value)
                {
                    _nodeName = value;
                    OnPropertyChanged("NodeName");
                }
            }
        }

        bool _hasChildren;
        public bool HasChildren
        {
            get
            {
                return _hasChildren;
            }
            private set
            {
                if (_hasChildren != value)
                {
                    _hasChildren = value;
                    OnPropertyChanged("HasChildren");
                }
            }
        }

        ObservableCollection<TableEntity> _tables;
        public IEnumerable<TableEntity> Tables
        {
            get
            {
                return _tables;
            }
            //private set
            //{
            //    if (_tables != value)
            //    {
            //        _tables = value;
            //        OnPropertyChanged("Tables");
            //    }
            //}
        }

        private static int nodeCounter = 0;

        public ExecutionNode AddChild(int repeatCount, string nodeName = "")
        {
            var n = new ExecutionNode(this.Level + 1, this, repeatCount, nodeName);
            this.HasChildren = true;
            _children.Add(n);
            return n;
        }

        public static ExecutionNode CreateLevelOneNode(int repeatCount, string nodeName = "")
        {
            return new ExecutionNode(1, null, repeatCount, nodeName);
        }

        private int _repeatCount  = 1;
        public int RepeatCount
        {
            get
            {
                return _repeatCount;
            }
            private set
            {
                if (_repeatCount != value)
                {
                    _repeatCount = value;
                    OnPropertyChanged("RepeatCount");
                }
            }
        }

        private ExecutionNode(int level, ExecutionNode parent, int repeatCount, string nodeName)
        {
            _parent = parent;
            _nodeId = nodeCounter++;
            _tables = new ObservableCollection<TableEntity>();
            _children = new ObservableCollection<ExecutionNode>();
            RepeatCount = repeatCount;
            NodeName = nodeName;

            Level = level;
            HasChildren = false;
        }

        public override bool Equals(System.Object obj)
        {
            ExecutionNode p = obj as ExecutionNode;
            if ((object)p == null)
                return false;

            if (Object.ReferenceEquals(this, p))
                return true;

            return this.Equals(p);
        }

        public bool Equals(ExecutionNode b)
        {
            // Return true if the fields match:
            return
                this.NodeId.Equals(b.NodeId);
        }


        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 37;
                hash = hash * 23 + this.NodeId.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("[ExecutionNode NodeId: {0}, Level: {1}, RepeatCount: {2}]"
                , this.NodeId, this.Level, this.RepeatCount);
        }

        public ExecutionNode AddTable(TableEntity tableToAdd)
        {
            if (!Tables.Contains(tableToAdd))
                _tables.Add(tableToAdd);
            return this;
        }

        public bool HasWarning { get; private set; }

        public bool RemoveTable(TableEntity tableToRemove)
        {
            return _tables.Remove(tableToRemove);
        }

        public void MoveTableToParentNode(TableEntity tableToMove)
        {
            if (_tables.Remove(tableToMove))
                Parent.AddTable(tableToMove);
            else
                throw new KeyNotFoundException("unable to move table since it did not exist in the node");
        }

        public void MoveTableToNode(TableEntity tableToMove, ExecutionNode targetNode)
        {
            if (RemoveTable(tableToMove))
                targetNode.AddTable(tableToMove);
            else
                throw new KeyNotFoundException("unable to move table since it did not exist in the node");
        }

        public void MoveTableToNewChildNode(TableEntity tableToMove, int repeatCount, string nodeName = "")
        {
            if (RemoveTable(tableToMove))
            {
                var child = AddChild(repeatCount, nodeName);
                child.AddTable(tableToMove);
            }
            else
                throw new KeyNotFoundException("unable to move table since it did not exist in the node");
        }

        public void RemoveTables(params TableEntity[] tablesToRemove)
        {
            ValidateUtil.ValidateNotNull(tablesToRemove, "tablesToRemove");
            foreach (var tableToRemove in tablesToRemove)
            {
                RemoveTable(tableToRemove);
            }
        }
    }
    
}
