using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace Tetris_Windows_Mobile
{
    public class XmlSave
    {
        XmlDocument highscore;
        public XmlSave()
        {
            highscore = new XmlDocument();
            if (!System.IO.File.Exists(Constant.fPath))
            {
                createNew();
            }
        }

        public void readRecords(ListView l)
        {
            highscore.Load(Constant.fPath);
            XmlNodeList nodelist = highscore.SelectNodes("HighScoreBoard/Record");
            for (int i = 0; i < 10; i++) 
            {
                XmlNode node = nodelist[i];
                ListViewItem item = new ListViewItem(node.Attributes[0].Value);
                item.SubItems.Add(node.Attributes[1].Value);
                l.Items.Add(item);
            }
        }
        public void createNew()
        {
            XmlElement Goc = highscore.CreateElement("HighScoreBoard");
            highscore.AppendChild(Goc);
            for (int i = 0; i < 10; i++)
            {
                XmlElement Record = highscore.CreateElement("Record");
                Record.SetAttribute("Name", (i+1).ToString());
                Record.SetAttribute("Score", "0");
                Goc.AppendChild(Record);
            }
            highscore.Save(Constant.fPath);
        }
        public void clear()
        {
            highscore.Load(Constant.fPath);
            highscore.RemoveAll();
            XmlElement Goc = highscore.CreateElement("HighScoreBoard");
            highscore.AppendChild(Goc);
            for (int i = 0; i < 10; i++)
            {
                XmlElement Record = highscore.CreateElement("Record");
                Record.SetAttribute("Name", (i + 1).ToString());
                Record.SetAttribute("Score", "0");
                Goc.AppendChild(Record);
            }
            highscore.Save(Constant.fPath);
        }
        public int saveRecords(int scores)
        {
            int res = 0;
            highscore.Load(Constant.fPath);
            int[] list = new int[10];
            for(int i = 0; i < 10;i++)
                list[i] = 0;
            XmlNodeList nodelist = highscore.SelectNodes("HighScoreBoard/Record");
            for (int i = 0; i < 10; i++)
            {
                XmlNode node = nodelist[i];
                
                if (int.Parse(node.Attributes[0].Value) > 0 && int.Parse(node.Attributes[0].Value) <= 10)
                    list[int.Parse(node.Attributes[0].Value) - 1] = int.Parse(node.Attributes[1].Value);
            }
            for (int i = 0; i < 10; i++)
                if(i == 0 && list[i] < scores) 
                {
                    for(int x = 9; x > 0; x--)
                        list[x] = list[x - 1];
                    list[i] = scores;
                    res = 1;
                    i = 10;
                }
                else if (i < 9 && list[i] > scores && list[i + 1] <= scores)
                {
                    for (int x = 9; x > i; x--)
                        list[x] = list[x - 1];
                    list[i + 1] = scores;
                    res = i + 2;
                    i = 10;
                }
                else if (i == 9 && list[i] <= scores && list[i - 1] > scores)
                {
                    list[i] = scores;
                    res = 10;
                    i = 10;
                }
            if (res > 0)
            {
                highscore.RemoveAll();
                XmlElement Goc = highscore.CreateElement("HighScoreBoard");
                highscore.AppendChild(Goc);
                for (int i = 0; i < 10; i++)
                {
                    XmlElement Record = highscore.CreateElement("Record");
                    Record.SetAttribute("Name", (i + 1).ToString());
                    Record.SetAttribute("Score", list[i].ToString());
                    Goc.AppendChild(Record);
                }
                highscore.Save(Constant.fPath);
            }
            return res;
            

        }

    }
}
