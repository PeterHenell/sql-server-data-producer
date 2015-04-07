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
    public class RandomExponentialIntGenerator : IntegerGeneratorBase
    {
        public static readonly string GENERATOR_NAME = "Exponential Random Numbers";

        public RandomExponentialIntGenerator(ColumnDataTypeDefinition dataType)
            : base(GENERATOR_NAME, dataType)
        {
            GeneratorParameters.StartValue = new GeneratorParameter("StartValue", 1,
               GeneratorParameterParser.IntegerParser);
            GeneratorParameters.Step = new GeneratorParameter("Step", 1,
                GeneratorParameterParser.IntegerParser);
            GeneratorParameters.MaxValue = new GeneratorParameter("MaxValue", dataType.MaxValue, GeneratorParameterParser.IntegerParser, false);
            GeneratorParameters.MinValue = new GeneratorParameter("MinValue", dataType.MinValue, GeneratorParameterParser.IntegerParser, false);
        }

        protected override object InternalGenerateValue(long n)
        {
            //    /// http://www.sqlservercentral.com/articles/SQL+Uniform+Random+Numbers/91103/
            ///// </summary>
            ///// <returns></returns>
            //[GeneratorMetaData(Generators.GeneratorMetaDataAttribute.GeneratorType.Integer)]
            //public static Generator CreateExponentialRandomNumbersGenerator()
            //{
            //    GeneratorParameterCollection paramss = new GeneratorParameterCollection();

            //    paramss.Add(new GeneratorParameter("Lambda", new decimal(1.1), GeneratorParameterParser.DecimalParser));

            //    Generator gen = new Generator(GENERATOR_ExponentialRandomNumbers, (n, p) =>
            //    {
            //        double Lambda = (double)p.GetValueOf<decimal>("Lambda");
            //        double URN1 = RandomSupplier.Instance.GetNextDouble();

            //        //-LOG(@URN)/@Lambda
            //        return -1.0 * (Math.Log(URN1) / Lambda);
            //    }
            //        , paramss);
            //    return gen;
            //}
            return n;
        }
    }
}
