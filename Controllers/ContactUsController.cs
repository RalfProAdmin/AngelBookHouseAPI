using EkartAPI.Models;
using EkartAPI.Repository.Implementation;
using EkartAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EkartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly IEmailInterface _emailrepository;

        public ContactUsController(IContactUsRepository contactUsRepository, IEmailInterface emailrepository)
        {

            _contactUsRepository = contactUsRepository ?? throw new ArgumentNullException(nameof(contactUsRepository));
            _emailrepository = emailrepository;
        }

        [HttpPost]
        public async Task<IActionResult> PostContactUsDetail(ContactDetails Request)
        {
            var ContactUsDetails = new ContactDetails
            {
                Name = Request.Name,
                Email = Request.Email,
                Phone = Request.Phone,
                Subject = Request.Subject,
                Message = Request.Message,
            };

            await _contactUsRepository.AddContactDetails(ContactUsDetails);

            var response = new ContactUsModel
            {
                Name = ContactUsDetails.Name,
                Email = ContactUsDetails.Email,
                Phone = ContactUsDetails.Phone,
                Subject = ContactUsDetails.Subject,
                Message = ContactUsDetails.Message
            };

            // --- Send Emails ---
            string superAdminEmail = "info@angelbookhouse.in"; // super admin
            string superAdminSubject = "📩 New Contact Us Request";
            string superAdminBody = $@"
        New Contact Us request received:

        Name: {ContactUsDetails.Name}
        Email: {ContactUsDetails.Email}
        Phone: {ContactUsDetails.Phone}
        Subject: {ContactUsDetails.Subject}
        Message: {ContactUsDetails.Message}
    ";

            await _emailrepository.SendEmailAsync(
                superAdminEmail,
                superAdminSubject,
                superAdminBody,
                ContactUsDetails.Name,
                ContactUsDetails.Email
            );

            string userSubject = "✅ Thank you for contacting Angel Book House";
            string userBody = $@"
        Hi {ContactUsDetails.Name},

        Thank you for reaching out to us. 
        We have received your message and will get back to you soon.

        Your submitted details:
        - Subject: {ContactUsDetails.Subject}
        - Message: {ContactUsDetails.Message}

        Regards,  
        Angel Book House Team
    ";

            await _emailrepository.SendEmailAsync(
                ContactUsDetails.Email,
                userSubject,
                userBody,
                "Angel Book House",
                superAdminEmail
            );

            return Ok(response);
        }

    }
}
