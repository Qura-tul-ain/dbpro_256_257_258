use [DB43]


CREATE TABLE [Department](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Name] varchar(30) NOT NULL,
[Image] varchar(MAX) NULL,
)

CREATE TABLE [Person](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[LastName] varchar(30) NOT NULL,
[FirstName] varchar(30) NOT NULL,
[Contact] varchar(20) NOT NULL UNIQUE,
[Email] varchar(50) NOT NULL UNIQUE,
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
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Title] varchar(30) NOT NULL,
[Credits] varchar(30) NOT NULL,
[Fee] Decimal NOT NULL,
[DepartmentId] int NOT NULL FOREIGN KEY REFERENCES Department(Id),
[CourseImage] varchar(MAX) NULL,
)
CREATE TABLE [AssignInstructor](
[InstructorId] int NOT NULL FOREIGN KEY REFERENCES Instructor(InstructorId),
[CourseId] int NOT NULL FOREIGN KEY REFERENCES Course(Id),
PRIMARY KEY(InstructorId, CourseId)
)

CREATE TABLE [Student](
[Id] int not null PRIMARY KEY(Id) IDENTITY,
[RegistrationNumber] varchar(30) NOT NULL UNIQUE,
[EnrollmentDate] Date NOT NULL,
[Status] varchar(20) NOT NULL,
[PersonId] int NOT NULL FOREIGN KEY REFERENCES Person(Id) UNIQUE,

)

CREATE TABLE [StudentRegisterCourse](
[Id] int NOT NULL PRIMARY KEY IDENTITY ,
[DepartmentId]  int not null FOREIGN KEY REFERENCES Department(Id),
[CourseId] int not null FOREIGN KEY REFERENCES Course(Id),
[StudentId] int not null FOREIGN KEY REFERENCES Student(PersonId),
)

CREATE TABLE [Quiz](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Title] varchar(30) NOT NULL UNIQUE,
[DateCreated] Date NOT NULL,
[TotalMarks] int NOT NULL,
[CourseId] int not null FOREIGN KEY REFERENCES Course(Id),

)

create TABLE [Questions](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Name] varchar(30) NOT NULL UNIQUE,
[TotalMarks] int NOT NULL DEFAULT(1),
[QuizId] int NOT NULL FOREIGN KEY REFERENCES Quiz(Id),

)

CREATE TABLE [Options](
[Id] int NOT NULL PRIMARY KEY IDENTITY, 
[OptionValue] varchar(30) NOT NULL UNIQUE,
[IsAnswer] varchar(15) NOT NULL DEFAULT('false'),
[QuestionsId] int NOT NULL FOREIGN KEY REFERENCES Questions(Id),

)

CREATE TABLE [CorrectOption](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Correctvalue] varchar(30) NOT NULL UNIQUE,
[QuestionId] int NOT NULL FOREIGN KEY REFERENCES Questions(Id),

)

CREATE TABLE [StudentResult](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[StudentId] int not null FOREIGN KEY REFERENCES Student(PersonId),
[QuestionId] int not null FOREIGN KEY REFERENCES Questions(Id),
[EvalutionDate] Date NOT NULL,
[Ans] varchar(30) NOT NULL,
[Marks] float NOT NULL,


)

CREATE TABLE [Announcement](
[Id] int NOT NULL PRIMARY KEY IDENTITY,
[Text] varchar(300) NOT NULL,
[PersonId] int NOT NULL FOREIGN KEY REFERENCES Person(Id),
[CourseId] int NOT NULL FOREIGN KEY REFERENCES Course(Id),

)






