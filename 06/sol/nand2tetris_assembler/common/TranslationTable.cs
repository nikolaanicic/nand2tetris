namespace common
{
    public abstract class TranslationTable
    {
        protected Dictionary<string, ushort> table;
        protected abstract void PopulateTable();

        public TranslationTable()
        {
            table = new Dictionary<string, ushort>();
            PopulateTable();
        }

        public (bool,ushort) Translate(string expr)
        {
            if (table.ContainsKey(expr)){
                return (true, table[expr]);
            }
            return (false, 0);
        }
    }
}
