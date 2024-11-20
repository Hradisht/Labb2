Console.WriteLine("Enter a string: ");
string input = Console.ReadLine();
string numbers = "1234567890";
//string characters = "abcdefghijklmnopqrstuvwxyz";

string number = "";
//int sum = 0;

for (int i = 0; i < input.Length; i++)
{
    string foundNumberPattern = "";
    char currentChar = input[i];

    if (numbers.Contains(currentChar))
    {
        // lägg den första siffran i tal-strängen, foundNumberPattern
        number += currentChar;
        foundNumberPattern += currentChar;

        // hitta mönstret
        for (int j = i + 1; j < input.Length; j++)
        {
            char innerChar = input[j];
            if (numbers.Contains(innerChar))
            {
                // lägg till varje hittad siffra i foundNumberPattern
                foundNumberPattern += innerChar;


                // om vi hittar startsiffran igen, skriv ut
                if (innerChar == currentChar)
                {


                    // om startsiffran inte är den första i input, skriv ut strängen innan delsträngen
                    if (i != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(input.Substring(0, i));
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    // skriv ut talserien vi hittat, dvs inrenumber
                    Console.Write(foundNumberPattern);

                    Console.ForegroundColor = ConsoleColor.White;
                   // sum += Int32.Parse(foundNumberPattern);
                    if (j != input.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(input.Substring(j + 1));
                        break;
                    }

                }
            }
            else
            {
                break;
            }

        }
    }
}
//Console.WriteLine("totalsumman är: " + sum);