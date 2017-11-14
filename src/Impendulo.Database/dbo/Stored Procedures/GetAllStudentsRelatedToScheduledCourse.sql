Create Procedure GetAllStudentsRelatedToScheduledCourse
--DROP Procedure  GetAllStudentsRelatedToScheduledCourse
@startDate date,
@EndDate date,
@FaciltatorID int

as

SELECT        
Students.StudentIDNumber AS IDNumber, 
Individuals.IndividualFirstName + ' ' + 
Individuals.IndividualSecondName + ' ' + 
Individuals.IndividualLastname AS StudentName, 
ISNULL(Companies.CompanyName, 'Private') AS CompanyName, 
LookupDepartments.DepartmentName, 
Curriculums.CurriculumName, 
Courses.CourseName, 
Schedules.ScheduleStartDate AS StartDate, 
Schedules.ScheduleCompletionDate AS EndDate, 
LookupScheduleStatuses.ScheduleStatus, 
Schedules.IndividualID AS FacilitatorID, 
CurriculumCourseEnrollments.CurriculumCourseEnrollmentID, 
CurriculumCourseEnrollments.EnrollmentID, 
Enrollments.EnquiryID, 
Enrollments.ProgressFileID, 
CurriculumCourseEnrollments.CurriculumCourseID
FROM            
		CurriculumCourseEnrollments INNER JOIN
                         Individuals INNER JOIN
                         StudentEnrollments INNER JOIN
                         Enrollments ON StudentEnrollments.EnrollmentID = Enrollments.EnrollmentID INNER JOIN
                         Students INNER JOIN
                         StudentProgressFiles ON Students.IndividualID = StudentProgressFiles.StudentID ON StudentEnrollments.IndividualID = Students.IndividualID ON Individuals.IndividualID = Students.IndividualID ON 
                         CurriculumCourseEnrollments.EnrollmentID = Enrollments.EnrollmentID INNER JOIN
                         Schedules ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         LookupDepartments INNER JOIN
                         Courses ON LookupDepartments.DepartmentID = Courses.DepartmentID ON CurriculumCourses.CourseID = Courses.CourseID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID INNER JOIN
                         LookupScheduleStatuses ON Schedules.ScheduleStatusID = LookupScheduleStatuses.ScheduleStatusID LEFT OUTER JOIN
                         Companies INNER JOIN
                         CompanyStudentProgressFiles INNER JOIN
                         CompanyProgressFiles ON CompanyStudentProgressFiles.CompanyProgressFileID = CompanyProgressFiles.CompanyProgressFileID ON Companies.CompanyID = CompanyProgressFiles.CompanyID ON 
                         StudentProgressFiles.StudentProgressFileID = CompanyStudentProgressFiles.StudentProgressFileID
WHERE       
 (Schedules.ScheduleStartDate = CONVERT(DATETIME, @startDate, 102)) 
 AND 
 (Schedules.ScheduleCompletionDate = CONVERT(DATETIME, @EndDate, 102)) 
 AND (Schedules.IndividualID = @FaciltatorID);