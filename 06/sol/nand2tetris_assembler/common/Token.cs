namespace common
{
    public class Token
    {
        private string _name;
        private char _value;
        public char Value { get { return _value; } }


        public Token(char value)
        {
            _value = value;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !obj.GetType().Equals(typeof(Token))) return false;

            Token other = (Token)obj;

            return other.Value == _value;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
