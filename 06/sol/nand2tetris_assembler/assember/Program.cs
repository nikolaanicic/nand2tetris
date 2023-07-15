
parser.Parser parser = new parser.Parser();
translator.Translator translator = new translator.Translator();


string filename = @"E:\GLUPOSTI\nand2tetris\nand2tetris\projects\06\Pong\Pong.asm";
string output = @"E:\GLUPOSTI\nand2tetris\nand2tetris\projects\06\Pong\Pong.hack";


using(var st = new StreamReader(filename))
{
    var cnt = 0;
    Console.WriteLine("parsing...");
    var tokenizedInstructions = parser.ParseFile(st);

    Console.WriteLine("translating...");

    var translatedInstructions = translator.Translate(tokenizedInstructions);
    Console.WriteLine($"writing to {output}");

    using (var st2 = new StreamWriter(output))
    {
        for (int i = 0; i < translatedInstructions.Count - 1; i++)
        {
            st2.WriteLine(Convert.ToString(translatedInstructions[i], 2).PadLeft(16, '0'));
            Console.WriteLine(Convert.ToString(translatedInstructions[i], 2).PadLeft(16, '0'));
            cnt++;
        }
        st2.Write(Convert.ToString(translatedInstructions[translatedInstructions.Count - 1], 2).PadLeft(16, '0'));
        cnt++;

    }

    Console.WriteLine($"file contains {cnt} instructions");
}



