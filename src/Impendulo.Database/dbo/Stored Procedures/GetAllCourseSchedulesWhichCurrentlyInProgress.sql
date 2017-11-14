
CREATE PROCEDURE [dbo].[GetAllCourseSchedulesWhichCurrentlyInProgress]
--DROP PROCEDURE [GetAllCourseSchedulesWhichCurrentlyInProgress]
AS
	--OnSite scheduled courses currently running
SELECT       
	Schedules.ScheduleStartDate					AS [StartDate], 
	Schedules.ScheduleCompletionDate			AS [EndDate], 
	Individuals.IndividualFirstName + ' ' + 
	Individuals.IndividualLastname				AS [FacilitatorName], 
	Venues.VenueName								AS [Venue], 
	COUNT(1)									AS [AmountEnrolled], 
	Courses.CourseName							AS [CourseName], 
	LookupScheduleLocations.ScheduleLocation	AS [ScheduleLocation],
	Facilitators.IndividualID					AS [FacilitatorID], 
	Venues.VenueID								AS [VenueID], 
	CurriculumCourses.CurriculumCourseID		AS [CurriculumCourseID], 
	Courses.CourseID							AS [CourseID], 
	Schedules.LookupScheduleLocationID			AS [ScheduleLocationID]
FROM            
		Schedules 
			INNER JOIN OnSiteSchedules 
						ON Schedules.ScheduleID = OnSiteSchedules.ScheduleID 
			INNER JOIN Facilitators 
						ON Schedules.IndividualID = Facilitators.IndividualID 
			INNER JOIN Individuals 
						ON Facilitators.IndividualID = Individuals.IndividualID 
			INNER JOIN Venues 
						ON OnSiteSchedules.VenueID = Venues.VenueID 
			INNER JOIN CurriculumCourseEnrollments 
						ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID 
			INNER JOIN CurriculumCourses 
						ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID 
			INNER JOIN Courses 
						ON CurriculumCourses.CourseID = Courses.CourseID 
			INNER JOIN LookupScheduleLocations 
						ON Schedules.LookupScheduleLocationID = LookupScheduleLocations.LookupScheduleLocationID
	GROUP BY 
				Schedules.ScheduleStartDate, 
				Schedules.ScheduleCompletionDate, 
				Venues.VenueName, 
				Schedules.LookupScheduleLocationID, 
				Courses.CourseName, 
				Facilitators.IndividualID, 
				Venues.VenueID, 
				CurriculumCourses.CurriculumCourseID, 
				Courses.CourseID, 
				LookupScheduleLocations.ScheduleLocation, 
				Individuals.IndividualFirstName, 
				Individuals.IndividualLastname
HAVING        (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))
UNION
SELECT        
	Schedules.ScheduleStartDate					AS [StartDate], 
	Schedules.ScheduleCompletionDate			AS [EndDate], 
	Individuals.IndividualFirstName + ' ' + 
	Individuals.IndividualLastname				AS [FacilitatorName],
	Addresses.AddressDescription				AS [Venue], 
	COUNT(1)									AS [AmountEnrolled], 
	Courses.CourseName							AS [CourseName], 
	LookupScheduleLocations.ScheduleLocation	AS [ScheduleLocation],
	Facilitators.IndividualID					AS [FacilitatorID],
	Addresses.AddressID							AS [VenueID], 
	CurriculumCourses.CurriculumCourseID		AS [CurriculumCourseID], 
	Courses.CourseID							AS [CourseID], 
	Schedules.LookupScheduleLocationID			AS [ScheduleLocationID]

FROM Schedules 
		INNER JOIN Facilitators 
				ON Schedules.IndividualID = Facilitators.IndividualID 
		INNER JOIN Individuals 
				ON Facilitators.IndividualID = Individuals.IndividualID 
		INNER JOIN CurriculumCourseEnrollments 
				ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID 
		INNER JOIN CurriculumCourses 
				ON CurriculumCourseEnrollments.CurriculumCourseID = CurriculumCourses.CurriculumCourseID 
		INNER JOIN Courses 
				ON CurriculumCourses.CourseID = Courses.CourseID 
		INNER JOIN LookupScheduleLocations 
				ON Schedules.LookupScheduleLocationID = LookupScheduleLocations.LookupScheduleLocationID 
		INNER JOIN OffSiteSchedule 
				ON Schedules.ScheduleID = OffSiteSchedule.ScheduleID 
		INNER JOIN Addresses 
				ON OffSiteSchedule.AddressID = Addresses.AddressID
GROUP BY 
		Schedules.ScheduleStartDate, 
		Schedules.ScheduleCompletionDate, 
		Schedules.LookupScheduleLocationID, 
		Courses.CourseName, 
		Facilitators.IndividualID, 
		CurriculumCourses.CurriculumCourseID, 
		Courses.CourseID, 
		LookupScheduleLocations.ScheduleLocation, 
		Individuals.IndividualFirstName, 
		Individuals.IndividualLastname, 
		Addresses.AddressID, 
		Addresses.AddressDescription
HAVING        (GETDATE() BETWEEN DATEADD (DAY , -1 , Schedules.ScheduleStartDate )    AND DATEADD (DAY , 1 , Schedules.ScheduleCompletionDate ))