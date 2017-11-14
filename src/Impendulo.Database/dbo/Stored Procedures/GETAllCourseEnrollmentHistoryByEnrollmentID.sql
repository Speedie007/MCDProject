CREATE PROCEDURE [dbo].[GETAllCourseEnrollmentHistoryByEnrollmentID]
--DROP PROCEDURE GETAllCourseEnrollmentHistoryByEnrollmentID

	@EnrollmentID int
AS
	WITH EnollmentHiearchy(
	EnrollmentID, 
	EnrolmentParentID, 
	--EnquiryID,
	ProgressFileID, 
	EnrollmentExcempt, 
	LookupEnrollmentProgressStateID, 
	CurriculumID, 
	DateIntitiated,  
	ComponentLevel) AS
(

SELECT  
a.EnrollmentID, 
a.EnrolmentParentID, 
--a .EnquiryID, 
a.ProgressFileID, 
a.EnrollmentExcempt, 
a.LookupEnrollmentProgressStateID, 
a.CurriculumID, 
a.DateIntitiated, 
	  0 AS ComponentLevel
FROM            
[Enrollments] a
  where a.EnrollmentID = @EnrollmentID
  and EnrolmentParentID = 0

  Union ALL

  SELECT
  b.EnrollmentID, 
b.EnrolmentParentID, 
--b.EnquiryID, 
b.ProgressFileID,
b.EnrollmentExcempt, 
b.LookupEnrollmentProgressStateID, 
b.CurriculumID, 
b.DateIntitiated, 
	 ComponentLevel + 1
  FROM [Enrollments] b
  inner join EnollmentHiearchy EH
  on b.EnrolmentParentID = EH.EnrollmentID    
)

select 
c.EnrollmentID, 
c.ProgressFileID,
d.CurriculumCourseEnrollmentID,
		c.DateIntitiated, 
		F.EnrollmentProgressCurrentState AS [ProgressState],
		G.CurriculumName,
		I.CourseName, 
		E.Duration, 
		d.CourseCost, 
		case  h.ScheduleStatusID
		when 1 then 'N'
		when 2 then 'Y'
		else 'NA'
		end AS Confirmed,
		ISNULL(CONVERT(VARCHAR(50),H.ScheduleStartDate),
		CASE F.EnrollmentProgressCurrentState
		WHEN 'Excempt' THen 'Course/Module Excempt'
		WHEN 'New Enrollment' THen 'Waiting To Be Scheduled'
		 ELSE 'Not Secheduled'
		END) AS ScheduleStartDate, 
		ISNULL(CONVERT(varchar(50),H.ScheduleCompletionDate),
		CASE F.EnrollmentProgressCurrentState
		WHEN 'Excempt' THen 'Course/Module Excempt'
		WHEN 'New Enrollment' THen 'Waiting To Be Scheduled'
		 ELSE 'Not Secheduled'
		END) AS ScheduleCompletionDate, 
		d.CoursePaymentMade
from EnollmentHiearchy c
	INNER JOIN CurriculumCourseEnrollments d
		ON c.EnrollmentID = d.EnrollmentID 
	INNER JOIN CurriculumCourses  E
				ON d.CurriculumCourseID = E.CurriculumCourseID
	INNER JOIN Courses I
		ON E.CourseID = I.CourseID  
	INNER JOIN LookupEnrollmentProgressStates F
				ON d.LookupEnrollmentProgressStateID = F.LookupEnrollmentProgressStateID
	INNER JOIN Curriculums G
					ON E.CurriculumID = G.CurriculumID 
	LEFT OUTER JOIN Schedules H
					ON d.CurriculumCourseEnrollmentID = H.CurriculumCourseEnrollmentID
order by  ComponentLevel DESC, H.ScheduleStartDate ;