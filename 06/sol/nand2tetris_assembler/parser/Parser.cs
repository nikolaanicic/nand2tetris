using common;

namespace parser
{
    // the parser should be rewritten with better data models
    // the parsing should be done line by line
    // that means the parser should get a file
    // i want to be able to step through the parsing process
    // meaning when i click a button i want the parser and the translator to do the following:
    // the parser should get the next line from a file and parse the expressions contained in that line
    // and then it should give those expressions to the translator which should assemble the instruction if it can

    public class Parser
    {
        private HashSet<char> _newExprTokens;
        private HashSet<char> _cmpExprTokens;
        private SymbolTable _symbolTable;

        public Parser(SymbolTable symbolTable)
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

            this._symbolTable = symbolTable;

        }

        public IList<TokenizedInstruction> ParseFile(StreamReader file)
        {
            List<TokenizedInstruction> result = new List<TokenizedInstruction>();
            if (file == null) return result;

            string? line;
            ushort lineCounter = 0;

            while((line = file.ReadLine()) != null)
            {
                line = line.Trim();

                if (shouldSkipLine(line)) continue;
                else if (line.Contains("//")) line = line.Substring(0, line.IndexOf("//")-1);

                var inst = parseLine(line);
                inst.Line = lineCounter++;


                if(inst.Type == InstructionType.A)
                {
                    var expression = inst.GetExpressions()[0];
                    var str = expression.GetExpressionAsString();
                    if (str[0] == '(')
                    {
                        str = new string(str.Where(e => e != '(' && e != ')').ToArray());
                        _symbolTable.Put(str, inst.Line);
                    }

                }

                result.Add(inst);
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
