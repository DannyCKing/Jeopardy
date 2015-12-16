using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Jeopardy.Model.Helpers
{
    public class GetGameboardFromFile
    {
        public static Gameboard GetGameboard(string fileName, bool useDesktopAsLocation = false)
        {
            string filePathAndName = null;
            if (useDesktopAsLocation)
            {
                string desktopPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                filePathAndName = desktopPath + System.IO.Path.DirectorySeparatorChar + fileName;
            }
            else
            {
                filePathAndName = fileName;
            }

            XDocument doc = XDocument.Load(filePathAndName);

            var round = doc.Element("Round");

            var roundName = round.Element("RoundName").Value.ToString();
            Guid guid;
            try
            {
                guid = Guid.Parse(round.Element("Guid").Value.ToString());
            }
            catch (Exception e)
            {
                guid = Guid.NewGuid();
            }
            var categoriesXML = round.Element("Categories").Elements();

            List<Category> categories = new List<Category>();
            foreach (var categoryXML in categoriesXML)
            {
                //Get each category
                string categoryTitle = categoryXML.Attribute("title").Value;
                var catSquare = new CategoryHeader(categoryTitle);
                List<Question> questions = new List<Question>();
                foreach (var questionXML in categoryXML.Elements())
                {
                    var questionType = "text";
                    var questionText = string.Empty;
                    var answerText = string.Empty;
                    int rating = 0;

                    try
                    {
                        questionType = questionXML.Element("Type").Value;
                        questionText = questionXML.Element("Info").Value;
                        answerText = questionXML.Element("Answer").Value;
                        rating = Int32.Parse(questionXML.Element("Rating").Value);
                    }
                    catch (Exception e)
                    {

                    }
                    Question question = new Question(questionText, answerText, rating);
                    questions.Add(question);
                }
                Category category = new Category(categoryTitle, questions[0], questions[1], questions[2], questions[3], questions[4]);
                categories.Add(category);
            }
            return new Gameboard(roundName, guid, categories[0], categories[1], categories[2], categories[3], categories[4], categories[5]);
        }
    }
}
