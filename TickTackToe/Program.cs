using System;
using System.Linq;

namespace TickTackToe
{
    class Program
    {
        //┌─┬─┬─┐
        //│6│7│8│
        //├─┼─┼─┤
        //│3│4│5│
        //├─┼─┼─┤
        //│0│1│2│
        //└─┴─┴─┘

        private static (bool Placed, bool X, bool O)[] Playground = new (bool Placed, bool X, bool O)[9];
        private static int Round = 0;

        static void Main(string[] args)
        {
            Console.Title = "Tick Tack Toe";
            Console.CursorVisible = false;
            Console.CancelKeyPress += (s, e) => Environment.Exit(0);

            DrawPlayground();
            while (true)
            {
                if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int i))
                {
                    i--;

                    if (i == -1)
                        continue;

                    if (!Playground[i].Placed)
                    {
                        Round++;
                        var x = Round % 2 == 1;
                        Playground[i].Placed = true;
                        Playground[i].X = x;
                        Playground[i].O = !x;

                        DrawPlayground();

                        if (CheckWin() || Round == 9)
                            Reset();
                    }
                }
            }
        }

        static void DrawPlayground()
        {
            Console.Clear();
            var lines = Playground.Select(x => x.Placed ? x.X ? "X" : "O" : " ");

            Console.WriteLine("┌─┬─┬─┐");
            Console.WriteLine("│" + string.Join('│', lines.TakeLast(3)) + "│");
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine("│" + string.Join('│', lines.SkipLast(3).TakeLast(3)) + "│");
            Console.WriteLine("├─┼─┼─┤");
            Console.WriteLine("│" + string.Join('│', lines.SkipLast(6).TakeLast(3)) + "│");
            Console.WriteLine("└─┴─┴─┘");
        }

        static bool CheckWin()
        {
            bool Check(bool CheckX)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckX)
                    {
                        // vertical 
                        if (Playground[i].X && Playground[i + 3].X && Playground[i + 6].X)
                            return true;

                        // horizontal
                        if (Playground[i * 3].X && Playground[i * 3 + 1].X && Playground[i * 3+ 2].X)
                            return true;
                    }
                    else
                    {
                        // vertical 
                        if (Playground[i].O && Playground[i + 3].O && Playground[i + 6].O)
                            return true;

                        // horizontal
                        if (Playground[i * 3].O && Playground[i * 3 + 1].O && Playground[i * 3 + 2].O)
                            return true;
                    }
                }

                // diagonal
                if (CheckX)
                {
                    if ((Playground[0].X && Playground[4].X && Playground[8].X) ||
                        (Playground[6].X && Playground[4].X && Playground[2].X))
                        return true;
                }
                else
                {
                    if ((Playground[0].O && Playground[4].O && Playground[8].O) ||
                        (Playground[6].O && Playground[4].O && Playground[2].O))
                        return true;
                }

                return false;
            }

            if (Check(true))
                Console.WriteLine("X won!");
            else if (Check(false))
                Console.WriteLine("O won!");
            else
                return false;

            return true;
        }

        static void Reset()
        {
            Round = 0;
            Array.Clear(Playground, 0, Playground.Length);
        }
    }
}
