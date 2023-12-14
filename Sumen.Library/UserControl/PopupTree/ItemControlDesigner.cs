using System;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;

namespace PhoHa7.Library.UserControl.PopupTree
{
    public class ItemControlDesigner : ControlDesigner
    {
        private DesignerVerbCollection verbs = new DesignerVerbCollection();

        public override DesignerVerbCollection Verbs
        {
            get { return verbs; }
        }

        public ItemControlDesigner()
        {
            verbs.Add(new DesignerVerb("About", new EventHandler(OnClick)));
        }

        protected void OnClick(object sender, EventArgs e)
        {
            IDesignerHost host = (IDesignerHost)GetService(typeof(IDesignerHost));
            IComponentChangeService changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            DesignerTransaction tran = host.CreateTransaction("About");
            string info = "Author\t: Phạm Văn Duy\n";
            info += "Purpose\t: Cho phép nhập liệu theo dạng popup\n";
            info += "Version\t: 1.0\n";
            info += "Copyright\t: CUSC";
            System.Windows.Forms.MessageBox.Show(info, "Thông tin", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            tran.Commit();
        }
    }
}