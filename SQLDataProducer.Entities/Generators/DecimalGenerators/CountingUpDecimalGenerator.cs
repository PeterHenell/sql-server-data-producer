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
    public class CountingUpDecimalGenerator : DecimalGeneratorBase
    {
        public static readonly string GENERATOR_NAME = "Counting up Decimal";

        public CountingUpDecimalGenerator(ColumnDataTypeDefinition datatype)
            : base(GENERATOR_NAME, datatype)
        {
            GeneratorParameters.StartValue = new GeneratorParameter("StartValue", 0.0m,
                GeneratorParameterParser.DecimalParser);
            GeneratorParameters.Step = new GeneratorParameter("Step", 1.0m,
                GeneratorParameterParser.DecimalParser);
        }

        protected override object InternalGenerateValue(long n)
        {
            decimal step = GeneratorParameters.Step.Value;
            decimal startValue = GeneratorParameters.StartValue.Value;

            return startValue + (step * (n - 1));
        }
    }
}
