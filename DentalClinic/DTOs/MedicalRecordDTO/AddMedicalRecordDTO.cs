﻿using DentalClinic.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DentalClinic.DTOs.MedicalRecordDTO
{
    public class AddMedicalRecordDTO
    {
        public int PatientIdNo { get; set; }
        public int TreatedByID { get; set; } 

        public string LabTests { get; set; } = string.Empty;

        public string PrescribedMedicines { get; set; } = string.Empty;

        public string TreatmentDetails { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        public string ReferalsList { get; set; } = string.Empty;

        public string ReferedDoctor { get; set; } = string.Empty;

        public int[]? ProceduresIDs { get; set; }



    }
}
