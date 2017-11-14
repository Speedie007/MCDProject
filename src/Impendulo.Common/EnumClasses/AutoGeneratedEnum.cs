﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Impendulo.Common.Enum
{

    public enum EnumActivityCategories : int
    {
        Gas_Cutting = 1, Gas_Welding = 2, Thermal_Application = 3, Trade_Related_Skills = 5, Materials = 6, Drawings_and_Sketches = 7, Hand_Skills = 8, Arc_Welding = 9, Safety = 10, Induction = 11, Hand_tools = 12, Workshop_Tools = 13, Marking_Off = 14, Power_Tools = 15, Developments = 17, Structural__Work = 18, Drawings_and_Sketches_Electrical = 19, Conductors = 20, Cables = 21, Soft_solder = 22, AC_Machines = 23, DC_Machines = 24, Installations = 25, Wiring = 26, Fault_Finding = 27, Electrical_Equipment = 28, Electronics = 29, Measuring_Equipment = 30, Marking_Off__Applicable_Trade = 31, Arc_Welding_Operating_Procedures = 32, Materials_Engineering = 33, Electrical_Test_Instruments = 34, Keys_and_Locking_Devices = 36, Bearings = 38, Lubrication = 39, Assemblies = 40, Installation_of_Machinery = 41, Couplings = 42, Drives = 43, Brakes_and_Clutches = 44, Electrical_Measuring_Instruments = 45, Pumps = 47, Hydraulics = 48, Pneumatics = 49, Slinging_Tackle = 50, Ropes = 51, Lifting_Tackle = 52, Erecting_and_Dismantling = 53, Transport_Loads = 54, Scaffolding_and_Staging = 55, Cranes = 56, Electronic_and_Electronic_Test_Instruments = 58, Thermocouples_and_Resistance_Bulbs = 59, Pneumatic_Components_and_Installations = 60, Electronic_Equipment_Quality = 61, Control_Valves = 62, Sensors = 63, Electrical__Electronic_and_Pneumatic = 64, Instrument_Maintenance = 65, Micro_Processor = 66, Basic_Rigging = 67, Centre_Lathe_Work = 68, Milling_Machine_Work = 69, Machines = 70
    }
    public enum EnumAddressTypes : int
    {
        Billing_Invoice = 3008, Head_Main_Office = 3005, Postal = 1, Remote_Site = 3004, Residential = 2, Stores = 3003, Training_Site = 3007, Workshop = 3006
    }
    public enum EnumAssessmentRecommendations : int
    {
        Competent = 1, Course_InComplete__Reschedule = 2, Not_Yet_Competent = 3
    }
    public enum EnumContactTypes : int
    {
        Cell_Number = 2, Email_Address = 1, Fax_Number = 2007, Home_Number = 3, Office_Number = 4
    }
    public enum EnumCountries : int
    {
        South_Africa = 1, Lesotho = 2, Mozambique = 3, Zimbabwe = 4, Namibia = 5
    }
    public enum EnumDayOfWeeks : int
    {
        Monday = 1, Tuesday = 2, Wednesday = 3, Thursday = 4, Friday = 5, Saturday = 6, Sunday = 7
    }
    public enum EnumDepartments : int
    {
        Apprenticeship = 2004, Learnership = 1004, Lifting_Operator_Training_LOPT = 1005, Short_Courses = 2005, Surface_Mobile_Equipment = 1006, Training_Related_Services = 2009
    }
    public enum EnumDisabilities : int
    {
        CognitiveORLearning = 5, Hearing = 4, Mobility_Impairments = 1, Other = 7, Physical_Impairments = 2, Vision = 3
    }
    public enum EnumEmailMessageRepositoryHistoryTrancationTypes : int
    {
        NewMessage = 1, MessageSent = 2
    }
    public enum EnumEnquiryStatuses : int
    {
        New = 1, Enrollment_In_Progress = 2, Awaiting_Scheduling = 3, Schedule_Finalised = 4, Enquiry_Closed = 5
    }
    public enum EnumEnquiryTypes : int
    {
        Company = 1, Student = 2
    }
    public enum EnumEnrollentDocumentTypes : int
    {
        Enrollment_Form = 1, ID_Documents = 2, Student_Contract_Form = 5, Facilitator_Report = 17, Scheduel_Confirmation_Documents = 20
    }
    public enum EnumEnrollmentProgressStates : int
    {
        In_Progress = 1, Complete = 2, Excempt = 1002, New_Enrollment = 2002, ReSchedule = 3002
    }
    public enum EnumEnrollmentTypes : int
    {
        Novice = 1, ReCertification = 2
    }
    public enum EnumEquiryHistoryTypes : int
    {
        Company_Contact_Selection = 1, Curriculum_Selection = 2, Equiry_Responded_And_Accepted = 3, Equiry_Responded_And_Closed = 4, Enquiry_Initial_Consultation_Completed = 1003, Enquiry_Initial_Documentation_Sent = 1004, Enquiry_Custom_Email_Message_Sent = 1005, Enrollment_Course_PreRequisite_Was_Except = 1006, Enrollment_Custom_Email_Message_Sent = 1007, Enquiry_Contact_Details_Changed_To_Private = 2006, Enquiry_Contact_Details_Changed_To_Company = 2007, Enquiry_Contact_Person_Changed = 2008, Curriculum_Enquiry_Item_Closed = 2009, Curriculum_Enquiry_Item_Reinstated = 2010, Enrollment_Student_Successfully_Enrolled = 2011
    }
    public enum EnumEquiryOrigions : int
    {
        Internet_Advert = 1, Facebook = 2, Referal = 3, Twitter = 4, Existing_Customer = 5, Flyer_Advert = 6, Consultant_Referal = 7, UnKnown = 8
    }
    public enum EnumEthnicities : int
    {
        Black_African = 1, White = 2, Coloured = 3, Indian = 4, Asian = 5, Other_Unspecified = 6
    }
    public enum EnumGenders : int
    {
        Male = 1, Female = 2
    }
    public enum EnumMartialStatuses : int
    {
        Married = 1, Widowed = 2, Separated = 3, Divorced = 4, Single = 5
    }
    public enum EnumPracticalAssessmentStatuses : int
    {
        Competent = 1, Not_Yet_Competent = 2, Not_attempted = 3, No_practical = 4
    }
    public enum EnumProcesses : int
    {
        Enquiry = 1, Enrollment = 2
    }
    public enum EnumProcessSteps : int
    {
        An_Enquiry_Capture_Completed = 1, Enquiry__Apprenticeship__Step_1__Documentation_To_Send = 3
    }
    public enum EnumProgessFileTypes : int
    {
        Student = 1, Company = 2
    }
    public enum EnumProvinces : int
    {
        Gauteng = 1, Free_State = 2, Western_Cape = 3, Northern_Cape = 4, Eastern_Cape = 5, Limpopo = 6, KwaZuluNatal = 7, Mpumalanga = 8, North_West = 9
    }
    public enum EnumQualificationLevels : int
    {
        NQF_1_Grade_9_National_Certificate = 1, NQF_2_National_Certificate = 2, NQF_3_National_Certificate = 3, NQF_4_Grade_12_Matric = 4, NQF_5_National_Diplomas = 5, NQF_6_National_Diplomas = 6, NQF_7_Bachelors_Degree, _B_Techs, _Advanced_Diploma = 7, NQF_8_Honours_Degree_Postgraduate_Diploma = 8, NQF_9_Masters_Degree = 9, NQF_10_Doctrate = 10, Unknown_or_To_be_Indicated = 11
    }
    public enum EnumScheduleConfirmationDocumentationTypes : int
    {
        Proforma_Invoice = 1, Quotation = 2, Proof_Of_Payment = 3
    }
    public enum EnumScheduleLocations : int
    {
        Onsite = 1, OffSite = 2
    }
    public enum EnumScheduleStatuses : int
    {
        Reserved = 1, Confirmed = 2
    }
    public enum EnumSectionalEnrollmentTypes : int
    {
        Section_13 = 1, Section_28 = 2
    }
    public enum EnumSetas : int
    {
        Construction_Education_and_Training_Authority = 2, Transport_Education_Training_Authority = 3, Mining_Qualifications_Authority = 4
    }
    public enum EnumTheoriticalAssesmentStatuses : int
    {
        Competent = 1, Not_Yet_Competent = 2, Not_attempted = 3, No_Theory = 4
    }
    public enum EnumTitles : int
    {
        Mr = 1, Mrs = 2, Miss = 3, Dr = 4, Prof = 5
    }
    public enum EnumTypeOfRelations : int
    {
        Father = 1, Mother = 2, Brother = 3, Sister = 4, Uncle = 5, Aunt = 6, Friend = 7, Husband = 8, Wife = 9, Girl_Friend = 10, Boy_Friend = 11
    }
}



