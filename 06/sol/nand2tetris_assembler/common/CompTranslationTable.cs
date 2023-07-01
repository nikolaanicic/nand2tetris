namespace common
{

    /// <summary>
    /// The comparison translation table contains all of the comp expressions available in the jack assembly
    /// Each table entry has been padded with zeroes so it can just be OR-ed with the rest of the instruction
    /// 
    /// c instruction format is: 
    ///     111accccccdddjjj
    ///     
    /// so when the padding is applied:
    ///     111acccccc000000
    /// </summary>
    public class CompTranslationTable : TranslationTable
    {
        private HashSet<string> _aValueOneSet = new HashSet<string>();
        protected override void PopulateTable()
        {
            // PART OF THE TABLE THAT SHOULD
            // BE INDEXED WITH a=0
            table.Add("0",     0b101010000000);
            table.Add("1",     0b111111000000);
            table.Add("-1",    0b111010000000);
            table.Add("D",     0b001100000000);
            table.Add("A",     0b110000000000);
            table.Add("!D",    0b001101000000);
            table.Add("!A",    0b110001000000);
            table.Add("-D",    0b001111000000);
            table.Add("-A",    0b110011000000);
            table.Add("D+1",   0b011111000000);
            table.Add("A+1",   0b110111000000);
            table.Add("D-1",   0b001110000000);
            table.Add("A-1",   0b110010000000);
            table.Add("D+A",   0b000010000000);
            table.Add("D-A",   0b010011000000);
            table.Add("A-D",   0b000111000000);
            table.Add("D&A",   0b000000000000);
            table.Add("D|A",   0b010101000000);



            // PART OF THE TABLE THAT SHOULD
            // BE INDEXED WITH a=1
            table.Add("M",     0b110000000000);
            table.Add("!M",    0b110001000000);
            table.Add("-M",    0b110011000000);
            table.Add("M+1",   0b110111000000);
            table.Add("M-1",   0b110010000000);
            table.Add("D+M",   0b000010000000);
            table.Add("D-M",   0b010011000000);
            table.Add("M-D",   0b000111000000);
            table.Add("D&M",   0b000000000000);
            table.Add("D|M",   0b010101000000);

            _aValueOneSet.Add("M");
            _aValueOneSet.Add("-M");
            _aValueOneSet.Add("M+1");
            _aValueOneSet.Add("M-1");
            _aValueOneSet.Add("D+M");
            _aValueOneSet.Add("D-M");
            _aValueOneSet.Add("M-D");
            _aValueOneSet.Add("D&M");
            _aValueOneSet.Add("D|M");
        }

        public ushort GetaValue(string expression)
        {
            return (ushort) (_aValueOneSet.Contains(expression) ? 0b1000000000000 : 0);
        }
    }
}
