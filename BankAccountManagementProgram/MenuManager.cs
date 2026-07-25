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

        public void Start()
        {
            // 메인 메뉴 불러오기

            throw new NotImplementedException("MenuManager의 Start가 구현되지 않았습니다.");
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

            // 콘솔 클리어
            // 메인 메뉴 출력

            // 맞는 숫자 입력 시 맞게 이동
            // 다른 숫자 입력 시 "잘못된 입력입니다." 출력

            throw new NotImplementedException("MenuManager의 MainMenu이 구현되지 않았습니다.");
        }

        private void DepositMenu()
        {
            // 입금 메뉴 출력

            /*
             * 예시 >
             * 입금 금액 : 50000
             * 50,000원이 입금되었습니다.
             */

            // 콘솔 클리어
            // 입금 메뉴 출력

            // while true
            // IOManager의 TryInputNumber로 숫자 입력 받기
            // false 반환 시 "잘못된 입력입니다." 밑에 출력, continue

            // 0이면 "0 초과의 금액을 입력해주세요." 밑에 출력, continue

            // BankManager의 TryWithdrawalAndSave으로 출금 시도
            // false 반환 시 "계좌 최대 보유 금액에 도달했습니다." 밑에 출력, continue

            // IOManager의 PrintNumberWithComma로 숫자 출력 후 "원이 입금되었습니다".
            // "키를 입력해 돌아가기" 출력하고 키 입력 시 메인 메뉴

            throw new NotImplementedException("MenuManager의 DepositMenu이 구현되지 않았습니다.");
        }

        private void WithDrawalMenu()
        {
            /*
             * 예시 >
             * 출금 금액 : 10000
             * 10,000원이 출금되었습니다.
             */

            // 잔액 부족 예시 > 현재 잔액이 부족합니다.

            // 콘솔 클리어
            // 출금 메뉴 출력

            // while true
            // IOManager의 TryInputNumber로 숫자 입력 받기
            // false 반환 시 "잘못된 입력입니다." 밑에 출력, continue

            // 0이면 "0 초과의 금액을 입력해주세요." 밑에 출력, continue

            // BankManager의 TryDepositAndSave으로 출금 시도
            // false 반환 시 "현재 잔액이 부족합니다." 밑에 출력, continue

            // IOManager의 PrintNumberWithComma로 숫자 출력 후 "원이 출금되었습니다".
            // "키를 입력해 돌아가기" 출력하고 키 입력 시 메인 메뉴

            throw new NotImplementedException("MenuManager의 WithDrawalMenu이 구현되지 않았습니다.");
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

            throw new NotImplementedException("MenuManager의 BalanceCheckMenu가 구현되지 않았습니다.");
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
            // Q를 입력해 나가기

            throw new NotImplementedException("MenuManager의 BankStatementMenu가 구현되지 않았습니다.");
        }
    }
}
