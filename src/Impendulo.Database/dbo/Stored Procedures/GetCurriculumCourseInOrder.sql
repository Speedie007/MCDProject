-- =============================================
-- Create basic stored procedure template
-- =============================================

CREATE PROCEDURE [dbo].[GetCurriculumCourseInOrder]
--drop Procedure GetCurriculumCourseInOrder 8
	@CurriculumID int
AS
	 

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

select CurriculumCourseID, CurriculumCourseParentID, CurriculumID,CourseID,EnrollmentTypeID,Duration,Cost,CostCode, ComponentLevel
from CoursePiorirty CP
order by ComponentLevel, CostCode;