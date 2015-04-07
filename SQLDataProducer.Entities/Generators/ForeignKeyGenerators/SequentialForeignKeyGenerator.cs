using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace SQLDataProducer.Entities.Generators.ForeignKeyGenerators
{
    /// <summary>
    /// 
    /// </summary>
    public class SequentialForeignKeyGenerator : ForeignKeyGeneratorBase
    {
        public static readonly string GENERATOR_NAME = "SequentialForeignKeyGenerator";

        public SequentialForeignKeyGenerator(List<string> foreignKeys)
            : base(GENERATOR_NAME, foreignKeys)
        {

        }

        protected override object InternalGenerateValue(long n)
        {
            var foreignKeys = GeneratorParameters.ForeignKeys.Value;
            int index = (int)((n % foreignKeys.Count) % int.MaxValue);
            return foreignKeys[index];
        }     
    }
}
