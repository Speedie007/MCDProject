
CREATE PROCEDURE [dbo].[GetAllOffSiteScheduledCourses]
--DROP PROCEDURE GetAllOffsiteScheduledCourses
	 @ScheduleLocationID int,
	 @CurriculumCourseID int,
	 @StartDate date,
	 @EndDate date
AS
	SELECT         Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Individuals.IndividualID, Individuals.IndividualFirstName + ' ' + Individuals.IndividualLastname AS FacilitatorName, 
                         Addresses.AddressID AS VenueID, Addresses.AddressDescription AS VenueName, 1 AS AmountEnrolled
FROM            Schedules INNER JOIN
                         Facilitators ON Schedules.IndividualID = Facilitators.IndividualID INNER JOIN
                         Individuals ON Facilitators.IndividualID = Individuals.IndividualID INNER JOIN
                         CurriculumCourseEnrollments ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID INNER JOIN
                         OffSiteSchedule ON Schedules.ScheduleID = OffSiteSchedule.ScheduleID INNER JOIN
                         Addresses ON OffSiteSchedule.AddressID = Addresses.AddressID
WHERE        (Schedules.LookupScheduleLocationID = @ScheduleLocationID) AND (CurriculumCourseEnrollments.CurriculumCourseID = @CurriculumCourseID)
GROUP BY Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Individuals.IndividualID, Individuals.IndividualFirstName + ' ' + Individuals.IndividualLastname, Addresses.AddressID, Addresses.AddressDescription
HAVING        (Schedules.ScheduleStartDate >= @StartDate) AND (Schedules.ScheduleCompletionDate <= @EndDate);