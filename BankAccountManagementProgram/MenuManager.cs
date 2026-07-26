using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public enum Menu
    {
        Main,
        Deposit,
        Withdrawal,
        BalanceCheck,
        BankStatement,
        Quit
    }

    public class MenuManager
    {
        private Menu currentMenu = Menu.Main;
        private const int ERROR_PRINT_LINE_POS = 3;
        private BankManager bankManager = default;

        public MenuManager(BankManager bankManager)
        {
            this.bankManager = bankManager;
        }

        public void Start()
        {
            bankManager.CheckSaveFolderAndFileAndLoad();
            Program.RequestAdministrator();
            MainMenu();
        }

        private void MainMenu()
        {
            /*
             * 예시 >
             *  ========================
             *  은행 계좌 관리 프로그램
             *  ========================
             *  
             *  1. 입금
             *  2. 출금
             *  3. 잔액 조회
             *  4. 거래 내역 조회
             *  5. 종료
             */

            /*
            // 콘솔 클리어
            // 메인 메뉴 출력

            // 맞는 숫자 입력 시 맞게 이동
            // 다른 숫자 입력 시 "잘못된 입력입니다." 출력
            */

            Console.Clear();

            
            Console.Write
                ("""
                ========================
                은행 계좌 관리 프로그램
                ========================

                1. 입금
                2. 출금
                3. 잔액 조회
                4. 거래 내역 조회
                5. 종료

                메뉴 번호 입력 : 
                """);

            if (!Program.isAdministrator()) Program.ClearAndPrintMessage("관리자 권한으로 실행하지 않아 파일이 저장되지 않을 수도 있습니다.", ERROR_PRINT_LINE_POS);

            switch (SaveManager.LoadStatus)
            {
                case LoadStatus.NoFile:
                    Program.ClearAndPrintMessage("저장된 파일이 존재하지 않습니다.", ERROR_PRINT_LINE_POS + 1);
                    break;
                case LoadStatus.LoadFailed:
                    Program.ClearAndPrintMessage("세이브 로드에 실패했습니다.", ERROR_PRINT_LINE_POS + 1);
                    break;
                case LoadStatus.LoadSucceed:
                    Program.ClearAndPrintMessage("세이브 로드에 성공했습니다.", ERROR_PRINT_LINE_POS + 1);
                    break;
            }

            int menuInput = default;

            while (true)
            {
                Program.ClearAndPrintMessageAbsolute("메뉴 번호 입력 :", 10, false);

                bool isInputAlright = IOManager.TryInputNumber(out menuInput);
                if (!isInputAlright) {
                    Program.ClearAndPrintMessage("잘못된 입력입니다.", ERROR_PRINT_LINE_POS);
                    continue;
                }
                else
                {
                    break;
                }
            }

            switch ((Menu)menuInput)
            {
                case Menu.Deposit:
                    DepositMenu();
                    break;
                case Menu.Withdrawal:
                    WithdrawalMenu();
                    break;
                case Menu.BalanceCheck:
                    BalanceCheckMenu();
                    break;
                case Menu.BankStatement:
                    BankStatementMenu();
                    break;
                case Menu.Quit:
                    Program.Quit();
                    break;
            }
        }

        private void DepositMenu()
        {
            /*
             * 예시 >
             * 입금 금액 : 50000
             * 50,000원이 입금되었습니다.
             */

            /*
            // 콘솔 클리어
            // 입금 메뉴 출력

            // while true
            // IOManager의 TryInputNumber로 숫자 입력 받기
            // false 반환 시 "잘못된 입력입니다." 밑에 출력, continue

            // 0이면 "0 초과의 금액을 입력해주세요." 밑에 출력, continue

            // BankManager의 TryDepositlAndSave으로 출금 시도
            // false 반환 시 "계좌 최대 보유 금액에 도달했습니다." 밑에 출력, continue

            // IOManager의 PrintNumberWithComma로 숫자 출력 후 "원이 입금되었습니다".
            // "키를 입력해 돌아가기" 출력하고 키 입력 시 메인 메뉴
            */

            
            ulong input = default;
            bool isQuit = false;

            Console.Clear();

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Program.ClearAndPrintMessage("입금 금액 (Q를 눌러 돌아가기) :");

                bool isInputAlright = IOManager.TryInputNumberAndQuit(out input, out isQuit);
                if (!isInputAlright)
                {
                    Program.ClearAndPrintMessage("잘못된 입력입니다.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                if (isQuit) break;
                
                if (input == 0)
                {
                    Program.ClearAndPrintMessage("0 초과 금액을 입력해 주세요.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                bool isDepositSucceed = bankManager.TryDepositAndSave(input);
                if (!isDepositSucceed)
                {
                    Program.ClearAndPrintMessage("계좌 최대 보유 금액에 도달했습니다.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                break;
            }

            if (isQuit)
            {
                MainMenu();
                return;
            }

            Program.ClearAndPrintMessage("", ERROR_PRINT_LINE_POS);
            IOManager.PrintNumberWithComma(input); Console.Write($"원이 입금되었습니다. / 현재 계좌 내 금액 : {bankManager.Balance}");

            PressKeyToMainMenu(ERROR_PRINT_LINE_POS + 1);
        }

        private void WithdrawalMenu()
        {
            /*
             * 예시 >
             * 출금 금액 : 10000
             * 10,000원이 출금되었습니다.
             */

            /*
            // 잔액 부족 예시 > 현재 잔액이 부족합니다.

            // 콘솔 클리어
            // 출금 메뉴 출력

            // while true
            // IOManager의 TryInputNumber로 숫자 입력 받기
            // false 반환 시 "잘못된 입력입니다." 밑에 출력, continue

            // 0이면 "0 초과의 금액을 입력해주세요." 밑에 출력, continue

            // BankManager의 TryWithdrawalAndSave으로 출금 시도
            // false 반환 시 "현재 잔액이 부족합니다." 밑에 출력, continue

            // IOManager의 PrintNumberWithComma로 숫자 출력 후 "원이 출금되었습니다".
            // "키를 입력해 돌아가기" 출력하고 키 입력 시 메인 메뉴
            */

            Console.Clear();

            ulong input = default;
            bool isQuit = false;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Program.ClearAndPrintMessage($"출금 금액 - 현재 잔고 : {bankManager.Balance.ToString("N0")}원 (Q를 눌러 돌아가기) :");

                bool isInputAlright = IOManager.TryInputNumberAndQuit(out input, out isQuit);
                if (!isInputAlright)
                {
                    Program.ClearAndPrintMessage("잘못된 입력입니다.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                if (isQuit) break;

                if (input == 0)
                {
                    Program.ClearAndPrintMessage("0 초과 금액을 입력해 주세요.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                bool isWithdrawalSucceed = bankManager.TryWithdrawalAndSave(input);
                if (!isWithdrawalSucceed)
                {
                    Program.ClearAndPrintMessage("현재 잔액이 부족합니다.", ERROR_PRINT_LINE_POS);
                    continue;
                }

                break;
            }

            if (isQuit) 
            {
                MainMenu();
                return;
            }

            IOManager.PrintNumberWithComma(input); Console.Write($"원이 출금되었습니다. / 현재 계좌 내 금액 : {bankManager.Balance}");

            PressKeyToMainMenu(ERROR_PRINT_LINE_POS + 1);
        }

        private void BalanceCheckMenu()
        {
            /*
             * 예시 > 
             * 현재 잔액               
             * 125,000원
             */

            // 콘솔 클리어
            // BankManager의 Balance를 가져와 출력

            Console.Clear();
            Console.WriteLine("현재 잔액");
            Console.WriteLine($"{bankManager.Balance.ToString("N0")}원");

            PressKeyToMainMenu(ERROR_PRINT_LINE_POS);
        }

        private void BankStatementMenu()
        {
            /*
             * 예시 >
             *  [2026-07-24 18:32:11]
             *  입금
             *  50,000원
             *  ----------------------------
             *  [2026-07-24 18:35:02]
             *  출금
             *  10,000원
             *  ----------------------------
             */

            // 콘솔 클리어
            // BankManager의 BankStatement를 가져와서 모두 출력

            Console.Clear();

            foreach (BankStatement statement in bankManager.BankStatements)
            {
                Console.WriteLine($"[{statement.Time}]");
                Console.WriteLine(statement.IsDeposit ? "입금" : "출금");
                IOManager.PrintNumberWithComma(statement.Amount); Console.WriteLine("원");
                Console.WriteLine("----------------------------");
            }

            PressKeyToMainMenu(ERROR_PRINT_LINE_POS);
        }

        private void PressKeyToMainMenu(int pos = ERROR_PRINT_LINE_POS)
        {
            Program.ClearAndPrintMessage("아무 키나 눌러 메인 메뉴로 돌아가기", pos);
            Console.ReadKey();
            MainMenu();
        }
    }
}
