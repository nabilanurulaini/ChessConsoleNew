namespace Helper;

public static class ConsoleHelper {
        public static void WriteLine(object message) {
            Console.WriteLine(message);
        }
        public static void Write(object message) {
            Console.Write(message);
        }

        public static ConsoleKeyInfo ReadKey() {
            return Console.ReadKey();
        }

        public static string ReadLine() {
            return Console.ReadLine();
        }

        public static void Clear() {
            Console.Clear();
        }
    }