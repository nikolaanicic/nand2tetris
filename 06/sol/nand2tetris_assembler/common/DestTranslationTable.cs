
namespace common
{

    /// <summary>
    /// The destination translation table contains all of the possible destinations that can hold a value
    /// Each destination is padded with 3 zeroes so it can be OR-ed with the rest of the instruction
    /// 
    /// c instruction format is:
    ///     111accccccdddjjj
    ///     
    /// so when the padding is applied
    ///     111accccccddd000
    /// </summary>
    public class DestTranslationTable : TranslationTable
    {
        protected override void PopulateTable()
        {
            table.Add("M",  0b001000);
            table.Add("D",  0b010000);
            table.Add("DM", 0b011000);
            table.Add("MD", 0b011000);
            table.Add("A",  0b100000);
            table.Add("AM", 0b101000);
            table.Add("MA", 0b101000);
            table.Add("AD", 0b110000);
            table.Add("DA", 0b110000);
            table.Add("ADM",0b111000);
            table.Add("AMD", 0b111000);
            table.Add("MDA", 0b111000);
            table.Add("DAM", 0b111000);
            table.Add("DMA", 0b111000);
            table.Add("MAD", 0b111000);
        }
    }
}
