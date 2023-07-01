namespace common
{

    /// <summary>
    /// The jump translation table contains all of the available jump commands
    /// in the jack assembly
    /// Each table entry is already preset by the zero padding so it can just be OR-ed with the rest of the instruction
    /// </summary>
    public class JmpTranslationTable : TranslationTable
    {


        protected override void PopulateTable()
        {
            table.Add("JGT",    0b001);
            table.Add("JEQ",    0b010);
            table.Add("JGE",    0b011);
            table.Add("JLT",    0b100);
            table.Add("JNE",    0b101);
            table.Add("JLE",    0b110);
            table.Add("JMP",    0b111);
        }
    }
}
