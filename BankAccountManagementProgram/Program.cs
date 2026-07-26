namespace BankAccountManagementProgram
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BankManager bankManager = new();
            MenuManager menuManager = new(bankManager);

            menuManager.Start();
        }

        public static void Quit()
        {
            Environment.Exit(0);
        }

        public static void ClearAndPrintMessage(string message, int pos = 0)
        {
            int currentCursorTopPosition = Console.CursorTop;
            int currentCursorLeftPosition = Console.CursorLeft;

            Console.SetCursorPosition(0, currentCursorTopPosition + pos);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition(0, currentCursorTopPosition + pos);
            Console.Write(message);
            if (pos == 0) currentCursorLeftPosition = Console.CursorLeft;

            Console.SetCursorPosition(currentCursorLeftPosition, currentCursorTopPosition);
        }

        public static void ClearAndPrintMessageAbsolute(string message, int pos = 0, bool shouldGoBack = true)
        {
            int currentCursorTopPosition = Console.CursorTop;

            Console.SetCursorPosition(0, pos);
            Console.Write(new string(' ', Console.WindowWidth));

            Console.SetCursorPosition(0, pos);
            Console.Write(message);

            if (shouldGoBack)
                Console.SetCursorPosition(0, currentCursorTopPosition);
        }
    }
}
