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
    public class RandomNumberDecimalGenerator : DecimalGeneratorBase
    {
        public static readonly string GENERATOR_NAME = "Random Decimal";

        public RandomNumberDecimalGenerator(ColumnDataTypeDefinition datatype)
            : base(GENERATOR_NAME, datatype)
        {
            GeneratorParameters.MinValue = new GeneratorParameter("Min Value", 1m,
                GeneratorParameterParser.DecimalParser);
            GeneratorParameters.MaxValue = new GeneratorParameter("Max Value", datatype.MaxValue,
                GeneratorParameterParser.DecimalParser);
        }

        protected override object InternalGenerateValue(long n)
        {
            var min = (decimal)GeneratorParameters.MinValue.Value;
            var max = (decimal)GeneratorParameters.MaxValue.Value;

            return RandomSupplier.Instance.GetNextDecimal(min, max);
        }
    }
}
