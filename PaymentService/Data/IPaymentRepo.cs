using PaymentService.Models;
using System.Collections.Generic;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        bool SaveChanges();

        //platforms
        IEnumerable<Enrollment> GetAllPlatforms();
        void CreateEnrollment(Enrollment enrollment);
        bool EnrollmentExist(int enrollmentid);
        bool ExternalEnrollmentExist(int externalPlatformId);

        //Command
        IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid);
        Payment GetPayment(int enrollmentid, int paymentId);
        void CreatePayment(int enrollmentid, Payment payment);
    }
}
