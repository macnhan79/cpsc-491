using System;
using System.Text;
using System.Xml;

namespace PhoHa7.Library.Froms.MsgBox
{
    class ClsConfig
    {
        public ClsConfig()
        {
            newConfig();
            loadConfig();
        }
        private int lRongNho;
        /// <summary>
        /// Lấy độ rộng tối thiểu của hộp thoại
        /// </summary>
        public int MinWidth
        {
            get { return lRongNho; }
        }

        private int lRongLon;

        /// <summary>
        /// Độ rộng nhỏ nhất
        /// </summary>
        public int MaxWidth
        {
            get { return lRongLon; }
        }

        private int lCaoNho;
        /// <summary>
        /// Lấy độ cao tối đa của hộp thoại
        /// </summary>
        public int MinHeight
        {
            get { return lCaoNho; }
        }

        private int lCaoLon;

        /// <summary>
        /// Độ rộng lớn nhất
        /// </summary>
        public int MaxHeight
        {
            get { return lCaoLon; }
        }

        private string lMailServer;
        /// <summary>
        /// Tên servermail sẻ gủi thông tin lỗi
        /// </summary>
        public string MailServer
        {
            get { return lMailServer; }
        }

        private string lEmailSend;
        /// <summary>
        /// Địa chỉ mail trên mail server gủi
        /// </summary>
        public string EmailSend
        {
            get { return lEmailSend; }
        }

        private string lPassWord;
        /// <summary>
        /// Pass để đăng nhập mailserver
        /// </summary>
        public string PassWord
        {
            get { return lPassWord; }
        }

        private string lEmailRec;
        /// <summary>
        /// Địa chỉ mail nhận
        /// </summary>
        public string EmailRec
        {
            get { return lEmailRec; }
        }

        /// <summary>
        /// Tạo một file cấu hình với thông số mặc định
        /// </summary>
        private void newConfig()
        {
            if (!System.IO.File.Exists("ErrorSender.xml"))
            {
                XmlTextWriter textWriter = new XmlTextWriter("ErrorSender.xml", null);
                textWriter.Formatting = Formatting.Indented;
                // Opens the document 
                textWriter.WriteStartDocument();
                // ghi thu mục gốc
                textWriter.WriteStartElement("Config");

                textWriter.WriteStartElement("MailServer");
                textWriter.WriteString(Encode("123.com"));
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MailFrom");
                textWriter.WriteString(Encode("support@123.com"));
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("PassWord");
                textWriter.WriteString(Encode("sd@123"));
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MailTo");
                textWriter.WriteString("support@123.com");
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MinWidth");
                textWriter.WriteString("220");
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MaxWidth");
                textWriter.WriteString("600");
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MinHeight");
                textWriter.WriteString("140");
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("MaxHeight");
                textWriter.WriteString("400");
                textWriter.WriteEndElement();

                textWriter.WriteEndElement();
                textWriter.WriteEndDocument();
                textWriter.Close();
            }
        }

        /// <summary>
        /// Ma hoa chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string Encode(string strRoot)
        {
            string str = "";
            byte[] asciiBytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(strRoot));
            for (int i = 0; i < asciiBytes.Length; i++)
            {
                str = str + Convert.ToChar(asciiBytes[i] + 7);
            }
            return str;
        }

        /// <summary>
        /// Giai max chuoi
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Decode(string strCode)
        {
            string str = "";
            byte[] asciiBytes = Encoding.Convert(Encoding.Unicode, Encoding.ASCII, Encoding.Unicode.GetBytes(strCode));
            for (int i = 0; i < asciiBytes.Length; i++)
            {
                str = str + Convert.ToChar(asciiBytes[i] - 7);
            }
            return str;
        }

        /// <summary>
        /// Load thông tin cấu hình thông báo
        /// </summary>
        private void loadConfig()
        {
            if (System.IO.File.Exists("ErrorSender.xml"))
            {
                XmlTextReader textReader = new XmlTextReader("ErrorSender.xml");
                while (textReader.Read())
                {
                    XmlNodeType nType = textReader.NodeType;
                    if (nType == XmlNodeType.Element)
                    {
                        if (textReader.LocalName == "MailServer")
                        {
                            lMailServer = Decode(textReader.ReadElementContentAsString());
                        }

                        if (textReader.LocalName == "MailFrom")
                        {
                            lEmailSend = Decode(textReader.ReadElementContentAsString());
                        }

                        if (textReader.LocalName == "MailTo")
                        {
                            lEmailRec = textReader.ReadElementContentAsString();
                        }

                        if (textReader.LocalName == "PassWord")
                        {
                            lPassWord = Decode(textReader.ReadElementContentAsString());
                        }

                        if (textReader.LocalName == "MinWidth")
                        {
                            int min = textReader.ReadElementContentAsInt();
                            lRongNho = min <= 220 ? 220 : min;                            
                        }

                        if (textReader.LocalName == "MaxWidth")
                        {
                            int max = textReader.ReadElementContentAsInt();
                            lRongLon = max <= 600 ? 600 : max; 
                        }

                        if (textReader.LocalName == "MinHeight")
                        {
                            int min = textReader.ReadElementContentAsInt();
                            lCaoNho = min <= 140 ? 140 : min;
                        }

                        if (textReader.LocalName == "MaxHeight")
                        {
                            int max = textReader.ReadElementContentAsInt();
                            lCaoLon = max <= 400 ? 400 : max;                            
                        }
                    }
                }
            }
        }
    }
}
