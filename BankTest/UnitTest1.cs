using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {

        // unit test code  
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        //unit test method  
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            //// act  
            //account.Debit(debitAmount);

            //// assert is handled by ExpectedException  

            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            // an exception should have been thrown, so if we get here, it failed the test
            Assert.Fail("No exception was thrown. Expected ArgumentOutOfRangeException.");
        }


        //unit test method  
        [TestMethod]
        //[ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 10;
            double debitAmount = 100.00;
            BankAccount account = new BankAccount("Mr. Walter White", beginningBalance);

            // act  
            //account.Debit(debitAmount);

            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("No exception was thrown. Expected ArgumentOutOfRangeException.");
        }


        [TestMethod]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // assert
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }
            // an exception should have been thrown, so if we get here, it failed the test
            Assert.Fail("No exception was thrown. Expected ArgumentOutOfRangeException.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Credit_WhenAccountFrozen_ShouldThrowException()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = 20.00;
            BankAccount account = new BankAccount("Sandy Cheeks", beginningBalance);

            account.ToggleFreeze();
            account.Credit(creditAmount);
        }

        [TestMethod]
        public void Credit_WithValidAmounts_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11;
            double creditAmount = 20.00;
            double expected = beginningBalance + creditAmount;
            BankAccount account = new BankAccount("Sandy Cheeks", beginningBalance);

            account.Credit(creditAmount);

            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
    }
}
