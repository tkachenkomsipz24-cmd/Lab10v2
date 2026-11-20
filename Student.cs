namespace Lab10v2;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public int CourseId { get; set; }

    public override string ToString() => $"Student(Id={Id}, Name={Name}, Age={Age}, CourseId={CourseId})";
}
