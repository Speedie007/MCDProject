

CREATE PROCEDURE [dbo].[GetAllEnrollmentsCompanyAndStudent]
--DROP PROCEDURE GetAllEnrollmentsCompanyAndStudent
	
AS
SELECT       
LookupDepartments.DepartmentName,
	Curriculums.CurriculumName, 
	Courses.CourseName, 
	COUNT(1) AS AmountEnrolled, 
	ISNULL(CONVERT(VARCHAR(25),Schedules.ScheduleStartDate),'Not Yet Secheduled') AS ScheduleStartDate, 
	ISNULL(CONVERT(VARCHAR(25),Schedules.ScheduleCompletionDate),'Not Yet Secheduled') AS ScheduleCompletionDate, 
	ISNULL(CONVERT(VARCHAR(25),LookupScheduleLocations.ScheduleLocation),'Not Yet Determined') AS CourseLocation, 
	ISNULL(CONVERT(VARCHAR(50),Companies.CompanyName),'Private Student') AS Client
	
FROM LookupScheduleLocations 
		INNER JOIN Schedules 
			ON LookupScheduleLocations.LookupScheduleLocationID = Schedules.LookupScheduleLocationID 
		RIGHT OUTER JOIN LookupDepartments 
		INNER JOIN Enrollments 
		INNER JOIN CurriculumCourseEnrollments 
			ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID 
		INNER JOIN CurriculumCourses 
			ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID 
		INNER JOIN Curriculums 
			ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID 
		INNER JOIN Courses 
			ON CurriculumCourses.CourseID = Courses.CourseID 
			ON LookupDepartments.DepartmentID = Curriculums.DepartmentID 
			ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID 
		LEFT OUTER JOIN StudentProgressFiles 
		INNER JOIN ProgressFiles 
			ON StudentProgressFiles.StudentProgressFileID = ProgressFiles.ProgressFileID 
		INNER JOIN CompanyStudentProgressFiles 
			ON StudentProgressFiles.StudentProgressFileID = CompanyStudentProgressFiles.StudentProgressFileID 
		INNER JOIN CompanyProgressFiles 
			ON CompanyStudentProgressFiles.CompanyProgressFileID = CompanyProgressFiles.CompanyProgressFileID 
		INNER JOIN Companies 
			ON CompanyProgressFiles.CompanyID = Companies.CompanyID 
			ON Enrollments.ProgressFileID = ProgressFiles.ProgressFileID
GROUP BY	Curriculums.CurriculumName, 
			Courses.CourseName, 
			Schedules.ScheduleStartDate, 
			Schedules.ScheduleCompletionDate, 
			LookupScheduleLocations.ScheduleLocation, 
			Companies.CompanyName, 
			LookupDepartments.DepartmentName
ORDER BY 
			LookupDepartments.DepartmentName, 
			Curriculums.CurriculumName, 
			Courses.CourseName