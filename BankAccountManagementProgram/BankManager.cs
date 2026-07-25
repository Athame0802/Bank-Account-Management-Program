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

            throw new NotImplementedException("BankManager의 TryDeposit이 구현되지 않았습니다.");
        }

        public bool TryWithdrawalAndSave(ulong amount)
        {
            // checked로 숫자 더해서 예외 시 false 반환
            // 성공 시 true 반환

            throw new NotImplementedException("BankManager의 TryWithdrawal이 구현되지 않았습니다.");
        }
    }
}
