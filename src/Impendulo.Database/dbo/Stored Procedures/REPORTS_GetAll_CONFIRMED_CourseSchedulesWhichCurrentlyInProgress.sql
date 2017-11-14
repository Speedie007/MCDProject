﻿CREATE PROCEDURE [dbo].[REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress]
--DROP PROCEDURE [REPORTS_GetAll_CONFIRMED_CourseSchedulesWhichCurrentlyInProgress]
--GetAllCourseSchedulesWhichCurrentlyInProgress
AS
SELECT      
	COUNT(1)											AS AmountEnrolled,   
	Schedules.ScheduleStartDate							AS StartDate, 
	Schedules.ScheduleCompletionDate					AS EndDate, 
	--LookupScheduleStatuses.ScheduleStatus				AS [Status], 
	LookupDepartments.DepartmentName					AS DepartmentName,
	Curriculums.CurriculumName							AS CurriculumName, 
	Courses.CourseName									AS CourseName, 
	CurriculumCourses.CostCode							AS CostCode,
	LookupTitles.Title + ' ' + 
	Individuals.IndividualFirstName + ' ' + 
	Individuals.IndividualSecondName + ' ' + 
	Individuals.IndividualLastname						AS FacilitatorName, 
	CASE Schedules.LookupScheduleLocationID 
	WHEN 1 THEN Venues.VenueName 
	ELSE Addresses.AddressDescription 
	END													AS VenueName, 
	Schedules.IndividualID								AS FacilitatorID, 
	CASE Schedules.LookupScheduleLocationID 
	WHEN 1 THEN OnSiteSchedules.VenueID 
	ELSE OffSiteSchedule.AddressID 
	END													AS VenueID, 
	CurriculumCourseEnrollments.CurriculumCourseID		AS CurriculumCourseID, 
	Courses.CourseID									as CourseID, 
	LookupDepartments.DepartmentID						as DepartmentID,
	Schedules.LookupScheduleLocationID					AS LookupScheduleLocationID
FROM           
	  Venues INNER JOIN
                         OnSiteSchedules ON Venues.VenueID = OnSiteSchedules.VenueID RIGHT OUTER JOIN
                         Schedules INNER JOIN
                         CurriculumCourseEnrollments ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID INNER JOIN
                         LookupScheduleStatuses ON Schedules.ScheduleStatusID = LookupScheduleStatuses.ScheduleStatusID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         Courses ON CurriculumCourses.CourseID = Courses.CourseID INNER JOIN
                         Individuals ON Schedules.IndividualID = Individuals.IndividualID INNER JOIN
                         LookupTitles ON Individuals.TitleID = LookupTitles.TitleID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID INNER JOIN
                         LookupDepartments ON Courses.DepartmentID = LookupDepartments.DepartmentID ON OnSiteSchedules.ScheduleID = Schedules.ScheduleID LEFT OUTER JOIN
                         Addresses INNER JOIN
                         OffSiteSchedule ON Addresses.AddressID = OffSiteSchedule.AddressID ON Schedules.ScheduleID = OffSiteSchedule.ScheduleID
WHERE        (Schedules.ScheduleStatusID = 2) AND (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))
GROUP BY Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, LookupDepartments.DepartmentName, Curriculums.CurriculumName, Courses.CourseName, 
                         LookupTitles.Title + ' ' + Individuals.IndividualFirstName + ' ' + Individuals.IndividualSecondName + ' ' + Individuals.IndividualLastname, Schedules.IndividualID, CurriculumCourseEnrollments.CurriculumCourseID, 
                         Courses.CourseID, LookupDepartments.DepartmentID, Schedules.LookupScheduleLocationID, Addresses.AddressDescription, Venues.VenueName, OffSiteSchedule.AddressID, OnSiteSchedules.VenueID,CurriculumCourses.CostCode
ORDER BY StartDate, LookupDepartments.DepartmentName