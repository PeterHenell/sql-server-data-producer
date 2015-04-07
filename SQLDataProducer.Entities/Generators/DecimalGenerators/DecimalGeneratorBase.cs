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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDataProducer.Entities.Generators.DecimalGenerators
{
    public abstract class DecimalGeneratorBase : AbstractValueGenerator
    {
        protected DecimalGeneratorBase(string generatorName, ColumnDataTypeDefinition dataType)
            : base(generatorName)
        {
            //GeneratorParameters.MinValue = new GeneratorParameter("MinValue", dataType.MinValue,
            //   GeneratorParameterParser.DecimalParser, false);
            //GeneratorParameters.MaxValue = new GeneratorParameter("MaxValue", dataType.MaxValue,
            //    GeneratorParameterParser.DecimalParser, false);
        }

        protected override object ApplyGeneratorTypeSpecificLimits(object value)
        {
            if (value is DBNull)
            {
                return value;
            }
            //decimal max = GeneratorParameters.MaxValue.Value;
            //decimal min = GeneratorParameters.MinValue.Value;

            //decimal newvalue = (decimal)value;
            // return the value if it is smaller than the max value and larger than the min value.
            return value;//Math.Max(Math.Min(newvalue, max), min);
        }
    }
}
