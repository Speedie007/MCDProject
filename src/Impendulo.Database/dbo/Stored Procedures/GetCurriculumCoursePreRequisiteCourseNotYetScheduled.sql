
create PROCEDURE [dbo].[GetCurriculumCoursePreRequisiteCourseNotYetScheduled]
--drop PROCEDURE [dbo].[GetCurriculumCoursePreRequisiteCourseNotYetScheduled]
--GetCurriculumCoursePreRequisiteCourse 4143,6060
	@CurriculumCourseID int, --4142
	@EnrollmentID int --6060
AS
--set @CurriculumID = 4142;
--set @EnrollmentID = 6060;

--	WITH CoursePiorirty(CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,  ComponentLevel) AS
--(

--SELECT  b.[CurriculumCourseID]
--      ,b.[CurriculumCourseParentID]
--      ,b.[CurriculumID]
--      ,b.[CourseID]
--      ,b.[EnrollmentTypeID]
--      ,b.[Duration]
--      ,b.[Cost],
--	  0 AS ComponentLevel
--  FROM [CurriculumCourses] b
--  where 
--  --b.[CurriculumID] = 1 and
--   [CurriculumCourseID] = @CurriculumCourseID 

--  Union ALL

--  SELECT  c.[CurriculumCourseID]
--      ,c.[CurriculumCourseParentID]
--      ,c.[CurriculumID]
--      ,c.[CourseID]
--      ,c.[EnrollmentTypeID]
--      ,c.[Duration]
--      ,c.[Cost],
--	 ComponentLevel + 1
--  FROM [CurriculumCourses] c
--  inner join CoursePiorirty CP
--  on CP.CurriculumCourseParentID = c.CurriculumCourseID    
--)

----Selects all the Parent Courses for the Course Linked to the enrollment 
--select CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost, ComponentLevel
--INTO #newtable
--from CoursePiorirty CP
--where  CurriculumCourseID not in (@CurriculumCourseID) and CurriculumCourseID in (
--											--CourseLinked to the Enrollment
--											SELECT          CurriculumCourseID
--											FROM            CurriculumCourseEnrollments d
--											WHERE        (d.EnrollmentID =  @EnrollmentID) AND d.CurriculumCourseEnrollmentID in (
--																									--Linked Courses Which are allready Scheduled
--																									SELECT         CurriculumCourseEnrollmentID 
--																											FROM            Schedules s
--																											WHERE        (s.EnrollmentID =  @EnrollmentID)
--																									))
--order by ComponentLevel desc;


--Select * from #newtable;


	WITH CoursePiorirty(CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost, [CostCode], ComponentLevel) AS
(

SELECT  b.[CurriculumCourseID]
      ,b.[CurriculumCourseParentID]
      ,b.[CurriculumID]
      ,b.[CourseID]
      ,b.[EnrollmentTypeID]
      ,b.[Duration]
      ,b.[Cost]
	  ,b.[CostCode]
	  ,0 AS ComponentLevel
  FROM [CurriculumCourses] b
  where 
  --b.[CurriculumID] = 1 and
   [CurriculumCourseID] = @CurriculumCourseID
  Union ALL

  SELECT  c.[CurriculumCourseID]
      ,c.[CurriculumCourseParentID]
      ,c.[CurriculumID]
      ,c.[CourseID]
      ,c.[EnrollmentTypeID]
      ,c.[Duration]
      ,c.[Cost]
	  ,c.[CostCode]
	 ,ComponentLevel + 1
  FROM [CurriculumCourses] c
  inner join CoursePiorirty CP
  on CP.CurriculumCourseParentID = c.CurriculumCourseID    
)

--Selects all the Parent Courses for the Course Linked to the enrollment 
select CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost, [CostCode],ComponentLevel
INTO #newtable
from CoursePiorirty CP
where  CP.CurriculumCourseID not in (@CurriculumCourseID)
 and CP.CurriculumCourseID in (
											--CourseLinked to the Enrollment
											SELECT          CurriculumCourseID
											--,*
											FROM            CurriculumCourseEnrollments d
											WHERE        (d.EnrollmentID =  @EnrollmentID)
							   );






Select CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,[CostCode], ComponentLevel
from #newtable A where A.CurriculumCourseID not in( 
SELECT 
--CurriculumCourseEnrollments.CurriculumCourseEnrollmentID, 
CurriculumCourseEnrollments.CurriculumCourseID
--,CurriculumCourseEnrollments.EnrollmentID, 
--CurriculumCourseEnrollments.LookupEnrollmentProgressStateID, 
--CurriculumCourseEnrollments.CourseCost, 
--Schedules.ScheduleStartDate, 
--Schedules.ScheduleCompletionDate
FROM            
CurriculumCourseEnrollments 
	INNER JOIN Schedules 
		ON CurriculumCourseEnrollments.CurriculumCourseEnrollmentID = Schedules.CurriculumCourseEnrollmentID
		where CurriculumCourseEnrollments.EnrollmentID = @EnrollmentID)
		order by ComponentLevel desc;

		drop table #newtable;