using System;
using System.Linq;

namespace What_weekday_is_today
{
    class Program
    {
        static string[] dayOfWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thurday", "Friday", "Saturday" };
        static void Main(string[] args)
        {
            bool loop = true;
            Console.WriteLine("What weekday is today?");
            while (loop)
            {
                // Get number of year
                getYearFromUser(out int year, out bool isLeapYear);

                // Get number of month
                getMonthFromUser(out int month);
                
                // Get number of day
                getDayFromUser(isLeapYear, month, out int day);
                
                // Calculate the day of the week from date
                calDayOfWeek(year, month, day, isLeapYear);

                // Ask for loop program
                loop = askForRunAgain();
            }
        }

        // function for get number of year from user
        private static void getYearFromUser(out int year, out bool isLeap)
        {
            while (true)
            {
                Console.Write("Please enter Christian year (Range of year is from 1900 afterward): ");
                var foo = Console.ReadLine();
                if (int.TryParse(foo, out int number))
                {
                    if (number >= 1900)
                    {
                        // for check is leap year
                        if (number % 4 != 0)
                        {
                            isLeap = false;
                        }
                        else if (number % 100 != 0)
                        {
                            isLeap = true;
                        }
                        else if (number % 400 != 0)
                        {
                            isLeap = false;
                        }
                        else
                        {
                            isLeap = true;
                        }
                        year = number;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Range of year is from 1900 afterward!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter number of Christian year!");
                }
            }
        }

        // function for get number of month from user
        private static void getMonthFromUser(out int month)
        {
            while (true)
            {
                Console.Write("Please enter number of month (1 to 12): ");
                var foo = Console.ReadLine();
                if (int.TryParse(foo, out int number))
                {
                    if (number >= 1 && number <= 12)
                    {
                        month = number;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Range of month is from 1 to 12");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter number of month!");
                }
            }
        }

        // function for get number of day from user
        private static void getDayFromUser(bool isLaepYear, int month, out int day)
        {
            while (true)
            {
                int maxDay = 31;
                int[] month30Day = { 4, 6, 9, 11 }; // array of month that have 30 days.
                if (month == 2)
                {
                    maxDay = isLaepYear ? 29 : 28;
                }
                else
                {
                    maxDay = month30Day.Contains(month) ? 30 : 31;
                }
                Console.Write($"Please enter number of day (1 to {maxDay}): ");
                var foo = Console.ReadLine();
                if (int.TryParse(foo, out int number))
                {
                    if (number >= 1 && number <= maxDay)
                    {
                        day = number;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Range of day is from 1 to {maxDay}");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter number of day!");
                }
            }
        }

        // function for calculate the day of the week from date
        private static void calDayOfWeek(int year, int month, int day, bool isLeapYear)
        {
            // m is the shifted month (1 = March, ..., 10 = December, 11 = Jan, 12 = Feb) Treat Jan & Feb as months of the preceding year
            int[] m = { 11, 12, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // C is century (1987 has C = 19)
            var C = Math.Floor((double)year / 100);

            // Y is the last 2 digit of year (1987 has Y = 87 except Y = 86 for Jan & Feb)
            int Y = month == 1 || month == 2 ? (year % 100) - 1 : year % 100;

            // modulus function
            int mod(int x, int m)
            {
                int r = x % m;
                return r < 0 ? r + m : r;
            }

            // Gauss's algorithm
            var W = mod((int)(day + Math.Floor((2.6 * m[month - 1]) - 0.2) - (2 * C) + Y + Math.Floor((double)Y / 4) + Math.Floor((C / 4))), 7); //W is week day (0 = Sunday, ..., 6 = Saturday)

            // For the last 2 digits of year is 00 and isn't leap year
            if (Y < 0 && !isLeapYear)
            {
                Y = 99;
                W = mod((int)(day + Math.Floor((2.6 * m[month - 1]) - 0.2) - (2 * (C - 1)) + Y + Math.Floor((double)Y / 4) + Math.Floor(((C - 1) / 4))), 7);
            }

            Console.WriteLine($"The weekday of the input date is {dayOfWeek[Math.Abs((int)W)]}");
        }

        // function for looping program
        private static bool askForRunAgain()
        {
            while (true)
            {
                Console.Write("Calculate the day of the week again?(y/n): ");
                var foo = Console.ReadLine();
                if (foo is string)
                {
                    if (foo.ToLower().Contains("y") || foo.ToLower().Contains("n"))
                    {
                        return foo.ToLower().Contains("y");
                    }
                    else
                    {
                        Console.WriteLine("Please enter y/n!");
                    }
                }
                else
                {
                    Console.WriteLine("Please enter y/n");
                }
            }
        }
    }
}
