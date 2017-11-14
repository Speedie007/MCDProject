

CREATE PROCEDURE [dbo].[GetCurriculumCourseEnrollmentPreRequisiteEndDate]
--DROP PROCEDURE GetCurriculumCourseEnrollmentPreRequisiteEndDate 4129,8084

	@CurriculumCourseID int,
	@EnrollmentID int
AS
		WITH CoursePiorirty(CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,[CostCode],  ComponentLevel) AS
(


SELECT  b.[CurriculumCourseID]
      ,b.[CurriculumCourseParentID]
      ,b.[CurriculumID]
      ,b.[CourseID]
      ,b.[EnrollmentTypeID]
      ,b.[Duration]
      ,b.[Cost]
	  ,b.CostCode
	  ,0 AS ComponentLevel
  FROM [CurriculumCourses] b
  where 
  --b.[CurriculumID] = 1 and
   [CurriculumCourseID] = @CurriculumCourseID--4151 -- 4153

  Union ALL

  SELECT  c.[CurriculumCourseID]
      ,c.[CurriculumCourseParentID]
      ,c.[CurriculumID]
      ,c.[CourseID]
      ,c.[EnrollmentTypeID]
      ,c.[Duration]
      ,c.[Cost]
	  ,c.CostCode
	  ,ComponentLevel + 1
  FROM [CurriculumCourses] c
  inner join CoursePiorirty CP
  on CP.CurriculumCourseParentID = c.CurriculumCourseID    
)

select top 1 CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,[CostCode], ComponentLevel
INTO #newtable
from CoursePiorirty CP

where
CP.CurriculumCourseID not in (@CurriculumCourseID)  AND 
CP.CurriculumCourseID in (
SELECT      CurriculumCourseID
FROM            CurriculumCourseEnrollments
where  EnrollmentID = @EnrollmentID
)
order by CP.ComponentLevel;

SELECT         
--Schedules.ScheduleID, 
--Schedules.CurriculumCourseEnrollmentID, 
--Schedules.IndividualID, 
--Schedules.ScheduleStartDate, 
Schedules.ScheduleCompletionDate
--Schedules.ScheduleStatusID, 
--Schedules.EnrollmentID, 
--Schedules.LookupScheduleLocationID
FROM            
		Schedules 
		INNER JOIN CurriculumCourseEnrollments 
			ON Schedules.CurriculumCourseEnrollmentID = CurriculumCourseEnrollments.CurriculumCourseEnrollmentID
						 where 
						 CurriculumCourseEnrollments.EnrollmentID = @EnrollmentID AND 
						 CurriculumCourseEnrollments.CurriculumCourseID in (  
																				 Select CurriculumCourseID from #newtable
																				 );

						-- ;