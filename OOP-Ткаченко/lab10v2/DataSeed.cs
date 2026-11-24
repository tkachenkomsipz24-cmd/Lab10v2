namespace Lab10v2;

public static class DataSeed
{
    public static List<Course> GetCourses() => new()
    {
        new Course { Id = 1, Title = "Programming Basics", Credits = 5 },
        new Course { Id = 2, Title = "Databases",          Credits = 4 },
        new Course { Id = 3, Title = "Algorithms",         Credits = 6 },
        new Course { Id = 4, Title = "Web Development",    Credits = 5 },
    };

    public static List<Student> GetStudents() => new()
    {
        new Student { Id = 1,  Name = "Олександр Коваль",    Age = 18, CourseId = 1 },
        new Student { Id = 2,  Name = "Марія Шевченко",      Age = 19, CourseId = 1 },
        new Student { Id = 3,  Name = "Іван Бондаренко",     Age = 20, CourseId = 1 },
        new Student { Id = 4,  Name = "Софія Ткаченко",      Age = 21, CourseId = 1 },
        new Student { Id = 5,  Name = "Андрій Мельник",      Age = 22, CourseId = 2 },
        new Student { Id = 6,  Name = "Наталія Кравчук",     Age = 19, CourseId = 2 },
        new Student { Id = 7,  Name = "Дмитро Олійник",      Age = 23, CourseId = 2 },
        new Student { Id = 8,  Name = "Катерина Лисенко",    Age = 20, CourseId = 3 },
        new Student { Id = 9,  Name = "Богдан Гриценко",     Age = 24, CourseId = 3 },
        new Student { Id = 10, Name = "Юлія Савченко",       Age = 18, CourseId = 3 },
        new Student { Id = 11, Name = "Максим Руденко",      Age = 25, CourseId = 4 },
        new Student { Id = 12, Name = "Оксана Мазур",        Age = 21, CourseId = 4 },
        new Student { Id = 13, Name = "Володимир Павленко",  Age = 19, CourseId = 4 },
        new Student { Id = 14, Name = "Анна Захарченко",     Age = 17, CourseId = 1 },
        new Student { Id = 15, Name = "Тарас Лук'яненко",    Age = 22, CourseId = 4 },
    };
}
