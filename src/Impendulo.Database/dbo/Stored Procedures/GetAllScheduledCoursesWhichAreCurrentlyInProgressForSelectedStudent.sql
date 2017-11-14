

CREATE PROCEDURE [dbo].[GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedStudent]
--DROP PROCEDURE GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedStudent

	@StudentProgressFileID int
AS
	/*ONSTITE Course Schedule*/
SELECT        

LookupDepartments.DepartmentName					AS Department, 
Curriculums.CurriculumName							AS CurriculumName, 
Courses.CourseName									AS CourseName, 
COUNT(1)											AS AmountEnrolled, 
Schedules.ScheduleStartDate							AS StartDate, 
Schedules.ScheduleCompletionDate					AS EndDate, 
LookupScheduleLocations.ScheduleLocation			AS ScheduledLocation, 
Venues.VenueName										AS VenueName, 
LookupTitles.Title + ' ' + 
Individuals.IndividualFirstName + ' ' + 
Individuals.IndividualLastname						AS FacilitatorName,
LookupEnrollmentProgressStates.
EnrollmentProgressCurrentState						AS CurrentState,
OnSiteSchedules.VenueID								AS VenueID, 
Schedules.IndividualID								AS FacilitactorID, 
Schedules.LookupScheduleLocationID					AS LocationID, 
CurriculumCourses.CurriculumID						AS CurriculumID,
Schedules.CurriculumCourseEnrollmentID				AS CurriculumCourseEnrollmentID

--------------------


FROM             LookupScheduleLocations INNER JOIN
                         Enrollments INNER JOIN
                         CurriculumCourseEnrollments ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         Courses ON CurriculumCourses.CourseID = Courses.CourseID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID INNER JOIN
                         Schedules ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID ON 
                         LookupScheduleLocations.LookupScheduleLocationID = Schedules.LookupScheduleLocationID INNER JOIN
                         LookupDepartments ON Curriculums.DepartmentID = LookupDepartments.DepartmentID INNER JOIN
                         Individuals ON Schedules.IndividualID = Individuals.IndividualID INNER JOIN
                         LookupTitles ON Individuals.TitleID = LookupTitles.TitleID INNER JOIN
                         ProgressFiles ON Enrollments.ProgressFileID = ProgressFiles.ProgressFileID INNER JOIN
                         StudentProgressFiles ON ProgressFiles.ProgressFileID = StudentProgressFiles.StudentProgressFileID INNER JOIN
                         OnSiteSchedules ON Schedules.ScheduleID = OnSiteSchedules.ScheduleID INNER JOIN
                         Venues ON OnSiteSchedules.VenueID = Venues.VenueID INNER JOIN
                         LookupEnrollmentProgressStates ON CurriculumCourseEnrollments.LookupEnrollmentProgressStateID = LookupEnrollmentProgressStates.LookupEnrollmentProgressStateID
GROUP BY Curriculums.CurriculumName, Courses.CourseName, Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Schedules.IndividualID, Schedules.LookupScheduleLocationID, 
                         LookupScheduleLocations.ScheduleLocation, LookupDepartments.DepartmentName, Individuals.IndividualFirstName, Individuals.IndividualLastname, LookupTitles.Title, CurriculumCourses.CurriculumID, 
                         StudentProgressFiles.StudentProgressFileID, Venues.VenueName, OnSiteSchedules.VenueID, LookupEnrollmentProgressStates.EnrollmentProgressCurrentState,
		Schedules.CurriculumCourseEnrollmentID
HAVING        (StudentProgressFiles.StudentProgressFileID = @StudentProgressFileID)  AND (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))--Courses that are Currently Inprogress

UNION ALL
/*ALL OFFSITE SCHEDULED COURSES*/
SELECT        
	LookupDepartments.DepartmentName					AS Department,
	Curriculums.CurriculumName							AS CurriculumName, 
	Courses.CourseName									AS CourseName, 
	COUNT(1)											AS AmountEnrolled, 
	Schedules.ScheduleStartDate							AS StartDate, 
	Schedules.ScheduleCompletionDate					AS EndDate, 
	LookupScheduleLocations.ScheduleLocation			AS ScheduledLocation, 
	Addresses.AddressDescription						AS VenueName, 
	LookupTitles.Title + ' ' + 
	Individuals.IndividualFirstName + ' ' + 
	Individuals.IndividualLastname						AS FacilitatorName,
	LookupEnrollmentProgressStates.
	EnrollmentProgressCurrentState						AS CurrentState,
	OffSiteSchedule.AddressID							AS VenueID, 
	Schedules.IndividualID								AS FacilitactorID, 
	Schedules.LookupScheduleLocationID					AS LocationID, 
	CurriculumCourses.CurriculumID						AS CurriculumID,
Schedules.CurriculumCourseEnrollmentID				AS CurriculumCourseEnrollmentID
FROM            
          LookupScheduleLocations INNER JOIN
                         Enrollments INNER JOIN
                         CurriculumCourseEnrollments ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID INNER JOIN
                         CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID INNER JOIN
                         Courses ON CurriculumCourses.CourseID = Courses.CourseID INNER JOIN
                         Curriculums ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID INNER JOIN
                         Schedules ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID ON 
                         LookupScheduleLocations.LookupScheduleLocationID = Schedules.LookupScheduleLocationID INNER JOIN
                         LookupDepartments ON Curriculums.DepartmentID = LookupDepartments.DepartmentID INNER JOIN
                         Individuals ON Schedules.IndividualID = Individuals.IndividualID INNER JOIN
                         LookupTitles ON Individuals.TitleID = LookupTitles.TitleID INNER JOIN
                         OffSiteSchedule ON Schedules.ScheduleID = OffSiteSchedule.ScheduleID INNER JOIN
                         Addresses ON OffSiteSchedule.AddressID = Addresses.AddressID INNER JOIN
                         ProgressFiles ON Enrollments.ProgressFileID = ProgressFiles.ProgressFileID INNER JOIN
                         StudentProgressFiles ON ProgressFiles.ProgressFileID = StudentProgressFiles.StudentProgressFileID
						 INNER JOIN
                         LookupEnrollmentProgressStates ON CurriculumCourseEnrollments.LookupEnrollmentProgressStateID = LookupEnrollmentProgressStates.LookupEnrollmentProgressStateID
GROUP BY Curriculums.CurriculumName, Courses.CourseName, Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Schedules.IndividualID, Schedules.LookupScheduleLocationID, 
                         LookupScheduleLocations.ScheduleLocation, LookupDepartments.DepartmentName, Individuals.IndividualFirstName, Individuals.IndividualLastname, LookupTitles.Title, Addresses.AddressDescription, 
                         OffSiteSchedule.AddressID, CurriculumCourses.CurriculumID, StudentProgressFiles.StudentProgressFileID, LookupEnrollmentProgressStates.EnrollmentProgressCurrentState,
		Schedules.CurriculumCourseEnrollmentID
HAVING        (StudentProgressFiles.StudentProgressFileID = @StudentProgressFileID)  AND (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))--Courses that are Currently Inprogress