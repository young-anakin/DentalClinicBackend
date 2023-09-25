﻿namespace DentalClinic.DTOs.SettingsDTO
{
    public class UpdateCompanySettingDTO
    {
        public int CompanySettingIDs { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;

        public int LoanExpireAfter { get; set; }

        public int CardExpireAfter { get; set; }

        public int EarlyReminderDate { get; set; }

        public decimal MaximumLoanAmount { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
