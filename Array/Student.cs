namespace Array
{
    public class Student
    {
        private string name;
        private int age;

        public Student(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public override string ToString()
        {
            return $"{{Student name='{name}', age={age}}}";
        }
    }
}