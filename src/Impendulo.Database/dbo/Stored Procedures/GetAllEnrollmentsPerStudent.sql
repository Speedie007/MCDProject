

CREATE PROCEDURE [dbo].[GetAllEnrollmentsPerStudent]
--DROP PROCEDURE GetAllEnrollmentsPerStudent
	@StudentProgressFileID INT
AS
SELECT        
LookupDepartments.DepartmentName, 
Curriculums.CurriculumName, 
Courses.CourseName, 
COUNT(1) AS AmountEnrolled, 
ISNULL(CONVERT(VARCHAR(25), Schedules.ScheduleStartDate), 'Not Yet Secheduled') AS ScheduleStartDate, 
ISNULL(CONVERT(VARCHAR(25), Schedules.ScheduleCompletionDate), 'Not Yet Secheduled') AS ScheduleCompletionDate, 
ISNULL(CONVERT(VARCHAR(25), LookupScheduleLocations.ScheduleLocation), 'Not Yet Determined') AS CourseLocation, 
LookupEnrollmentProgressStates.EnrollmentProgressCurrentState,
CurriculumCourseEnrollments.CurriculumCourseEnrollmentID

FROM            LookupScheduleLocations 
	INNER JOIN Schedules 
		ON LookupScheduleLocations.LookupScheduleLocationID = Schedules.LookupScheduleLocationID 
	RIGHT OUTER JOIN LookupEnrollmentProgressStates 
	INNER JOIN LookupDepartments 
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
		ON LookupEnrollmentProgressStates.LookupEnrollmentProgressStateID = CurriculumCourseEnrollments.LookupEnrollmentProgressStateID 
		ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID 
	LEFT OUTER JOIN StudentProgressFiles 
	INNER JOIN ProgressFiles 
		ON StudentProgressFiles.StudentProgressFileID = ProgressFiles.ProgressFileID 
		ON Enrollments.ProgressFileID = ProgressFiles.ProgressFileID
GROUP BY 
		Curriculums.CurriculumName, 
		Courses.CourseName, 
		Schedules.ScheduleStartDate, 
		Schedules.ScheduleCompletionDate, 
		LookupScheduleLocations.ScheduleLocation, 
		LookupDepartments.DepartmentName, 
		LookupEnrollmentProgressStates.EnrollmentProgressCurrentState, 
		StudentProgressFiles.StudentProgressFileID, 
		CurriculumCourseEnrollments.CurriculumCourseEnrollmentID
HAVING        
		(StudentProgressFiles.StudentProgressFileID = @StudentProgressFileID)
ORDER BY 
			Schedules.ScheduleStartDate DESC, 
			ScheduleStartDate, 
			LookupDepartments.DepartmentName, 
			Curriculums.CurriculumName, 
			Courses.CourseName