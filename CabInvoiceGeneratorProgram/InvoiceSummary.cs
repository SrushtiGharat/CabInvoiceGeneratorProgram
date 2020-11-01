using System;
using System.Collections.Generic;
using System.Text;

namespace CabInvoiceGeneratorProgram
{
    public class InvoiceSummary
    {
        InvoiceData data;

        /// <summary>
        /// Get invoice summary 
        /// </summary>
        /// <param name="noOfRides"></param>
        /// <param name="totalFare"></param>
        /// <returns></returns>
        public InvoiceData GetInvoice(int noOfRides, double totalFare)
        {
            double averageFare = totalFare / noOfRides;
            data = new InvoiceData(noOfRides, totalFare, averageFare);

            return data;
        }

    }
}
