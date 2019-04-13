use [DB43]

CREATE TABLE [Person](
[Id]int NOT NULL PRIMARY KEY,
[LastName]varchar(30) NOT NULL,
[FirstName]varchar(30) NOT NULL,
[Contact] varchar(20) NOT NULL UNIQUE,
[Email] varchar(20) NOT NULL UNIQUE,
[Gender] varchar NOT NULL,
[Discriminator]Date NOT NULL,

)

CREATE TABLE [Instructor](
[InstructorId] int NOT NULL FOREIGN KEY REFERENCES Person(Id),
[Qualification] varchar(30) NOT NULL,
[HireDate] Date NOT NULL,
PRIMARY KEY(InstructorId)
)

CREATE TABLE [Course](
[Id] int NOT NULL PRIMARY KEY,
[Title] varchar(30) NOT NULL,
[Credits] varchar(30) NOT NULL,
[Email] varchar(20) NOT NULL UNIQUE,
[Fee] Decimal NOT NULL

)
CREATE TABLE [AssignInstructor](
[InstructorId] int NOT NULL FOREIGN KEY REFERENCES Instructor(InstructorId),
[CourseId] int NOT NULL FOREIGN KEY REFERENCES Course(Id),
PRIMARY KEY(InstructorId, CourseId)
)

CREATE TABLE [Student](
[Id] int NOT NULL FOREIGN KEY REFERENCES Person(Id),
[RegistrationNumber] varchar(30) NOT NULL UNIQUE,
[EnrollmentDate] Date NOT NULL,
[Status] varchar NOT NULL,
PRIMARY KEY(Id)
)

