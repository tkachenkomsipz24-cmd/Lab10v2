namespace Lab10v2;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Credits { get; set; }

    public override string ToString() => $"Course(Id={Id}, Title={Title}, Credits={Credits})";
}
