using PaymentService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentService.Data
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateEnrollment(Enrollment enrollment)
        {
            if (enrollment == null) 
            {
                throw new ArgumentNullException(nameof(enrollment));
            }
                
            _context.Enrollments.Add(enrollment);
        }

        public void CreatePayment(int enrollmentid, Payment payment)
        {
            if (payment == null) 
            {
                throw new ArgumentNullException(nameof(payment));
            }
                
            payment.EnrollmentId = enrollmentid;
            _context.Payments.Add(payment);
        }

        public bool EnrollmentExist(int enrollmentid)
        {
            return _context.Enrollments.Any(p => p.EnrollmentID == enrollmentid);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _context.Enrollments.ToList();
        }

        public Payment GetPayment(int enrollmentid, int paymentId)
        {
            return _context.Payments
                .Where(p => p.EnrollmentId == enrollmentid && p.PaymentID == paymentId)
                .FirstOrDefault();
        }

        public IEnumerable<Payment> GetPaymentsForEnrollment(int enrollmentid)
        {
            return _context.Payments
                .Where(p => p.EnrollmentId == enrollmentid)
                .OrderBy(p => p.Enrollment.EnrollmentID);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
