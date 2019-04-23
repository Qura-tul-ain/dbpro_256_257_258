use [DB43]

CREATE TABLE [Person](
[Id] int NOT NULL PRIMARY KEY,
[LastName] varchar(30) NOT NULL,
[FirstName] varchar(30) NOT NULL,
[Contact] varchar(20) NOT NULL UNIQUE,
[Email] varchar(20) NOT NULL UNIQUE,
[Gender] varchar(20) NOT NULL,
[Password] nvarchar(128) NOT NULL,
[Discriminator] varchar(50) NOT NULL ,

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
[Status] varchar(20) NOT NULL,
PRIMARY KEY(Id)
)

CREATE TABLE [StudentRegisterCourse](
[Id] int NOT NULL,
[CourseId] int not null FOREIGN KEY REFERENCES Student(Id),
[StudentId] int not null FOREIGN KEY REFERENCES Student(Id),
PRIMARY KEY(Id, CourseId, StudentId)
)

CREATE TABLE [Quiz](
[Id] int NOT NULL,
[Title] varchar(30) NOT NULL UNIQUE,
[DateCreated] Date NOT NULL,
[TotalMarks] int NOT NULL,
[CourseId] int not null FOREIGN KEY REFERENCES Course(Id),
PRIMARY KEY(Id)
)

create TABLE [Questions](
[Id] int NOT NULL,
[Name] varchar(30) NOT NULL UNIQUE,
[DateCreated] Date NOT NULL,
[DateUpdated] Date NOT NULL,
[TotalMarks] int NOT NULL,
[QuizId] int NOT NULL FOREIGN KEY REFERENCES Quiz(Id),
PRIMARY KEY(Id)
)

CREATE TABLE [Options](
[Id] int NOT NULL,
[OptionValue] varchar(30) NOT NULL UNIQUE,
[DateCreated] Date NOT NULL,
[DateUpdated] Date NOT NULL,
[QuestionsId] int NOT NULL FOREIGN KEY REFERENCES Questions(Id),
PRIMARY KEY(Id)
)

CREATE TABLE [CorrectOption](
[Id] int NOT NULL,
[Correctvalue] varchar(30) NOT NULL UNIQUE,
[DateCreated] Date NOT NULL,
[DateUpdated] Date NOT NULL,
[QuestionId] int NOT NULL FOREIGN KEY REFERENCES Questions(Id),
PRIMARY KEY(Id)
)

CREATE TABLE [StudentResult](
[Id] int NOT NULL,
[StudentId] int not null FOREIGN KEY REFERENCES Student(Id),
[QuestionId] int not null FOREIGN KEY REFERENCES Questions(Id),
[EvalutionDate] Date NOT NULL,
[Ans] varchar(30) NOT NULL,
[Marks] int NOT NULL,

PRIMARY KEY(Id)
)

CREATE TABLE [Announcement](
[Id] int NOT NULL,
[Text] varchar(300) NOT NULL,
[PersonId] int NOT NULL FOREIGN KEY REFERENCES Person(Id),
[CourseId] int NOT NULL FOREIGN KEY REFERENCES Course(Id),
PRIMARY KEY(Id)
)






