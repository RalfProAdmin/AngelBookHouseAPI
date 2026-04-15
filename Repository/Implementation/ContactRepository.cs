using EkartAPI.Data;
using EkartAPI.Models;
using EkartAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EkartAPI.Repository.Implementation
{
    public class ContactRepository: IContactRepository
    {
        private readonly EkartDBcontext _context;
        public ContactRepository(EkartDBcontext context)
        {
            _context = context;
        }

        public async Task<ContactInfoDto> GetContactInfoAsync()
        {
            var setting = await _context.tbl_ContactSettings.FirstOrDefaultAsync();
            var emails = await _context.tbl_ContactInfo.Where(c => c.ContactType == "email").Select(c => c.Value)
                .ToListAsync();

            var mobiles = await _context.tbl_ContactInfo.Where(c => c.ContactType == "mobile").Select(c => c.Value)
                .ToListAsync();

            return new ContactInfoDto
            {
                EnquiryMessage = setting?.EnquiryMessage ?? "",
                Emails = emails,
                MobileNumbers = mobiles
            };
        }

        public async Task UpdateContactInfoAsync(ContactInfoDto contactInfoDto)
        {
            var existingSetting = await _context.tbl_ContactSettings.FirstOrDefaultAsync();

            if(existingSetting == null)
            {
                existingSetting = new ContactSetting
                {
                    EnquiryMessage = contactInfoDto.EnquiryMessage,
                    CreatedAt = DateTime.Now
                };

                _context.tbl_ContactSettings.Add(existingSetting);
            }
            else
            {
                existingSetting.EnquiryMessage = contactInfoDto.EnquiryMessage;
                existingSetting.UpdatedAt = DateTime.Now;
            }

            //remove all previous contact infos
            var oldInfos = _context.tbl_ContactInfo.ToList();
            _context.tbl_ContactInfo.RemoveRange(oldInfos);
            // Add new emails
            foreach (var email in contactInfoDto.Emails)
            {
                _context.tbl_ContactInfo.Add(new ContactInfo
                {
                    ContactType = "email",
                    Value = email,
                    CreatedAt = DateTime.Now
                });
            }

            // Add new mobiles
            foreach (var mobile in contactInfoDto.MobileNumbers)
            {
                _context.tbl_ContactInfo.Add(new ContactInfo
                {
                    ContactType = "mobile",
                    Value = mobile,
                    CreatedAt = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task<ContactSetting?> GetLatestEnquiryMessageAsync()
        {
            return await _context.tbl_ContactSettings
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<string>> GetEmailsAsync()
        {
            return await _context.tbl_ContactInfo
                .Where(x => x.ContactType == "email")
                .Select(x => x.Value)
                .ToListAsync();
        }

        public async Task<List<string>> GetMobileNumbersAsync()
        {
            return await _context.tbl_ContactInfo
                .Where(x => x.ContactType == "mobile")
                .Select(x => x.Value)
                .ToListAsync();
        }
    }
}
