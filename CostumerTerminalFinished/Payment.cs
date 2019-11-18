using System;
using System.Collections.Generic;
using System.Text;

namespace BeställningsTerminal
{
    public static class Payment
    {
        public static bool Pay()
        {
            bool paymentApproved = true;

            Random payment = new Random();
            int checkPayment = payment.Next(1, 10);
            if (checkPayment == 8)
            {
                paymentApproved = false;
            }

            return paymentApproved;
        }

    }
}
