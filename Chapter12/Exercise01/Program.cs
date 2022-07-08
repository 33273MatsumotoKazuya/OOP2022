using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Exercise01 {

    [XmlRoot("employee")]
    public class Employee {
        [XmlElement(ElementName = "id")]
        public int Id { get; set; }
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "hiredate")]
        public DateTime HireDate { get; set; }
        public override string ToString() {
            return string.Format("[Id={0}, Name={1}, HireDate={2}]",
                                  Id, Name, HireDate);
        }
    }

    class Program {
        static void Main(string[] args) {
            Exercise1_1("employee.xml");

            // これは確認用
            Console.WriteLine(File.ReadAllText("employee.xml"));
            Console.WriteLine();

            Exercise1_2("employees.xml");
            Exercise1_3("employees.xml");
            Console.WriteLine();

            Exercise1_4("employees.json");

            // これは確認用
            Console.WriteLine(File.ReadAllText("employees.json"));
        }

        private static void Exercise1_1(string v) {
            var emp = new Employee {
                Id = 123,
                Name = "出井　秀行",
                HireDate = new DateTime(2001, 5, 10),
            };

            var settings = new XmlWriterSettings {
                Encoding = new System.Text.UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ",
            };

            //シリアル化
            using (var writer = XmlWriter.Create(v, settings)) {
                var serializer = new XmlSerializer(emp.GetType()); //P185
                serializer.Serialize(writer, emp);
            }

            //逆シリアル化
            using (var reader = XmlReader.Create(v)) {
                var serializer = new XmlSerializer(typeof(Employee));
                var employee = serializer.Deserialize(reader) as Employee;
                Console.WriteLine(employee);
            }
        }

        private static void Exercise1_2(string v) {
            var employees = new Employee[] {
               new Employee {
                  Id = 123,
                  Name = "出井　秀行",
                  HireDate = new DateTime(2001, 5, 10),
               },
               new Employee {
                  Id = 124,
                  Name = "石川 大貴",
                  HireDate = new DateTime(2001, 5, 10),
               },
            };

            var settings = new XmlWriterSettings {
                Encoding = new System.Text.UTF8Encoding(false),
                Indent = true,
                IndentChars = "  ",
            };

            using (var writer = XmlWriter.Create(v, settings)) {
                var serializer = new XmlSerializer(employees.GetType());
                serializer.Serialize(writer, employees);
            }
        }

        private static void Exercise1_3(string v) {
            using (var reader = XmlReader.Create(v)) {
                var serializer = new XmlSerializer(typeof(Employee[]));
                var employees = serializer.Deserialize(reader) as Employee[];
                foreach (var employee in employees) {
                    Console.WriteLine(employee);
                }
            }
        }

        private static void Exercise1_4(string v) {
            var employees = new Employee[] {
               new Employee {
                  Id = 123,
                  Name = "出井　秀行",
                  HireDate = new DateTime(2001, 5, 10),
               },
               new Employee {
                  Id = 124,
                  Name = "石川 大貴",
                  HireDate = new DateTime(2001, 5, 10),
               },
            };

            using (var stream = new FileStream(v, FileMode.Create, FileAccess.Write)) {
                var serializer = new DataContractJsonSerializer(employees.GetType());
                serializer.WriteObject(stream, employees);
            }
        }
    }
}
