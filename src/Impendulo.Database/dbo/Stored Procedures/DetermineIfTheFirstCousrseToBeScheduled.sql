

CREATE PROCEDURE [dbo].[DetermineIfTheFirstCousrseToBeScheduled]
--drop PROCEDURE DetermineIfTheFirstCousrseToBeScheduled 6,11135,4135
	@CurriculumID int,
	@EnrollmentID int,
	@CurriculumCourseID_ToCheck int
AS

DECLARE @FirstCourseToBeScheduled_CourseCurriculumCourseID int;
DECLARE @false bit = 0;
DECLARE @true bit = 1;

	WITH CoursePiorirty(CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,CostCode,  ComponentLevel) AS
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
  where b.[CurriculumID] = @CurriculumID
  and [CurriculumCourseParentID] = 0

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
  on c.CurriculumCourseParentID = CP.CurriculumCourseID    
)

select top 1 @FirstCourseToBeScheduled_CourseCurriculumCourseID = CurriculumCourseID
from CoursePiorirty CP
where CurriculumCourseID in (SELECT        CurriculumCourseEnrollments.CurriculumCourseID
FROM            Enrollments INNER JOIN
                         CurriculumCourseEnrollments ON Enrollments.EnrollmentID = CurriculumCourseEnrollments.EnrollmentID
WHERE        (Enrollments.EnrollmentID = @EnrollmentID)

)
order by ComponentLevel;

if @FirstCourseToBeScheduled_CourseCurriculumCourseID = @CurriculumCourseID_ToCheck
begin
 select @true AS [CheckValue]
end
else
begin
select @false AS [CheckValue]
end