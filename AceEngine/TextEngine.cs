using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace objection.AceEngine
{
    public class TextEngine
    {
        private List<Person> people;

        private string file;

        public List<Person> GetPeople()
        {
            if (people != null) return people;

            ReadFile();
            string[] ps = ExtractTextArrayBetween(file, "PERSON_BEGIN", "PERSON_END").ToArray();
            var personstrings = ExtractPersonStrings(ps);
            
            throw new NotImplementedException();
        }

        private List<PersonString> ExtractPersonStrings(IEnumerable<string> ps)
        {
            var personStrings = new List<PersonString>();
            
            foreach (var p in ps)
            {
                var personString = new PersonString();
                string tempString = "";

                string[] emotes = ExtractTextArrayBetween(p, "EMOTE_BEGIN", "EMOTE_END").ToArray();
                personString.emotes = ExtractPersonEmotes(emotes).ToArray();
                
                for (int i = 0; i < p.Length; i++)
                {
                    if (char.IsWhiteSpace(p, i))
                    {
                        if (personString.name == null) personString.name = tempString.Trim();
                        tempString = "";
                    }
                    else tempString += p[i];

                    switch(tempString)
                    {
                        case "filename":
                            string filenameS = p.Substring(i+1, p.Length - (i + 1));
                            string filenameText = ExtractTextBetween(filenameS, "(", ")");
                            personString.filename = filenameText;
                            break;
                        case "background":
                            string backgroundS = p.Substring(i+1, p.Length - (i+1));
                            string backgroundText = ExtractTextBetween(backgroundS, "(", ")");
                            personString.background = backgroundText;
                            break;
                        case "desk":
                            string deskS = p.Substring(i+1, p.Length - (i+1));
                            string deskText = ExtractTextBetween(deskS, "(", ")");
                            personString.desk = deskText;
                            break;
                    }
                }

                personStrings.Add(personString);
            }

            return personStrings;
        }

        private List<PersonEmoteString> ExtractPersonEmotes(IEnumerable<string> emotes)
        {
            return null;
        }

        private void ReadFile()
        {
            const string filename = "people.ace";

            file = File.ReadAllText(filename);
        }
        
        /// <summary>
        /// Gets all people's "code", and seperates them
        /// beginning from <param name="startAt">startAt</param> till <param name="endAt">endAt</param>
        /// and leaves them out.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> ExtractTextArrayBetween(string content, string startAt, string endAt)
        {
            string tempString = "";
            
            var personcode = new List<string>();

            for (int i = 0; i < content.Length; i++)
            {
                if (char.IsWhiteSpace(content, i)) tempString = "";
                else tempString += content[i];

                if (tempString != startAt) continue;
                
                int startIndex = i + 1;
                int length = ExtractTillFind(content, startIndex, endAt);
                    
                if(length == -1)
                    throw new Exception("Something didn't work.");


                int ilength = length;
                //if (startIndex + length > content.Length)
                 //   ilength = content.Length-startIndex;
                personcode.Add(content.Substring(startIndex, ilength));
                    
                i = startIndex + length;
                tempString = string.Empty;
            }

            return personcode;
        }

        private string ExtractTextBetween(string content, string startAt, string endAt)
        {
            string tempString = "";
            int wordStart = 0;

            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] != startAt[wordStart]) tempString = "";
                else
                {
                    wordStart++;
                    tempString += content[i];
                }

                if (tempString != startAt) continue;
                
                int startIndex = i + 1;
                int length = ExtractTillFind(content, startIndex, endAt);

                if (length == -1)
                    return null;
                    
                return content.Substring(startIndex, length).Trim();
            }

            return null;
        }
        
        private int ExtractTillFind(string content, int startIndex, string endAt)
        {
            int length = 0;
            int tempLength = 0;
            string tempString = "";
            int wordStart = 0;
            
            for (int i = startIndex; i < content.Length; i++)
            {
                if (content[i] != endAt[wordStart]) tempString = "";
                else
                {
                    wordStart++;
                    tempString += content[i];
                    length = tempLength;
                }
                
                tempLength++;
                
                if (tempString == endAt)
                {
                    return length;
                }
            }

            return -1;
        }
    }

    public struct PersonString
    {
        public string name;
        public string filename;
        public string background;
        public string desk;
        public PersonEmoteString[] emotes;
    }

    public struct PersonEmoteString
    {
        public string name;
        public string[] frames;
        public string[] idleframes;
        public string[] blinkframes;
    }
}