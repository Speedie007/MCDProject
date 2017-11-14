-- =============================================
-- Create basic stored procedure template

CREATE PROCEDURE [dbo].[GetAllOnSiteScheduledCourses]
--DROP PROCEDURE GetAllOnsiteScheduledCourses
	 @ScheduleLocationID int,
	 @CurriculumCourseID int,
	 @StartDate date,
	 @EndDate date
AS
SELECT         Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Individuals.IndividualID, Individuals.IndividualFirstName + ' ' + Individuals.IndividualLastname AS FacilitatorName, Venues.VenueID, 
                         Venues.VenueName, COUNT(1) AS AmountEnrolled
FROM            Schedules INNER JOIN
                         OnSiteSchedules ON Schedules.ScheduleID = OnSiteSchedules.ScheduleID INNER JOIN
                         Facilitators ON Schedules.IndividualID = Facilitators.IndividualID INNER JOIN
                         Individuals ON Facilitators.IndividualID = Individuals.IndividualID INNER JOIN
                         Venues ON OnSiteSchedules.VenueID = Venues.VenueID INNER JOIN
                         CurriculumCourseEnrollments ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID
GROUP BY Schedules.ScheduleStartDate, Schedules.ScheduleCompletionDate, Individuals.IndividualID, Individuals.IndividualFirstName, Individuals.IndividualLastname, Venues.VenueID, Venues.VenueName, 
                         Schedules.LookupScheduleLocationID, CurriculumCourseEnrollments.CurriculumCourseID
HAVING        (Schedules.LookupScheduleLocationID = @ScheduleLocationID) AND (CurriculumCourseEnrollments.CurriculumCourseID = @CurriculumCourseID) AND (Schedules.ScheduleStartDate >= @StartDate) AND 
                         (Schedules.ScheduleCompletionDate <= @EndDate);