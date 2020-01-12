using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Domain.Entities
{
    public class Discount
    {
        public Discount(decimal amount, DateTime expirateDate)
        {
            Amount = amount;
            ExpirateDate = expirateDate;
        }
        public decimal Amount { get; private set; }
        public DateTime ExpirateDate { get; private set; }

        public bool IsValid ()
        {
            return DateTime.Compare(DateTime.Now,ExpirateDate) < 0;
        }
        public decimal Value()
        {
            if (IsValid())
                return Amount;
            else
                return 0;
        }

    }

    
}
