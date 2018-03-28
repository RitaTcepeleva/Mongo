using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoDBApp
{
     class Program

    { public class User
        {
            public ObjectId Id { get; set; }
            public String Task { get; set; }
            public bool Done { get; set; }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("p \t список всех заданий");
            Console.WriteLine("a \t добавить задание");
            Console.WriteLine("g \t редактировать задание");
            Console.WriteLine("r \t удалить задание");
            Console.WriteLine("m \t отобразить меню");
            Console.WriteLine("e \t выход\n");
        }

        static void Main()
        {
            

            const string connect = "mongodb://localhost";
            var client = new MongoClient(connect);

            var server = client.GetServer();
            var mongoDatabase = server.GetDatabase("todolist");

            var collection = mongoDatabase.GetCollection<User>("users");
            
            PrintMenu();
            Console.Write("Команда: ");
            ConsoleKeyInfo c = Console.ReadKey();
            Console.WriteLine();
            while (true)
            {
                switch (c.Key)
                {
                    case ConsoleKey.P:
                        var users = collection.FindAll().ToList();
                        foreach (var user in users)
                        {
                            Console.WriteLine("Задание: {0}, Истинность: {1}", user.Task, user.Done);
                        }
                        break;
                    case ConsoleKey.A:
                        Console.WriteLine("Введите задание");
                        var addTask = Console.ReadLine();
                        Console.WriteLine("Введите истинность");
                        var addDone = Boolean.Parse(Console.ReadLine());
                        var newUser = new User { Done = addDone, Task = addTask };
                        collection.Insert(newUser);
                        break;

                    case ConsoleKey.G:
                        Console.WriteLine("Введите задание для редактирования");
                        var task0 = Console.ReadLine();
                        var query0 = Query<User>.EQ(x => x.Task, task0);
                        var tempUser0 = collection.Find(query0);
                        if (tempUser0 != null)
                        {
                            collection.Remove(query0);
                        }
                        Console.WriteLine("Введите исправленное задание");
                        var addTask0 = Console.ReadLine();
                        Console.WriteLine("Введите исправленный статус истинности");
                        var addDone0 = Boolean.Parse(Console.ReadLine());
                        var newUser0 = new User { Done = addDone0, Task = addTask0 };
                        collection.Insert(newUser0);
                        break;

                    case ConsoleKey.R:
                        Console.WriteLine("Введите задание для удаления\n");
                        var task1 = Console.ReadLine();
                        var query1 = Query<User>.EQ(x => x.Task, task1);
                        var tempUser1 = collection.Find(query1);
                        if (tempUser1 != null)
                        {
                            collection.Remove(query1);
                        }
                        break;
                    case ConsoleKey.M: PrintMenu(); break;
                    case ConsoleKey.E: return; break;
                }
                Console.Write("Команда: ");
                c = Console.ReadKey();
                Console.WriteLine();
            }

        }
    }
}