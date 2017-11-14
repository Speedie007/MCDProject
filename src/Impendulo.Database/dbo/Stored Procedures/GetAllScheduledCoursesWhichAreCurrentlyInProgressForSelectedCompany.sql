

CREATE PROCEDURE [dbo].[GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedCompany]
--DROP PROCEDURE GetAllScheduledCoursesWhichAreCurrentlyInProgressForSelectedCompany 1

	@CompanyProgressFileID int
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
OnSiteSchedules.VenueID								AS VenueID, 
Schedules.IndividualID								AS FacilitactorID, 
Schedules.LookupScheduleLocationID					AS LocationID, 
CurriculumCourses.CurriculumID						AS CurriculumID
--,Schedules.CurriculumCourseEnrollmentID				AS CurriculumCourseEnrollmentID

--------------------


FROM            
		CompanyStudentProgressFiles 
			INNER JOIN StudentProgressFiles 
				ON CompanyStudentProgressFiles.StudentProgressFileID = StudentProgressFiles.StudentProgressFileID 
			INNER JOIN ProgressFiles 
				ON StudentProgressFiles.StudentProgressFileID = ProgressFiles.ProgressFileID 
			INNER JOIN Enrollments 
			INNER JOIN CurriculumCourseEnrollments 
				ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID 
			INNER JOIN CurriculumCourses 
				ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID 
			INNER JOIN Courses 
				ON CurriculumCourses.CourseID = Courses.CourseID 
			INNER JOIN Curriculums 
				ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID 
			INNER JOIN Schedules 
				ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID 
				ON ProgressFiles.ProgressFileID = Enrollments.ProgressFileID 
			INNER JOIN OnSiteSchedules 
				ON Schedules.ScheduleID = OnSiteSchedules.ScheduleID 
			INNER JOIN Venues 
				ON OnSiteSchedules.VenueID = Venues.VenueID 
			INNER JOIN LookupScheduleLocations 
				ON Schedules.LookupScheduleLocationID = LookupScheduleLocations.LookupScheduleLocationID 
			INNER JOIN LookupDepartments 
				ON Curriculums.DepartmentID = LookupDepartments.DepartmentID 
			INNER JOIN Individuals 
				ON Schedules.IndividualID = Individuals.IndividualID 
			INNER JOIN LookupTitles 
				ON Individuals.TitleID = LookupTitles.TitleID
GROUP BY 
		Curriculums.CurriculumName, 
		Courses.CourseName, 
		Schedules.ScheduleStartDate, 
		Schedules.ScheduleCompletionDate, 
		OnSiteSchedules.VenueID, 
		CompanyStudentProgressFiles.CompanyProgressFileID, 
		Venues.VenueName, 
		Schedules.IndividualID, 
		Schedules.LookupScheduleLocationID, 
		LookupScheduleLocations.ScheduleLocation, 
		LookupDepartments.DepartmentName, 
		Individuals.IndividualFirstName, 
		Individuals.IndividualLastname, 
		LookupTitles.Title, 
		CurriculumCourses.CurriculumID 
		--,Schedules.CurriculumCourseEnrollmentID

HAVING        
		(CompanyStudentProgressFiles.CompanyProgressFileID = @CompanyProgressFileID)  AND (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))--Courses that are Currently Inprogress

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
	OffSiteSchedule.AddressID							AS VenueID, 
	Schedules.IndividualID								AS FacilitactorID, 
	Schedules.LookupScheduleLocationID					AS LocationID, 
	CurriculumCourses.CurriculumID						AS CurriculumID
	--,Schedules.CurriculumCourseEnrollmentID				AS CurriculumCourseEnrollmentID

FROM            
		CompanyStudentProgressFiles 
			INNER JOIN StudentProgressFiles 
				ON CompanyStudentProgressFiles.StudentProgressFileID = StudentProgressFiles.StudentProgressFileID 
			INNER JOIN	ProgressFiles 
				ON StudentProgressFiles.StudentProgressFileID = ProgressFiles.ProgressFileID 
			INNER JOIN Enrollments 
			INNER JOIN CurriculumCourseEnrollments 
				ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID 
			INNER JOIN CurriculumCourses ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID 
			INNER JOIN Courses 
				ON CurriculumCourses.CourseID = Courses.CourseID 
			INNER JOIN Curriculums 
				ON CurriculumCourses.CurriculumID = Curriculums.CurriculumID 
			INNER JOIN Schedules 
				ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID 
				ON ProgressFiles.ProgressFileID = Enrollments.ProgressFileID 
			INNER JOIN LookupScheduleLocations 
				ON Schedules.LookupScheduleLocationID = LookupScheduleLocations.LookupScheduleLocationID 
			INNER JOIN LookupDepartments 
				ON Curriculums.DepartmentID = LookupDepartments.DepartmentID 
			INNER JOIN Individuals 
				ON Schedules.IndividualID = Individuals.IndividualID 
			INNER JOIN LookupTitles 
				ON Individuals.TitleID = LookupTitles.TitleID 
			INNER JOIN OffSiteSchedule 
				ON Schedules.ScheduleID = OffSiteSchedule.ScheduleID 
			INNER JOIN Addresses 
				ON OffSiteSchedule.AddressID = Addresses.AddressID
GROUP BY 
		Curriculums.CurriculumName, 
		Courses.CourseName, 
		Schedules.ScheduleStartDate, 
		Schedules.ScheduleCompletionDate,
		CompanyStudentProgressFiles.CompanyProgressFileID, 
		Schedules.IndividualID, 
		Schedules.LookupScheduleLocationID, 
		LookupScheduleLocations.ScheduleLocation, 
		LookupDepartments.DepartmentName, 
		Individuals.IndividualFirstName, 
		Individuals.IndividualLastname, 
		LookupTitles.Title, 
		Addresses.AddressDescription, 
		OffSiteSchedule.AddressID, 
		CurriculumCourses.CurriculumID
		--,Schedules.CurriculumCourseEnrollmentID
HAVING        
		(CompanyStudentProgressFiles.CompanyProgressFileID = @CompanyProgressFileID)  AND (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))--Courses that are Currently Inprogress