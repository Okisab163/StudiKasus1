using PaymentService.Models;
using System.Collections.Generic;

namespace PaymentService.Data
{
    public interface IPaymentRepo
    {
        bool SaveChanges();

        //Enrollments
        IEnumerable<Enrollment> GetAllEnrollments();
        void CreateEnrollment(Enrollment enrollment);
        bool EnrollmentExist(int enrollmentid);

        //Payments
        IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid);
        Payment GetPayment(int enrollmentid, int paymentId);
        void CreatePayment(int enrollmentid, Payment payment);
    }
}
