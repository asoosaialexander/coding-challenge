﻿namespace LoanWebApi.Models
{
    public class LoanModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int AccountNo { get; set; }
        public int Balance { get; set; }
        public int Interest { get; set; }
        public int EarlyPaymentFee { get; set; }
        public int Carryover { get; set; }
    }
}