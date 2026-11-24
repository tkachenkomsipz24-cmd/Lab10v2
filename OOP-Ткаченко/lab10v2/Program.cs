using System.Diagnostics;
using System.Text;
using Lab10v2;

Console.OutputEncoding = Encoding.UTF8;

const int Iterations = 100_000;

var courses = DataSeed.GetCourses();
var students = DataSeed.GetStudents();
var summary = new List<(string Title, double QueryMs, double MethodMs)>();

Run(
    "Запит 1: студенти до 20 років на курсі \"Programming Basics\"",
    querySyntax: () =>
        (from s in students
         join c in courses on s.CourseId equals c.Id
         where s.Age < 20 && c.Title == "Programming Basics"
         select s).ToList(),
    methodSyntax: () =>
        students
            .Join(courses, s => s.CourseId, c => c.Id, (s, c) => new { s, c })
            .Where(x => x.s.Age < 20 && x.c.Title == "Programming Basics")
            .Select(x => x.s)
            .ToList(),
    print: list => list.ForEach(s => Console.WriteLine($"  {s}")));

Run(
    "Запит 2: студенти разом з назвами курсів (Join)",
    querySyntax: () =>
        (from s in students
         join c in courses on s.CourseId equals c.Id
         select new { StudentName = s.Name, CourseTitle = c.Title }).ToList(),
    methodSyntax: () =>
        students
            .Join(courses, s => s.CourseId, c => c.Id,
                  (s, c) => new { StudentName = s.Name, CourseTitle = c.Title })
            .ToList(),
    print: list => list.ForEach(x => Console.WriteLine($"  {x.StudentName,-22} -> {x.CourseTitle}")));

Run(
    "Запит 3: кількість студентів на кожному курсі (GroupBy)",
    querySyntax: () =>
        (from s in students
         group s by s.CourseId into g
         join c in courses on g.Key equals c.Id
         select new { CourseTitle = c.Title, Count = g.Count() }).ToList(),
    methodSyntax: () =>
        students
            .GroupBy(s => s.CourseId)
            .Join(courses, g => g.Key, c => c.Id,
                  (g, c) => new { CourseTitle = c.Title, Count = g.Count() })
            .ToList(),
    print: list => list.ForEach(x => Console.WriteLine($"  {x.CourseTitle,-20} : {x.Count}")));

Run(
    "Запит 4: наймолодший студент",
    querySyntax: () =>
        (from s in students
         orderby s.Age
         select s).First(),
    methodSyntax: () => students.MinBy(s => s.Age)!,
    print: s => Console.WriteLine($"  {s}"));

Console.WriteLine();
Console.WriteLine($"=== Підсумок (середній час одного виконання, {Iterations:N0} повторів) ===");
Console.WriteLine($"{"Запит",-62} {"Query, мс",12} {"Method, мс",12}");
foreach (var (title, q, m) in summary)
    Console.WriteLine($"{title,-62} {q,12:F6} {m,12:F6}");

var slowest = summary.OrderByDescending(x => Math.Max(x.QueryMs, x.MethodMs)).First();
Console.WriteLine();
Console.WriteLine($"Найдорожчий за часом запит: {slowest.Title}");

void Run<T>(string title, Func<T> querySyntax, Func<T> methodSyntax, Action<T> print)
{
    Console.WriteLine();
    Console.WriteLine($"=== {title} ===");

    var sw = Stopwatch.StartNew();
    var resQuery = querySyntax();
    sw.Stop();
    double firstQuery = sw.Elapsed.TotalMilliseconds;

    sw.Restart();
    var resMethod = methodSyntax();
    sw.Stop();
    double firstMethod = sw.Elapsed.TotalMilliseconds;

    Console.WriteLine("Результат (синтаксис запитів):");
    print(resQuery);
    Console.WriteLine("Результат (синтаксис методів):");
    print(resMethod);
    Console.WriteLine($"Перший виклик: query = {firstQuery:F4} мс, method = {firstMethod:F4} мс");

    double avgQuery = Benchmark(querySyntax);
    double avgMethod = Benchmark(methodSyntax);
    Console.WriteLine($"Середнє ({Iterations:N0} повторів): query = {avgQuery:F6} мс, method = {avgMethod:F6} мс");

    summary.Add((title, avgQuery, avgMethod));
}

double Benchmark<T>(Func<T> action)
{
    for (int i = 0; i < 1000; i++) action();

    var sw = Stopwatch.StartNew();
    for (int i = 0; i < Iterations; i++) action();
    sw.Stop();

    return sw.Elapsed.TotalMilliseconds / Iterations;
}
