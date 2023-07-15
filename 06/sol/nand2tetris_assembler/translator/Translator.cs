using common;

namespace translator
{
    public class Translator
    {
        private TranslationTable jmpTable;
        private TranslationTable destTable;
        private CompTranslationTable compTable;
        private SymbolTable _symbolTable;

        public Translator(SymbolTable symbolTable)
        {
            jmpTable = new JmpTranslationTable();
            destTable = new DestTranslationTable();
            compTable = new CompTranslationTable();
            _symbolTable = symbolTable;
        }

        private ushort TranslateAInstruction(TokenizedInstruction a)
        {
            // this should get the @ushort as a string, then substring it from ushort part and then convert the 
            // the ushort string to a real ushort 
            string value = a.GetExpressions()[0].GetExpressionAsString().Substring(1);
            bool valid = ushort.TryParse(value, out ushort result);

            // if the value can't be parsed as a ushort we should try to hit the symbol table
            if (!valid)
            {
                (ushort symbolTableValue,valid) = _symbolTable.Get(new string(value.Where(e=>e != '(' && e != ')').ToArray()));
                if(!valid) 
                    throw new TranslationException($"invalid ${a.GetExpressions()[0].Type} expression: {value}");
                
                result = symbolTableValue;
            } 

            return result;
        }

        private ushort TranslateCInstructon(TokenizedInstruction c)
        {
            ushort instruction = 0b1110000000000000;
            Expression[] expressions = c.GetExpressions();

            foreach(var exp in expressions)
            {

                bool valid = false;
                ushort value = 0;
                ushort a = 0;

                if (exp.Type == ExpressionType.DEST)
                {
                    (valid, value) = destTable.Translate(exp.GetExpressionAsString());

                    if (!valid) throw new TranslationException($"invalid dest expression:{exp.GetExpressionAsString()}");


                }
                else if (exp.Type == ExpressionType.CMP)
                {
                    (valid, value) = compTable.Translate(exp.GetExpressionAsString());
                    if (!valid) throw new TranslationException($"invalid cmp expression:{exp.GetExpressionAsString()}");

                    a = compTable.GetaValue(exp.GetExpressionAsString());
                    instruction |= a;

                }
                else if (exp.Type == ExpressionType.JMP) 
                {
                    (valid, value) = jmpTable.Translate(exp.GetExpressionAsString());
                    if (!valid) throw new TranslationException($"invalid jmp expression:{exp.GetExpressionAsString()}");

                }

                instruction |= value;
            }

            return instruction;
        }

        public List<ushort> Translate(IList<TokenizedInstruction> tokenizedInstructions)
        {
            List<ushort> result = new List<ushort>();
            ushort value = 0;
            foreach (var instruction in tokenizedInstructions)
            {

                if (instruction.Type == InstructionType.A) value = TranslateAInstruction(instruction);
                else value = TranslateCInstructon(instruction);

                result.Add(value);
            }

            return result;
        }
    }
}
