using System;
using System.Text;

namespace CheboksaryQuest
{
    class Question
    {
        public string Text { get; set; }
        public Option[] Options { get; set; }
    }

    class Option
    {
        public string Text { get; set; }

        public Question NextQuestion { get; set; }

        public Option(string text, Question nextQuestion)
        {
            Text = text;
            NextQuestion = nextQuestion;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Question question1 = new Question();
            Question question2 = new Question();
            Question question3 = new Question();
            Question question4 = new Question();
            Question question5 = new Question();

            question1.Text = "Добро пожаловать в Чебоксары!\nКак не прискорбно это сообщать, но ты проснулся. Тебе предстоит пережить этот день чтобы завтра проснуться вновь.";
            question1.Options = new[]
            {
                new Option("Начать собираться на работу", question2),
                new Option("Не хочу работать, буду сериалы смотреть", question3)
            };

            question2.Text = "Ну делать нечего";
            question2.Options = new[]
            {
                new Option("Топать на работу", question5)
            };

            question3.Text = "Надо бы продуктов купить: холодильник пуст";
            question3.Options = new[]
            {
                new Option("Пойти в магазин", question4)
            };

            question4.Text = "А может все-таки на работу?";
            question4.Options = new[]
            {
                new Option("На работу", question2),
                new Option("Нет уж!", null)
            };

            question5.Text = "Как поедем?";
            question5.Options = new[]
            {
                new Option("Пешком потопаю", null),
                new Option("Поеду на автобусе", null),
                new Option("Слишком сложные вопросы. Я на это не готов. Вернусь домой сериалы смотреть", question3)
            };

            ProcessQuestion(question1);

            //Console.WriteLine("Ноги болят, но ты не работе! Штраф за опоздание - 100 руб.");
            //Console.WriteLine("Ты на работе! Стоимость билетика - 50 руб.");

            Console.WriteLine("Конец!");
        }

        public static void ProcessQuestion(Question question)
        {
            Option selectedOption = AskUser(question);

            if (selectedOption != null && selectedOption.NextQuestion != null)
            {
                ProcessQuestion(selectedOption.NextQuestion);
            }
        }

        public static Option AskUser(Question question)
        {
            PrintQuestion(question.Text, question.Options);
            return GetAnswer(question.Options);
        }

        public static void PrintQuestion(string text, Option[] options)
        {
            Console.WriteLine(text);

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i].Text}");
            }
        }

        public static Option GetAnswer(Option[] options)
        {
            Option answer = null;
            string userInput;
            int numberInput;
            do
            {
                Console.WriteLine("Выбери номер действия");
                userInput = Console.ReadLine();

                if (int.TryParse(userInput, out numberInput) && IsValidIndex(numberInput - 1, options))
                {
                    answer = options[numberInput - 1];
                }
            } while (answer == null);

            return answer;
        }

        public static bool IsValidIndex(int index, object[] array)
        {
            return index <= array.Length && index >= 0;
        }
    }
}
