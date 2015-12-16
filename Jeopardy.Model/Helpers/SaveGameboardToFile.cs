using System.Xml.Linq;
using Jeopardy.Model.Interfaces;

namespace Jeopardy.Model.Helpers
{
    public class SaveGameboardToFile
    {
        public static void SaveGameboard(Gameboard gameboardToSave)
        {
            XDocument doc = new XDocument(new XElement("Round",
                                                       new XElement("RoundName", gameboardToSave.QuestionPackName),
                                                       new XElement("Guid", gameboardToSave.UniqueIdentifier),
                                                       new XElement("Categories",
                                                           GetCategory(gameboardToSave, 0),
                                                           GetCategory(gameboardToSave, 1),
                                                           GetCategory(gameboardToSave, 2),
                                                           GetCategory(gameboardToSave, 3),
                                                           GetCategory(gameboardToSave, 4),
                                                           GetCategory(gameboardToSave, 5))));



            string fileName = gameboardToSave.UniqueIdentifier.ToString();

            string fileNameAndExtension =
                 Constants.APP_NAME + fileName + ".xml";

            var pathAndFile = Constants.APP_DATA_LOCATION + System.IO.Path.DirectorySeparatorChar + fileNameAndExtension;

            doc.Save(pathAndFile);
        }

        private static XElement GetCategory(Gameboard gameboard, int categoryIndex)
        {
            var category = gameboard.Categories[categoryIndex];
            var xml = new XElement("Category",
                new XAttribute("title", category.CategoryName),
                GetQuestion(gameboard, categoryIndex, 0),
                GetQuestion(gameboard, categoryIndex, 1),
                GetQuestion(gameboard, categoryIndex, 2),
                GetQuestion(gameboard, categoryIndex, 3),
                GetQuestion(gameboard, categoryIndex, 4));
            return xml;
        }

        private static XElement GetQuestion(Gameboard gameboard, int categoryIndex, int questionIndex)
        {
            var question = gameboard.Categories[categoryIndex].Questions[questionIndex];
            var xml = new XElement("Question",
                    new XElement("Type", "Text"),
                    new XElement("Info", question.Source),
                    new XElement("Answer", question.Answer),
                    new XElement("Rating", question.Rating));

            return xml;
        }
    }
}
