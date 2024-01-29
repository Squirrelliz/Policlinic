using db_7_2.Data;
using db_7_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace db_7_2.Controllers
{
    public class PoliclinicController : Controller
    {
        private readonly PoliclinicContext _context;

        public PoliclinicController(PoliclinicContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Doctors()
        {
            return View(await _context.Doctors.ToListAsync());
        }


        [HttpGet]
        public async Task<IActionResult> Patients()
        {
            return View(await _context.Patients.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Diagnoses()
        {
            return View(await _context.Diagnoses.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> MedicalServices()
        {
            return View(await _context.MedicalServices.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> MedicalServicesReport()
        {
            IQueryable<MedicalServiceReportDTO> query = from HistoryOfMedicalService in _context.
            HistoryOfMedicalServices
            join MedicalService in _context.MedicalServices
            on HistoryOfMedicalService . MedicalServiceId equals MedicalService
            .Id
            join Doctor in _context.Doctors
            on HistoryOfMedicalService.DoctorId equals Doctor.Id
            join Qualification in _context.Qualifications
            on Doctor . QualificationId equals Qualification.Id
            join Patient in _context.Patients
            on HistoryOfMedicalService.ElectronicCardId equals Patient.
            ElectronicCardId
            where HistoryOfMedicalService.Status == StatusOfMedicalService.
            Done
            orderby MedicalService .Name , Doctor.Fcs , Patient.Fcs ,
            HistoryOfMedicalService.DateTime ascending
            select new MedicalServiceReportDTO
            {
            ServiceName = MedicalService.Name ,
            Result = HistoryOfMedicalService.Result ,
            PatientFcs = Patient.Fcs ,
            DateOfBirth = Patient.DateOfBirth ,
            DateTime = HistoryOfMedicalService.DateTime ,
            Specialization = Qualification.Specialization ,
            DoctorFcs = Doctor.Fcs
            };
            
            return View(await query.ToListAsync());
        }

        
        

        [HttpPost]
        public async Task<IActionResult> StatisticsOfTreatedCases(DateTime StartPeriodDate = new DateTime(), DateTime EndPeriodDate=new DateTime())
        {
            TempData["dateFrom"] = DateOnly.FromDateTime(StartPeriodDate).ToString();
            TempData["dateTo"] = DateOnly.FromDateTime(EndPeriodDate).ToString();
            var countCaseOfTheDisease = from HistoryOfDiagnose in _context.HistoryOfDiagnoses
                                        where HistoryOfDiagnose.DateOfDetection > DateOnly.FromDateTime(StartPeriodDate)
                                        && HistoryOfDiagnose.DateOfDetection < DateOnly.FromDateTime(EndPeriodDate)
                                        group HistoryOfDiagnose by HistoryOfDiagnose.DiagnoseId into g
                                        select new
                                        {
                                            DiagnoseId = g.Key,
                                            Count = g.Count()
                                        };

            IQueryable<StatisticsOfTreatedCasesDTO> query1 = from Diagnose in _context.Diagnoses
                         join Count in countCaseOfTheDisease on Diagnose.Id equals Count.DiagnoseId
                         select new StatisticsOfTreatedCasesDTO
                         {
                             DiagnoseName = Diagnose.Name,
                             NumberOfCases = Count.Count,
                          
                         };

            
            return View(await query1.ToListAsync());
        }


        [HttpGet]
        public IActionResult ChooseDataStatisticDiagnoses()
        {

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> HospitalIncomeStatistics(DateTime StartPeriodDate = new DateTime(), DateTime EndPeriodDate = new DateTime())
        {
            TempData["dateFrom"] = DateOnly.FromDateTime(StartPeriodDate).ToString();
            TempData["dateTo"] = DateOnly.FromDateTime(EndPeriodDate).ToString();
            var countSumOfMoney = from HistoryOfMedicalService in
_context.HistoryOfMedicalServices
                                  where HistoryOfMedicalService.DateTime > StartPeriodDate
                                  && HistoryOfMedicalService.DateTime < EndPeriodDate
                                  group HistoryOfMedicalService by HistoryOfMedicalService.
                                 MedicalServiceId into g
                                  select new
                                  {
                                      MedicalServiceId = g.Key,
                                      Count = g.Count()
                                  };

            var query1 = from MedicalService in _context.
            MedicalServices
                         join Count in countSumOfMoney on MedicalService.Id equals Count.
                            MedicalServiceId
                         where MedicalService.Price > 0
                         select new HospitalIncomeStatisticsDTO
                         {
                             MedicalServiceName= MedicalService.Name,
                             Income = Count.Count * MedicalService.Price
                         };

            return View(await query1.ToListAsync());
        }


        [HttpGet]
        public IActionResult ChooseDataStatisticHospitalIncome()
        {

            return View();
        }
    }
}
