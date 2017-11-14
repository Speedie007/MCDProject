CREATE TABLE [dbo].[ApprienticeshipEnrollments] (
    [EnrollmentID]                    INT  NOT NULL,
    [LookupSectionalEnrollmentTypeID] INT  NOT NULL,
    [DateUpdated]                     DATE CONSTRAINT [DF_ApprienticeshipEnrollments_DateUpdated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ApprienticeshipEnrollments_1] PRIMARY KEY CLUSTERED ([EnrollmentID] ASC),
    CONSTRAINT [FK_ApprienticeshipEnrollments_Enrollments] FOREIGN KEY ([EnrollmentID]) REFERENCES [dbo].[Enrollments] ([EnrollmentID]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_ApprienticeshipEnrollments_LookupSectionalEnrollmentTypes] FOREIGN KEY ([LookupSectionalEnrollmentTypeID]) REFERENCES [dbo].[LookupSectionalEnrollmentTypes] ([LookupSectionalEnrollmentTypeID])
);







