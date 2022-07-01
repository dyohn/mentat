using System.Collections.Generic;
using Mentat.Domain.IService;
using Mentat.Domain.Models;

namespace Mentat.Domain.Service
{
    public class StudentService : IStudentService
    {
        public StudentVM GetStudentVM()
        {
            // todo: actually get the data from the DB, once it is merged
            var vm = new StudentVM
            {
                AvailableCards = new List<FlashCardVM>
                {
                    new FlashCardVM
                    {
                        CardID = 1,
                        CardQuestion = "This is a test Question???",
                        HiddenCardAnswer = "Yep, sure is.",
                        CardColor = "221,160,221"
                    },
                    new FlashCardVM
                    {
                        CardID = 2,
                        CardQuestion = "This is a test Question AGAIN???",
                        HiddenCardAnswer = "Yep, sure is, again!",
                        CardColor = "200,190,221"
                    },
                    new FlashCardVM
                    {
                        CardID = 3,
                        CardQuestion = "What is your favorite color?",
                        HiddenCardAnswer = "Purple!",
                        CardColor = "150,190,221"
                    },
                    new FlashCardVM
                    {
                        CardID = 4,
                        CardQuestion = "How many days are there in a leap year?",
                        HiddenCardAnswer = "366. Come on, this is easy.",
                        CardColor = "150,120,221"
                    }
                }
            };

            return vm;
        }
    }
}
