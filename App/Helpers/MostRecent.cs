using App.Models;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace App.Helpers
{
    public class MostRecent
    {
        public static List<Story> getStoryList()
        {
            List<Story> stories = new List<Story>();
            WebClient client = new WebClient();
            using (Stream data = client.OpenRead("https://www.wired.com/most-recent/"))
            {
                using (StreamReader reader = new StreamReader(data))
                {
                    string content = reader.ReadToEnd();
                    string pattern = @"<li class=\""archive-item-component\"">([\s\S]*?)<\/li>";
                    MatchCollection matches = Regex.Matches(content, pattern, RegexOptions.ECMAScript);
                    List<string> urls = new List<string>();
                    for(int i = 0; i < 5; i++)
                    {
                        var c = matches[i].ToString();
                        string t = @"<h2 class=\""archive-item-component__title\"">([\s\S]*?)<\/h2>";
                        string co = @"<p class=\""archive-item-component__desc\"">([\s\S]*?)<\/p>";
                        string ti = @"<time>([\s\S]*?)<\/time>";
                        MatchCollection m_title = Regex.Matches(c, t, RegexOptions.ECMAScript);
                        MatchCollection m_content = Regex.Matches(c, co, RegexOptions.ECMAScript);
                        MatchCollection m_time = Regex.Matches(c, ti, RegexOptions.ECMAScript);
                        string s_title = m_title.Count > 0 ? m_title[0].Groups[1].Value.ToString() : "";
                        string s_content  = m_content.Count > 0 ? m_content[0].Groups[1].Value.ToString() : "";
                        string s_time  = m_time.Count > 0 ? m_time[0].Groups[1].Value.ToString() : "";
                        if (m_content.Count > 0 && m_title.Count > 0 && m_time.Count>0)
                        {
                            Story s = new()
                            {
                                Title = s_title,
                                Content = s_content,
                                StoryDate=s_time
                            };
                            stories.Add(s);
                        }
                    }

                }
            }
            return stories;
        }
    }
}
