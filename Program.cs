using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Öğretim Görevlisi oluşturma
            Instructor instructor1 = new Instructor("Dr. Mehmet Çelik", 101);
            Instructor instructor2 = new Instructor("Prof. Zeynep Yıldız", 102);
            Instructor instructor3 = new Instructor("Doç. Ahmet Karaca", 103);

            // Ders oluşturma
            Course course1 = new Course("Algoritmalar", 4, instructor1);
            Course course2 = new Course("Veri Yapıları", 3, instructor2);
            Course course3 = new Course("Yapay Zeka", 4, instructor3);
            Course course4 = new Course("Makine Öğrenimi", 4, instructor1);
            Course course5 = new Course("Veritabanı Yönetimi", 3, instructor2);

            // Derslerin öğretim görevlilerine atanması
            instructor1.Courses.Add(course1);
            instructor1.Courses.Add(course4);
            instructor2.Courses.Add(course2);
            instructor2.Courses.Add(course5);
            instructor3.Courses.Add(course3);

            // Öğrenci oluşturma
            Student student1 = new Student("Elif Demir", 201);
            Student student2 = new Student("Can Yılmaz", 202);
            Student student3 = new Student("Fatma Ak", 203);

            // Kullanıcıdan öğretim görevlisi seçmesini istemek
            Console.WriteLine("Öğretim Görevlisi Seçin:");
            List<Instructor> instructors = new List<Instructor> { instructor1, instructor2, instructor3 };
            for (int i = 0; i < instructors.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {instructors[i].Name}");
            }
            int instructorChoice = int.Parse( Console .ReadLine()) - 1;
            Instructor selectedInstructor = instructors[instructorChoice];

            // Seçilen öğretim görevlisinin derslerini gösterme
            Console.WriteLine($"\n{selectedInstructor.Name}'in Verdiği Dersler:");
            for (int i = 0; i < selectedInstructor.Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {selectedInstructor.Courses[i].Name} (Kredi: {selectedInstructor.Courses[i].Credit})");
            }

            // Kullanıcıdan ders seçmesini istemek
            Console.WriteLine("\nBir ders seçin:");
            int courseChoice = int.Parse( Console .ReadLine()) - 1;
            Course selectedCourse = selectedInstructor.Courses[courseChoice];

            // Seçilen öğretim görevlisi ve ders bilgilerini gösterme
            Console.WriteLine($"\nSeçilen Öğretim Görevlisi: {selectedInstructor.Name}");
            Console.WriteLine($"Seçilen Ders: {selectedCourse.Name}, Kredi: {selectedCourse.Credit}");

            // Öğrencilerin derse kayıt edilmesi
            Console.WriteLine("\nDerse öğrenci eklemek istiyor musunuz? (e/h)");
            string addStudentResponse = Console.ReadLine()?.ToLower();
            if (addStudentResponse == "e")
            {
                List<Student> students = new List<Student> { student1, student2, student3 };
                Console.WriteLine("\nKayıt edilecek öğrenciyi seçin:");
                for (int i = 0; i < students.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {students[i].Name}");
                }
                int studentChoice = int.Parse(Console.ReadLine()) - 1;
                Student selectedStudent = students[studentChoice];
                selectedCourse.RegisterStudent(selectedStudent);

                // Kayıt edilen öğrenci bilgilerini gösterme
                Console.WriteLine($"\nDerse Kayıt Edilen Öğrenci: {selectedStudent.Name}");
            }

            // Dersin tüm bilgilerini ve kayıtlı öğrencileri gösterme
            Console.WriteLine("\nDers Bilgileri:");
            selectedCourse.ShowCourseInfo();
        }
    }

    // Öğretim Görevlisi ve Öğrenci arasında ortak olan özellikleri ve metotları tanımlar
    public interface IPerson
    {
        void ShowInfo(); // Bilgi gösterme işlemi
    }

    // Person sınıfı (Öğrenci ve Öğretim Görevlisi için temel sınıf)
    public abstract class Person : IPerson
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public Person(string name, int id)
        {
            Name = name;
            ID = id;
        }

        // ShowInfo metodu her sınıfta özelleştirilecektir
        public abstract void ShowInfo();
    }

    // Öğrenci Sınıfı
    public class Student : Person
    {
        public Student(string name, int id) : base(name, id) { }

        public override void ShowInfo()
        {
            Console.WriteLine($"Öğrenci Adı: {Name}, ID: {ID}");
        }
    }

    // Öğretim Görevlisi Sınıfı
    public class Instructor : Person
    {
        public List<Course> Courses { get; set; }

        public Instructor(string name, int id) : base(name, id)
        {
            Courses = new List<Course>();
        }

        public override void ShowInfo()
        {
            Console.WriteLine($"Öğretim Görevlisi Adı: {Name}, ID: {ID}");
        }
    }

    // Ders Sınıfı
    public class Course
    {
        public string Name { get; set; }
        public int Credit { get; set; }
        public Instructor Instructor { get; set; }
        public List<Student> RegisteredStudents { get; set; }

        public Course(string name, int credit, Instructor instructor)
        {
            Name = name;
            Credit = credit;
            Instructor = instructor;
            RegisteredStudents = new List<Student>();
        }

        // Dersin bilgilerini ve öğretim görevlisini gösterir
        public void ShowCourseInfo()
        {
            Console.WriteLine($"Ders Adı: {Name}, Kredi: {Credit}");
            Console.WriteLine($"Öğretim Görevlisi: {Instructor.Name}");
            Console.WriteLine("Kayıtlı Öğrenciler:");
            foreach (var student in RegisteredStudents)
            {
                Console.WriteLine($" - {student.Name}");
            }
        }

        // Öğrenciyi derse kayıt eder
        public void RegisterStudent(Student student)
        {
            RegisteredStudents.Add(student);
            Console.WriteLine($"{student.Name} dersi {Name}'a kayıt oldu.");
        }
    }
}
