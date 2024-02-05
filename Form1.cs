using System;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace MyMetars
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            FormatUtcLabel(null, null);
            StartUtcLabelTimer();

        }

        private void FormatUtcLabel(object sender, EventArgs e)
        {
            String year = (DateTime.UtcNow.Year - 2000).ToString("D2");

            this.labelUTC.Text = DateTime.UtcNow.Month.ToString("D2") + "/" + DateTime.UtcNow.Day.ToString("D2") + "/" + year + " " + DateTime.UtcNow.Hour.ToString("D2") + ":" + DateTime.UtcNow.Minute.ToString("D2") + ":" + DateTime.UtcNow.Second.ToString("D2") + "Z";

        }

        private void StartUtcLabelTimer()
        {
            Timer utcLabelTimer = new Timer();

            utcLabelTimer.Interval = 1000;
            utcLabelTimer.Tick += new EventHandler(FormatUtcLabel);
            utcLabelTimer.Start();

        }

        private WebResponse MakeHttpRequest(String url)
        {
            try
            {
                WebRequest myRequest = WebRequest.Create(url);
                myRequest.Method = "POST";
                WebResponse response = myRequest.GetResponse();

                return response;
            }
            catch (WebException err)
            {
                this.toolStripStatusLabel.Text = "WebException Error Occurred";
                MessageBox.Show(err.Message);
            }
            catch (NotSupportedException err)
            {
                this.toolStripStatusLabel.Text = "NotSupportedException Error Occurred";
                MessageBox.Show(err.Message);
            }

            return null;
        }

        private void AttributeWithTab(XmlAttribute a)
        {
            this.textBoxReport.AppendText("\t");
            this.textBoxReport.AppendText(a.Name);
            this.textBoxReport.AppendText(" ");
            this.textBoxReport.AppendText(a.InnerText);
            this.textBoxReport.AppendText(Environment.NewLine);
        }

        private void NodeWithTab(XmlNode n)
        {
            this.textBoxReport.AppendText("\t");
            this.textBoxReport.AppendText(n.Name);
            this.textBoxReport.AppendText(" ");
            this.textBoxReport.AppendText(n.InnerText);
            this.textBoxReport.AppendText(Environment.NewLine);
        }

        private void ElementAndInnerText(XmlElement e)
        {
            this.textBoxReport.AppendText(e.Name);

            switch (e.Name)
            {
                case "temp_c":
                case "dewpoint_c":
                case "maxT_c":
                case "minT_c":
                    {
                        this.textBoxReport.AppendText(" ");
                        this.textBoxReport.AppendText(e.InnerText);
                        this.textBoxReport.AppendText(" ");
                        this.textBoxReport.AppendText(ConvertUnit.CelsiusToFahrenheit(Convert.ToDouble(e.InnerText)).ToString("F2"));
                        this.textBoxReport.AppendText(Environment.NewLine);

                        break;
                    }

                case "elevation_m":
                    {
                        this.textBoxReport.AppendText(" ");
                        this.textBoxReport.AppendText(e.InnerText);
                        this.textBoxReport.AppendText(" ");
                        this.textBoxReport.AppendText(ConvertUnit.MeterToFeet(Convert.ToDouble(e.InnerText)).ToString("F2"));
                        this.textBoxReport.AppendText(Environment.NewLine);

                        break;
                    }

                case "quality_control_flags":
                    {
                        textBoxReport.AppendText(Environment.NewLine);
                        foreach (XmlNode n in e.ChildNodes)
                        {
                            NodeWithTab(n);
                        }

                        break;
                    }

                default:
                    {
                        this.textBoxReport.AppendText(" ");
                        this.textBoxReport.AppendText(e.InnerText);
                        this.textBoxReport.AppendText(Environment.NewLine);

                        break;
                    }
            }

        }

        private void FormatMetarText(XmlNodeList nodes)
        {
            if (nodes.Count > 0)
            {
                foreach (XmlNode node in nodes)
                {
                    String prevName = "";
                    foreach (XmlElement element in node)
                    {
                        if (element.HasAttributes)
                        {
                            if (String.Compare(prevName, element.Name) != 0)
                            {
                                prevName = element.Name;
                                this.textBoxReport.AppendText(element.Name);
                                this.textBoxReport.AppendText(Environment.NewLine);
                            }
                            foreach (XmlAttribute attribute in element.Attributes)
                            {
                                AttributeWithTab(attribute);
                            }
                        }
                        else
                        {
                            ElementAndInnerText(element);
                        }
                    }
                    this.textBoxReport.AppendText(Environment.NewLine);
                }

                this.toolStripStatusLabel.Text = "Station Returned";
                this.textBoxStation.Focus();
            }
            else
            {
                this.toolStripStatusLabel.Text = "No Data For Station";
            }

        }

        private void FormatTafText(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                foreach (XmlElement element in node)
                {
                    if (element.Name == "forecast")
                    {
                        this.textBoxReport.AppendText(element.Name);
                        this.textBoxReport.AppendText(Environment.NewLine);
                        foreach (XmlNode n in element.ChildNodes)
                        {
                            if (n.Name == "sky_condition")
                            {
                                this.textBoxReport.AppendText("\t" + n.Name);
                                this.textBoxReport.AppendText(Environment.NewLine);
                                foreach (XmlAttribute a in n.Attributes)
                                {
                                    this.textBoxReport.AppendText("\t");
                                    AttributeWithTab(a);
                                }
                            }
                            else
                            {
                                NodeWithTab(n);
                            }
                        }
                    }
                    else
                    {
                        ElementAndInnerText(element);
                    }
                }
            }

        }

        private void buttonGet_Click(object sender, EventArgs e)
        {
            this.textBoxStation.Text = this.textBoxStation.Text.ToUpper();
            this.textBoxReport.Clear();

            WebResponse response = MakeHttpRequest("http://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=1&stationString=" + textBoxStation.Text);
            if (response != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();

                    doc.Load(response.GetResponseStream());

                    FormatMetarText(doc.SelectNodes("//METAR"));

                    response.Close();
                }
                catch (WebException err)
                {
                    this.toolStripStatusLabel.Text = "WebException Error Occurred";
                    MessageBox.Show(err.Message);
                }
                catch (XmlException err)
                {
                    this.toolStripStatusLabel.Text = "XML Error Occurred";
                    MessageBox.Show(err.Message);
                }

            }

            response = MakeHttpRequest("http://www.aviationweather.gov/adds/dataserver_current/httpparam?&dataSource=tafs&timeType=valid&requestType=retrieve&format=xml&mostRecent=true&hoursBeforeNow=1&stationString=" + textBoxStation.Text);
            if (response != null)
            {
                try
                {
                    XmlDocument doc = new XmlDocument();

                    doc.Load(response.GetResponseStream());

                    FormatTafText(doc.SelectNodes("//TAF"));

                    response.Close();
                }
                catch (WebException err)
                {
                    this.toolStripStatusLabel.Text = "WebException Error Occurred";
                    MessageBox.Show(err.Message);
                }
                catch (XmlException err)
                {
                    this.toolStripStatusLabel.Text = "XML Error Occurred";
                    MessageBox.Show(err.Message);
                }

            }


        }

        private void textBoxStation_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxStation.Text.Length == 4)
            {
                this.buttonGetMetar.Focus();
            }

        }

    }

    public class ConvertUnit
    {
        public static Double CelsiusToFahrenheit(Double c)
        {
            return ((9.0 / 5.0) * c) + 32;
        }

        public static Double FahrenheitToCelsius(Double f)
        {
            return (5.0 / 9.0) * (f - 32);
        }

        public static Double MeterToFeet(Double f)
        {
            return (f * 3.28084);
        }

        public static Double FeetToMeter(Double f)
        {
            return (f / 3.28084);
        }
    }
}
