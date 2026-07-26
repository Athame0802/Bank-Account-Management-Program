using System.Diagnostics;
using System.Security.Principal;

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

        public static bool isAdministrator()
        {
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    if (identity == null) return false;

                    WindowsPrincipal principal = new(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
        }

        public static void RequestAdministrator()
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    FileName = Process.GetCurrentProcess().MainModule?.FileName,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                Process.Start(info);
            }
            catch
            {
                return;
            }
        }
    }
}
