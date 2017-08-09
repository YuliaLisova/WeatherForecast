using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Resources;


namespace WindowsFormsApplication22
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> cities;
        string path;
        XmlDocument xmldoc;

        public Form1(Dictionary<string, string> towns)
        {
            InitializeComponent();

            button1.Click += new EventHandler(button1_click);
            cities = towns;
            xmldoc = new XmlDocument();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            foreach (KeyValuePair<string, string> kvp in cities)
            {
                listBox3.Items.Add(kvp.Key);
                listBox2.Items.Add(kvp.Value);
                label1.Text = "Выделите  Ваш город и нажмите кнопку \"Прогноз\" ";
            }


        }


        void button1_click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1)
            {

                listBox1.Items.Clear();

            try
            {
                xmldoc.Load(path);
            }

            catch
            {
                MessageBox.Show("Невозможно получить данные. Проверьте подключение к интернету");
            }

            
                XmlNodeList temp = xmldoc.GetElementsByTagName("FORECAST");


                foreach (XmlElement node in temp)
                {
                    string n = node.Name + " " + node.Value;
                    listBox1.Items.Add(n);

                    foreach (XmlAttribute a in node.Attributes)
                    {
                        string s = a.Name + ":" + a.Value;
                        listBox1.Items.Add("    " + s);

                    }

                    foreach (XmlNode table in node.ChildNodes)
                    {

                        string tablename = table.Name;
                        string resultTablename = table.Name;

                        switch (tablename)
                        {
                            case "WIND": resultTablename = "ВЕТЕР";
                                break;
                            case "PHENOMENA": resultTablename = "ОСАДКИ";
                                break;
                            case "PRESSURE": resultTablename = "ДАВЛЕНИЕ";
                                break;
                            case "RELWET": resultTablename = "ВЛАЖНОСТЬ";
                                break;
                            case "TEMPERATURE": resultTablename = "ТЕМПЕРАТУРА";
                                break;
                        }

                        listBox1.Items.Add("         " + resultTablename);


                        foreach (XmlAttribute attr in table.Attributes)
                        {
                            string result_name = attr.Name;
                            string result_value = attr.Value;
                            string name = attr.Name;
                            string value = attr.Value;

                            if (name == "cloudiness")
                            {
                                result_name = "облачность";


                                switch (value)
                                {
                                    case "0": result_value = "ясно";
                                        break;
                                    case "1": result_value = "малооблачно";
                                        break;
                                    case "2": result_value = "облачно";
                                        break;
                                    case "3": result_value = "пасмурно";
                                        break;
                                }
                            }

                            if (name == "direction")
                            {
                                result_name = "направление";
                                switch (value)
                                {
                                    case "0": result_value = "северный";
                                        break;
                                    case "1": result_value = "северо-восточный";
                                        break;
                                    case "2": result_value = "восточный";
                                        break;
                                    case "3": result_value = "юго-восточный";
                                        break;
                                    case "4": result_value = "южный";
                                        break;
                                    case "5": result_value = "юго-западный";
                                        break;
                                    case "6": result_value = "западный";
                                        break;
                                    case "7": result_value = "северо-западный";
                                        break;

                                }

                            }

                            if (name == "precipitation")
                            {
                                result_name = "осадки";
                                switch (value)
                                {
                                    case "10": result_value = "без осадков";
                                        break;
                                    case "9": result_value = " ";
                                        break;
                                    case "8":
                                        {
                                            if (table.Attributes["spower"].Value == "0")
                                                result_value = "возможна гроза";
                                            if (table.Attributes["spower"].Value == "1")
                                                result_value = "гроза";
                                        }
                                        break;

                                    case "7":
                                        {
                                            if (table.Attributes["rpower"].Value == "0")
                                                result_value = "возможен снег";
                                            if (table.Attributes["rpower"].Value == "1")
                                                result_value = "снег";
                                        }

                                        break;

                                    case "6":
                                        {
                                            if (table.Attributes["rpower"].Value == "0")
                                                result_value = "возможен снег";
                                            if (table.Attributes["rpower"].Value == "1")
                                                result_value = "снег";
                                        }
                                        break;
                                    case "5":
                                        {

                                            if (table.Attributes["rpower"].Value == "0")
                                                result_value = "возможен ливень";
                                            if (table.Attributes["rpower"].Value == "1")
                                                result_value = "ливень";
                                        }

                                        break;

                                    case "4":
                                        {
                                            if (table.Attributes["rpower"].Value == "0")
                                                result_value = "возможен дождь";
                                            if (table.Attributes["rpower"].Value == "1")
                                                result_value = "дождь";
                                        }
                                        break;
                                }
                            }

                            string s = result_name + ":" + result_value;
                            listBox1.Items.Add("           " + s);
                        }

                    }

                }

                label2.Text = listBox3.Items[listBox2.SelectedIndex].ToString();
            }

            else
            {
                MessageBox.Show("Необходимо выделить город");
            }
            
        }


        private void listBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Выделите код, соответсвующий данному городу и нажмите кнопку \"Прогноз\"");
            listBox2.SelectedIndex = listBox3.SelectedIndex;

            string city = " ";
            try
            {
                city = listBox2.Items[listBox3.SelectedIndex].ToString();
            }


            catch
            {
                MessageBox.Show("Необходимо выделить город");
            }

            path = "http://informer.gismeteo.ua/xml/" + city + ".xml";
        }
        

    }
}






