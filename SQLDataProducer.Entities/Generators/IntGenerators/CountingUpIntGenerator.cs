// Copyright 2012-2014 Peter Henell

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

namespace SQLDataProducer.Entities.Generators.IntGenerators
{
    public class CountingUpIntGenerator : IntegerGeneratorBase
    {
        public static readonly string GENERATOR_NAME = "Counting Up";

        public CountingUpIntGenerator(ColumnDataTypeDefinition datatype)
            : base(GENERATOR_NAME, datatype)
        {
            GeneratorParameters.StartValue = new GeneratorParameter("StartValue", 1,
                GeneratorParameterParser.IntegerParser);
            GeneratorParameters.Step = new GeneratorParameter("Step", 1,
                GeneratorParameterParser.IntegerParser);
            GeneratorParameters.MaxValue = new GeneratorParameter("MaxValue", datatype.MaxValue, GeneratorParameterParser.LonglParser, false);
            GeneratorParameters.MinValue = new GeneratorParameter("MinValue", datatype.MinValue, GeneratorParameterParser.LonglParser, false);
        }

        protected override object InternalGenerateValue(long n)
        {
            int step = GeneratorParameters.Step.Value;
            int startValue = GeneratorParameters.StartValue.Value;
            long maxValue = GeneratorParameters.MaxValue.Value;

            return startValue + ((step * (n - 1)) % maxValue);
        }
    }
}
