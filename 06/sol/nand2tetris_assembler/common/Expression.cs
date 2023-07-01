using System.Text;

namespace common
{
    public enum ExpressionType
    {
        DEST,
        CMP,
        JMP,
        LABEL,
        LITERAL
    };

    // napraviti lookup tabele na osnovu expressiona i tokena
    // napraviti kasnije (deo 2) tabelu simbola 

    public class Expression
    {
        private List<Token> _tokens;
        public ExpressionType Type { get; set; }

        public Expression()
        {
            this._tokens = new List<Token>();
        }

        public void AddToken(Token token)
        {
            _tokens.Add(token);
        }

        public void PopLastToken()
        {
            _tokens.RemoveAt(_tokens.Count - 1);
        }

        public Token[] GetTokens()
        {
            return _tokens.ToArray();
        }

        public string GetExpressionAsString()
        {
            StringBuilder s = new StringBuilder();

            foreach (Token t in _tokens) s.Append(t.Value);   

            return s.ToString();
        }

        public override string ToString()
        {
            return $"Expression type:{Type}\n{GetExpressionAsString()}";
        }
    }
}
