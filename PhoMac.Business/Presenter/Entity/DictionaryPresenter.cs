using PhoMac.Business.Data;
using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Business.Presenter
{
    public class DictionaryPresenter
    {
        Dictionary dic;
        public List<DictionaryPresenter> ListDictionaries;
        public Dictionary Dictionaries
        {
            //entity to database
            get
            {
                copyInstance();
                return dic;
            }
            //database to entity
            set
            {
                dic = value;
                this.Dic_Keyword = dic.Dic_Keyword;
                this.Dic_SourceLanguage = dic.Dic_SourceLanguage;
                this.Dic_TargetLanguage = dic.Dic_TargetLanguage;
                this.CountWord = dic.Dic_SourceLanguage.Split(' ').Length;
            }
        }

        public DictionaryPresenter()
        {
            dic = new Dictionary();
            ListDictionaries = new List<DictionaryPresenter>();
        }

        public void CopyToList(List<Dictionary> pListDic)
        {
            for (int i = 0; i < pListDic.Count; i++)
            {
                DictionaryPresenter obj = new DictionaryPresenter();
                obj.Dictionaries = pListDic[i];
                ListDictionaries.Add(obj);
            }
        }

        void copyInstance()
        {
            dic.Dic_Keyword = Dic_Keyword;
            dic.Dic_SourceLanguage = Dic_SourceLanguage;
            dic.Dic_TargetLanguage = Dic_SourceLanguage;
            CountWord = Dictionaries.Dic_SourceLanguage.Split(' ').Length;
        }

        

        #region Property

        public string Dic_Keyword { get; set; }
        public string Dic_SourceLanguage { get; set; }
        public string Dic_TargetLanguage { get; set; }
        public int CountWord { get; set; }
        #endregion

        public void test()
        {
            Dao dao = new Dao();
            DictionaryPresenter pre = new DictionaryPresenter();
            pre.Dic_Keyword = "";
            dao.Add<Dictionary>(pre.Dictionaries);


            pre.CopyToList(dao.GetAll<Dictionary>().ToList());
            pre.Dictionaries = dao.GetById<Dictionary>();
        }

    }
}
