using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public abstract class Instruction
    {

        public abstract ushort Translate(List<TranslationTable> tables);
    }
}
