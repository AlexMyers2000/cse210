using System;

class Program
{
    static void Main(string[] args)
    {
       
        Random randomGenerator = new Random();
        int magicNumber = randomGenerator.Next(1, 101);

        int guess = -1;

        while (guess != magicNumber)
        {
            Console.Write("What number do you guess? ");
            guess = int.Parse(Console.ReadLine());

            if (magicNumber > guess)
            {
                Console.WriteLine("It's Higher");
            }
            else if (magicNumber < guess)
            {
                Console.WriteLine("It's Lower");
            }
            else
            {
                Console.WriteLine("Great job, you got it!!");
            }   


        }                    
    }
}