using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccountManagementProgram
{
    public class BankManager
    {
        public ulong Balance { get; private set; } = 0;
        public List<BankStatement> BankStatements { get; private set; }

        public bool TryDepositAndSave(ulong amount)
        {
            // checked로 숫자 빼서 예외 시 false 반환
            // 성공 시 true 반환

            try
            {
                checked
                {
                    Balance = Balance + amount;
                }

                bool isSaveSucceed = SaveManager.TrySaveStatement(BankStatements, Balance, amount, isDeposit: true);
                if (!isSaveSucceed) Program.ClearAndPrintMessage("거래 내역 저장에 실패했습니다. 관리자 권한으로 실행 중인지 확인해주세요.", 7);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryWithdrawalAndSave(ulong amount)
        {
            // checked로 숫자 더해서 예외 시 false 반환
            // 성공 시 true 반환

            try
            {
                checked
                {
                    Balance = Balance - amount;
                }

                bool isSaveSucceed = SaveManager.TrySaveStatement(BankStatements, Balance, amount, isDeposit: false);
                if (!isSaveSucceed) Program.ClearAndPrintMessage("거래 내역 저장에 실패했습니다. 관리자 권한으로 실행 중인지 확인해주세요.", 15);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void CheckSaveFolderAndFileAndLoad()
        {
            List<BankStatement> _statements = BankStatements;
            ulong _balance = Balance;

            SaveManager.CheckSaveFolderAndFileAndLoad(ref _balance, ref _statements);

            Balance = _balance;
            BankStatements = _statements;
        }
}
}
