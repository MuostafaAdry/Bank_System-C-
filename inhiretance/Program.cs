namespace inhiretance
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public  override string ToString()
        {
            return $"The name: {Name} and the balance: {Balance}";
        }
        
    }

    public class SavingsAccount : Account
    {
        public SavingsAccount(string Name = "Unnamed SavingsAccount", double Balance = 0.0,double rate=0.0) :base(Name,Balance)
        {
            Rate = rate;
        }

        public double Rate { get; set; }
        public override bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return  false;
        }

        public override bool Withdraw(double amount)
        {
            //if (Balance - amount >= 0)
            //{
            //    Balance -= amount;
            //    return true;
            //}
            return base.Withdraw(amount + Rate);
        
        }
         
        public override string ToString()
        {
            return $"{base.ToString()} and the rate:{Rate}";
        }
         //operator overloding
        public static SavingsAccount operator +(SavingsAccount lhs, SavingsAccount rhs)
        {
            SavingsAccount saving = new SavingsAccount( lhs.Name  +  rhs.Name, lhs.Balance + rhs.Balance  );
            return saving;
        }
    }
    
    public class TrustAccount:SavingsAccount
    {
        private int Buns = 50;
        private int NumOfWithdrow = 0;

        private DateTime dateAfterYear = DateTime.Now.AddYears(1);
         
      public virtual bool Withdraw(double amount)
        {
            if ((dateAfterYear - DateTime.Now).TotalDays >= 360)
            {
                NumOfWithdrow = 0;
                dateAfterYear = DateTime.Now;
            }
            if (Balance - amount >= 0 && amount < Balance * 0.02 && NumOfWithdrow<3)
             {
                    Balance -= amount;
                    NumOfWithdrow++;                     
                    return true;
            }
                
            
            return false;
            




        }
        public override bool Deposit(double amount)
        {

            if (amount > 0 && amount>=5000)
            {
                Balance +=( amount+ Buns);

                return true;
            }else if (amount > 0  )
            {
                Balance += amount;
                return true;
            }
            else
            {
                Balance += amount;
            }
            return false;
        }
       

    }
    public class CheckingAccount :Account
    {
        public CheckingAccount(double fee=0.0, string Name = "Unnamed CheckingAccount", double Balance = 0.0) : base(Name, Balance)
        {
            Fee = fee;
        }
        public override string ToString()
        {
              return $"{base.ToString()}, Rate: {Fee}";
        }
        public double Fee { get; set; }
        public override bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public override bool Withdraw(double amount)
        {
            //if (Balance - amount >= 0)
            //{
            //    Balance -= amount;
            //    return true;
            //}
            return base.Withdraw(amount - Fee);

            
           
        }
       
    }


    public static class AccountUtil
    {
        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
                 
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }

    public class Program
    {
        static void Main()
        {

            //  DateTime dateNow = DateTime.Now;
            //  DateTime dateAfterYear = DateTime.Now.AddYears(1);
            //if ((dateAfterYear - dateNow).TotalDays >=360)
            //{
            //    Console.WriteLine($"true{(dateAfterYear - dateNow).TotalDays}");
            //}
             


            Account acount = new Account("mostafa",2000);
            SavingsAccount savingacount = new SavingsAccount();
            SavingsAccount saving1 = new SavingsAccount("mostafa",3000,0.5);
            SavingsAccount saving2 = new SavingsAccount("adry", 3000, 0.5);
            SavingsAccount saving3 = saving1 + saving2;
            Console.WriteLine($"operator overloding:{saving3.Balance}");
            CheckingAccount checkingaccount = new CheckingAccount();
           //output ToString
            Console.WriteLine(acount);
            Console.WriteLine(savingacount);
            Console.WriteLine(checkingaccount);
 
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);
            Console.WriteLine("end accounts ======================");


            // Savings
            var savAccounts = new List<Account>();
            savAccounts.Add(new Account());
            savAccounts.Add(new Account("Superman"));
            savAccounts.Add(new Account("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(savAccounts);
            AccountUtil.Deposit(savAccounts, 1000);
            AccountUtil.Withdraw(savAccounts, 2000);
            Console.WriteLine("end saving ======================");

            // Checking
            var checAccounts = new List<Account>();

            checAccounts.Add(new Account());
            checAccounts.Add(new Account("Larry2"));
            checAccounts.Add(new Account("Moe2", 2000));
            checAccounts.Add(new Account("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            Console.WriteLine("end Checking===============================");


            // Trust
            var trustAccounts = new List<Account>();
            Console.WriteLine("start trust ***********************************************************************************************");
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new SavingsAccount("Superman2"));
            trustAccounts.Add(new SavingsAccount("Batman2", 2000));
            trustAccounts.Add(new SavingsAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);
            Console.WriteLine("end trust ***********************************************************************************************");



            Console.WriteLine();

        }
    }
}
