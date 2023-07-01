using common;

namespace parser
{

    // for now this parser works withou symbols
    // and because of that it doesn't work with the @ character
    public class Parser
    {
        private HashSet<char> _newExprTokens;
        private HashSet<char> _cmpExprTokens;




        public Parser()
        {
            this._newExprTokens = new HashSet<char>();
            this._cmpExprTokens = new HashSet<char>();


            this._newExprTokens.Add('\n');
            this._newExprTokens.Add(';');

            this._cmpExprTokens.Add('-');
            this._cmpExprTokens.Add('+');
            this._cmpExprTokens.Add('!');
            this._cmpExprTokens.Add('&');
            this._cmpExprTokens.Add('|');

        }

        public IList<TokenizedInstruction> ParseFile(StreamReader file)
        {
            List<TokenizedInstruction> result = new List<TokenizedInstruction>();
            if (file == null) return result;

            string? line;

            while((line = file.ReadLine()) != null)
            {
                line = line.Trim();

                if (shouldSkipLine(line)) continue;
                else if (line.Contains("//")) line = line.Substring(0, line.IndexOf("//")-1);

                result.Add(parseLine(line));
            }

            return result;
        }

        private TokenizedInstruction parseLine(string line)
        {
            List<Expression> result = new List<Expression>();
            InstructionType instructionType = (line.StartsWith('@') || line.StartsWith('('))?InstructionType.A:InstructionType.C;

            Expression exp = new Expression();

            foreach(char c in line)
            {
                if (shouldSkipChar(c)) continue;

                if (instructionType == InstructionType.C)
                {
                    if (c == '=') 
                    {
                        exp.Type = ExpressionType.DEST;
                        result.Add(exp);

                        exp = new Expression();
                        exp.Type = ExpressionType.CMP;
                    } 
                    else if (c == ';')
                    {
                        exp.Type = ExpressionType.CMP;
                        result.Add(exp);

                        exp = new Expression();
                        exp.Type = ExpressionType.JMP;
                    }
                    else exp.AddToken(new Token(c));
                }
                else if (c != ';') exp.AddToken(new Token(c));
            }


            if (instructionType == InstructionType.A)
            {
                bool valid = ushort.TryParse(exp.GetExpressionAsString().Substring(1), out _);

                if (valid) exp.Type = ExpressionType.LITERAL;
                else exp.Type = ExpressionType.LABEL;
            }

            result.Add(exp);
            return new TokenizedInstruction(instructionType, result);
        }

        private bool startNewExpression(char c)
        {
            return _newExprTokens.Contains(c);
        }
        private bool shouldSkipChar(char c)
        {
            return char.IsWhiteSpace(c);
        }
        private bool shouldSkipLine(string line)
        {
            return line.StartsWith("//") || line == string.Empty;
        }
    }
}
