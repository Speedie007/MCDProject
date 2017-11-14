

CREATE PROCEDURE [dbo].[GetAllEnrollmentsPerCompany]
--DROP PROCEDURE GetAllEnrollmentsPerCompany 1
	@CompanyProgressFileID INT
AS
SELECT        
LookupDepartments.DepartmentName, 
Curriculums.CurriculumName, Courses.CourseName, 
COUNT(1) AS AmountEnrolled, 
ISNULL(CONVERT(VARCHAR(25), Schedules.ScheduleStartDate), 
LookupEnrollmentProgressStates.EnrollmentProgressCurrentState) AS ScheduleStartDate, 
ISNULL(CONVERT(VARCHAR(25), Schedules.ScheduleCompletionDate),LookupEnrollmentProgressStates.EnrollmentProgressCurrentState) AS ScheduleCompletionDate, 
ISNULL(CONVERT(VARCHAR(25), LookupScheduleLocations.ScheduleLocation), 'Not Yet Determined') AS CourseLocation, 
Curriculums.CurriculumID, CurriculumCourseEnrollments.LookupEnrollmentProgressStateID,

 CASE 
 WHEN  Schedules.LookupScheduleLocationID = 1 THEN Venues.VenueName
 WHEN  Schedules.LookupScheduleLocationID = 2 THEN Addresses.AddressDescription
            ELSE 'Not Yet Determined' END AS VenueName

FROM            
					 LookupEnrollmentProgressStates INNER JOIN
                         LookupDepartments INNER JOIN
                         Enrollments INNER JOIN
                         CurriculumCourseEnrollments ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID INNER JOIN
                         Courses ON CurriculumCourses.CourseID = Courses.CourseID ON LookupDepartments.DepartmentID = Curriculums.DepartmentID ON 
                         LookupEnrollmentProgressStates.LookupEnrollmentProgressStateID = CurriculumCourseEnrollments.LookupEnrollmentProgressStateID LEFT OUTER JOIN
                         OffSiteSchedule INNER JOIN
                         Addresses ON OffSiteSchedule.AddressID = Addresses.AddressID RIGHT OUTER JOIN
                         LookupScheduleLocations INNER JOIN
                         Schedules ON LookupScheduleLocations.LookupScheduleLocationID = Schedules.LookupScheduleLocationID ON OffSiteSchedule.ScheduleID = Schedules.ScheduleID LEFT OUTER JOIN
                         OnSiteSchedules INNER JOIN
                         Venues ON OnSiteSchedules.VenueID = Venues.VenueID ON Schedules.ScheduleID = OnSiteSchedules.ScheduleID ON 
                         CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID LEFT OUTER JOIN
                         StudentProgressFiles INNER JOIN
                         ProgressFiles ON StudentProgressFiles.StudentProgressFileID = ProgressFiles.ProgressFileID INNER JOIN
                         CompanyStudentProgressFiles ON StudentProgressFiles.StudentProgressFileID = CompanyStudentProgressFiles.StudentProgressFileID INNER JOIN
                         CompanyProgressFiles ON CompanyStudentProgressFiles.CompanyProgressFileID = CompanyProgressFiles.CompanyProgressFileID ON Enrollments.ProgressFileID = ProgressFiles.ProgressFileID

GROUP BY Curriculums.CurriculumName, Courses.CourseName, Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, LookupScheduleLocations.ScheduleLocation, LookupDepartments.DepartmentName, 
                         CompanyStudentProgressFiles.CompanyProgressFileID, Curriculums.CurriculumID, LookupEnrollmentProgressStates.EnrollmentProgressCurrentState, CurriculumCourseEnrollments.LookupEnrollmentProgressStateID, 
                         Venues.VenueName, Addresses.AddressDescription, Schedules.LookupScheduleLocationID

HAVING        
			(CompanyStudentProgressFiles.CompanyProgressFileID = @CompanyProgressFileID)
ORDER BY	
			Schedules.ScheduleStartDate DESC, 
			LookupDepartments.DepartmentName, 
			Curriculums.CurriculumName, 
			Courses.CourseName