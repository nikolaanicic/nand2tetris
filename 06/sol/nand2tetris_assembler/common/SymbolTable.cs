namespace common
{
    public class SymbolTable
    {
        private Dictionary<string,ushort> _symbols;

        public SymbolTable()
        {
            _symbols = new Dictionary<string,ushort>();
            _symbols.Add("R0",  0);
            _symbols.Add("R1",  1);
            _symbols.Add("R2",  2);
            _symbols.Add("R3",  3);
            _symbols.Add("R4",  4);
            _symbols.Add("R5",  5);
            _symbols.Add("R6",  6);
            _symbols.Add("R7",  7);
            _symbols.Add("R8",  8);
            _symbols.Add("R9",  9);
            _symbols.Add("R10", 10);
            _symbols.Add("R11", 11);
            _symbols.Add("R12", 12);
            _symbols.Add("R13", 13);
            _symbols.Add("R14", 14);
            _symbols.Add("R15", 15);
            
            _symbols.Add("SCREEN", 16384);
            _symbols.Add("SP", 0);
            _symbols.Add("LCL", 1);
            _symbols.Add("ARG", 2);
            _symbols.Add("THIS", 3);
            _symbols.Add("THAT", 4);
        }

        public (ushort,bool) Get(string key)
        {
            if (_symbols.ContainsKey(key.ToUpper())) return (_symbols[key.ToUpper()], true);

            return (0, false);
        }

        public void Put(string key, ushort value)
        {
            _symbols[key.ToUpper()] = value;
        }
    }
}
