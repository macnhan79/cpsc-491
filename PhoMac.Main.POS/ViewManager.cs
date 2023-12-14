using PhoMac.Business.Data;
using PhoMac.Business.Presenter;
using PhoMac.Main.POS.Views.UC;
using PhoMac.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Main.POS
{
    public class ViewManager
    {
        static ViewManager viewManager = new ViewManager();
        Dao dao = new Dao();


        public static ViewManager getInstance()
        {
            return viewManager;
        }

        private ViewManager()
        {
            createListTables();
            createListProducts();
            ListTableTab = new List<UCPanelLayoutTable>();
            ListTableTab = createListTableInTab(false);
        }

        void setViewToCache(string name, object item)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            cache.Set(name, item, policy);
        }

        T getViewFromCache<T>(string name)
        {
            ObjectCache cache = MemoryCache.Default;
            T view = (T)cache[name];

            if (view == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                // create view
                ///to do
                //
                //cache.Set(name, fileContents, policy);
            }
            return view;
        }


        /// <summary>
        /// List tables user control
        /// </summary>
        public List<UCTable> ListUCTables { get; set; }
        void createListTables()
        {
            ListUCTables = new List<UCTable>();
            TablePresenter table = new TablePresenter();
            table.CopyToList(dao.GetAll<Table>().ToList());
            for (int i = 0; i < table.ListTables.Count; i++)
            {
                UCTable panelTable = new UCTable();
                panelTable.TableID = table.ListTables[i].TableID;
                ListUCTables.Add(panelTable);
            }
        }

        /// <summary>
        /// List products user control
        /// </summary>
        public List<UCProduct> ListUCProducts { get; set; }
        void createListProducts()
        {
            ListUCProducts = new List<UCProduct>();
            ProductPresenter productPresenter = new ProductPresenter();
            productPresenter.CopyToList(dao.GetAll<Product>().ToList());
            for (int i = 0; i < productPresenter.ListProducts.Count; i++)
            {
                UCProduct panelProduct = new UCProduct();
                panelProduct.ProductID = productPresenter.ListProducts[i].ProductID;
                ListUCProducts.Add(panelProduct);
            }
        }


        public List<UCPanelLayoutTable> ListTableTab;
        List<UCPanelLayoutTable> createListTableInTab(bool allowDrap)
        {
            List<UCPanelLayoutTable> listTableTab = new List<UCPanelLayoutTable>();

            TabCategoryPresenter tabPresenter = new TabCategoryPresenter();
            //get all tab
            tabPresenter.CopyToList(dao.GetAll<TabCategory>().ToList());
            //get table belong to tab
            for (int i = 0; i < tabPresenter.ListTabCategories.Count; i++)
            {
                //TablePresenter table = new TablePresenter();
                int tabID = tabPresenter.ListTabCategories[i].CategoryID;
                var listUCTable = ListUCTables.Where(x => x.TableInfo.CategoryID == tabID).ToList();
                //table.CopyToList(dao.FindByMultiColumnAnd<Table>(new[] { "CategoryID" }, tabID).ToList());
                //create panel layout
                UCPanelLayoutTable panel = new UCPanelLayoutTable();
                panel.NumberOfRows = tabPresenter.ListTabCategories[i].Rows;
                panel.NumberOfColumns = tabPresenter.ListTabCategories[i].Cols;
                if (allowDrap)
                    panel.AllowDrapAndDrop = true;
                else
                    panel.AllowDrapAndDrop = false;


                panel.initLayout();

                for (int h = 0; h < listUCTable.Count; h++)
                {
                    UCTable panelTable = new UCTable();
                    panelTable.TableID = listUCTable[h].TableID;
                    panel.PushOject(panelTable, listUCTable[h].TableInfo.Row - 1, listUCTable[h].TableInfo.Col - 1);
                }
                listTableTab.Add(panel);
            }
            return listTableTab;
        }




    }
}
