namespace common
{
    public enum InstructionType
    {
        A,
        C
    };

    public class TokenizedInstruction
    {
        private List<Expression> expressions;
        public InstructionType Type { get; }

        public TokenizedInstruction(InstructionType type, IList<Expression> expressions)
        {
            this.expressions = new List<Expression>();
            this.expressions.AddRange(expressions);
            
            this.Type = type;
        }

        public string GetInstructionAsString()
        {
            string result = string.Empty;
            expressions.ForEach(e => result += e.GetExpressionAsString());

            return result;
        }

        public override string ToString()
        {
            return $"Instruction type:{Type}\nExpressions:\n{string.Join("\n", expressions)}";
        }

        public Expression[] GetExpressions()
        {
            return expressions.ToArray();
        }
    }
}
