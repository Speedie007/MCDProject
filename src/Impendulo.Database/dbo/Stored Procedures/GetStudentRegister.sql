

CREATE PROCEDURE [dbo].[GetStudentRegister]
--DROP PROCEDURE GetStudentRegister
	@StartDate Datetime,
	@EndDate Datetime
AS
	SELECT        
			ISNULL(Companies.CompanyName, 'Private Student')						AS Company,
			LookupTitles.Title 
			+ ' ' +Individuals.IndividualFirstName 
			+ ' ' +Individuals.IndividualSecondName + 
			' ' +Individuals.IndividualLastname			as [StudentName], 
			Students.StudentIDNumber					AS IDNumber, 
			--Students.StudentPassportNumber				AS PassPortNumber, 
			LookupDepartments.DepartmentName			AS Department, 
			Curriculums.CurriculumName					AS CurriculumName,
			Courses.CourseName							AS Course, 
			Schedules.ScheduleStartDate					AS StartDate, 
			Schedules.ScheduleCompletionDate			AS EndDate, 
			CurriculumCourses.CostCode					AS CostCode, 
			CurriculumCourses.Duration					AS Duration,
			CASE LookupScheduleStatuses.ScheduleStatus
			WHEN 'Reserved' THEN 'N'
			ELSE 'Y'
			END											AS Confirmed
FROM             CompanyProgressFiles INNER JOIN
                         CompanyStudentProgressFiles ON CompanyProgressFiles.CompanyProgressFileID = CompanyStudentProgressFiles.CompanyProgressFileID INNER JOIN
                         Companies ON CompanyProgressFiles.CompanyID = Companies.CompanyID RIGHT OUTER JOIN
                         StudentProgressFiles INNER JOIN
                         Students INNER JOIN
                         Individuals ON Students.IndividualID = Individuals.IndividualID INNER JOIN
                         LookupTitles ON Individuals.TitleID = LookupTitles.TitleID ON StudentProgressFiles.StudentID = Students.IndividualID INNER JOIN
                         Schedules INNER JOIN
                         CurriculumCourseEnrollments INNER JOIN
                         Enrollments ON CurriculumCourseEnrollments.EnrollmentID = Enrollments.EnrollmentID ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         Courses ON CurriculumCourses.CourseID = Courses.CourseID INNER JOIN
                         LookupDepartments ON Courses.DepartmentID = LookupDepartments.DepartmentID INNER JOIN
                         LookupScheduleStatuses ON Schedules.ScheduleStatusID = LookupScheduleStatuses.ScheduleStatusID ON StudentProgressFiles.StudentProgressFileID = Enrollments.ProgressFileID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID ON CompanyStudentProgressFiles.StudentProgressFileID = StudentProgressFiles.StudentProgressFileID

						 WHERE        (Schedules.ScheduleStartDate >= CONVERT(DATETIME, @StartDate, 102)) AND (Schedules.ScheduleStartDate <= CONVERT(DATETIME, @EndDate, 102))
ORDER BY Companies.CompanyName, StudentName, Schedules.ScheduleStartDate