using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Class1 class1 = new Class1();
        List<GisFlats> gis_flats_list;
        List<inary_flats> inary_flats_list;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            listView1.Items.Clear();
            treeView1.Nodes.Clear();
            try
            {
                inary_flats_list = class1.read_inary_flats();
                listView1.Columns.Add("Лицевой счет", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("ФИО", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Адрес", 100, HorizontalAlignment.Left);
                listView1.Columns.Add("Номер помещения", 100, HorizontalAlignment.Left);

                int i1 = 0;
                foreach (var l in inary_flats_list)
                {
                    listView1.Items.Add(new ListViewItem(new[] { l.occ_name, l.FIO, l.Address, l.flat_no, l.flat_no2 }));
                    if (l.PremisesGUID == null) listView1.Items[i1].BackColor = Color.Red;
                    i1++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK);
            }

            //tree view
            try
            {
                int i = 0;
                foreach (var inar in inary_flats_list)
                {
                    treeView1.Nodes.Add(inar.Address + " квартира " + inar.flat_no);

                    treeView1.Nodes[i].Nodes.Add("Лицевой счет: " + inar.occ_name);
                    treeView1.Nodes[i].Nodes.Add("ФИО: " + inar.FIO);
                    treeView1.Nodes[i].Nodes.Add("Тип помещения: " + inar.roomtype_id);
                    treeView1.Nodes[i].Nodes.Add("Общая площадь: " + inar.total_sq);
                    if (inar.closed == false) treeView1.Nodes[i].Nodes.Add("Статус: Открыт");
                    else
                    {
                        treeView1.Nodes[i].BackColor = Color.Gray;
                    }
                    treeView1.Nodes[i].Nodes.Add("GUID помещения: " + inar.PremisesGUID);
                    treeView1.Nodes[i].Tag = i;
                    if (inar.PremisesGUID == null) treeView1.Nodes[i].BackColor = Color.Red;
                    if (inar.closed == true) 
                    {
                        //treeView1.Nodes[i].BackColor = Color.Gray;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK);
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var si1 = listView1.SelectedIndices;
                var si2 = listView2.SelectedIndices;
                if (MessageBox.Show("Вы уверены?", "Вопрос", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    inary_flats_list[si1[0]].PremisesGUID = gis_flats_list[si2[0]].PremisesGUID;
                    inary_flats_list[si1[0]].PremisesNum = gis_flats_list[si2[0]].PremisesNum;
                    inary_flats_list[si1[0]].PremisesUniqueNumber = gis_flats_list[si2[0]].PremisesUniqueNumber;
                    Class1 class1 = new Class1();
                    // class1.update_inary_flats(inary_flats_list[si1[0]]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Enabled = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Class1 class1 = new Class1();
            //// var index = listView1.Items.IndexOf(listView1.SelectedItems[0]);
            // int index1 = listView1.FocusedItem.Index;
            // gis_flats_list = class1.read_gis_flats(inary_flats_list[index1].FiasGuid);

        }

        //int ind1 = 0;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeView2.Nodes.Clear();
            Class1 class1 = new Class1();
            var ind = Convert.ToInt32(treeView1.SelectedNode.Tag);
            //if ((treeView1.Nodes[ind1].IsExpanded) && (Convert.ToInt32(treeView1.SelectedNode.Tag)!=ind1)) treeView1.Nodes[ind1].Collapse();
            //treeView1.Nodes[ind].Expand();
            //ind1 = ind;
            var result_list = class1.read_gis_flats(inary_flats_list[ind].FiasGuid);
            int i = 0;

            foreach (var l in result_list)
            {
                if (l.FIO is null) l.FIO = "Нет ФИО";

                treeView2.Nodes.Add(l.Address + "  кв " + l.PremisesNum);
                treeView2.Nodes[i].Nodes.Add("Лицевой счет: " + l.AccountNumber);
                treeView2.Nodes[i].Nodes.Add("ФИО: " + l.FIO);
                treeView2.Nodes[i].Nodes.Add("Площадь: " + l.TotalArea);
                if (l.Living == 1) treeView2.Nodes[i].Nodes.Add("Жилое помещение: Да"); else treeView2.Nodes[i].Nodes.Add("Жилое помещение: Нет");
                if (l.TerminationDate is null) treeView2.Nodes[i].Nodes.Add("Статус: открыто"); else treeView2.Nodes[i].Nodes.Add("Статус: закрыто");
                treeView2.Nodes[i].Nodes.Add("GUID помещения: " + l.PremisesGUID);
                //если привязано, выделить зеленым
                if (l.PremisesGUID == inary_flats_list[ind].PremisesGUID)
                {
                    treeView2.Nodes[i].BackColor = Color.DarkGreen;
                    treeView2.Nodes[i].ForeColor = Color.White;
                    treeView2.Nodes[i].Expand();
                }

                if ((l.PremisesGUID == inary_flats_list[ind].PremisesGUID) && (l.PremisesNum.Equals(inary_flats_list[ind].flat_no2) == false)) treeView1.Nodes[ind].BackColor = Color.Blue;
                //возможное совпадение1
                if (inary_flats_list[ind].PremisesGUID is null)
                {
                    string[] s1 = inary_flats_list[ind].flat_no.Split(' ');

                    if (s1[0].Contains(l.PremisesNum) || l.PremisesNum.Contains(s1[0]))
                    {
                        treeView2.Nodes[i].BackColor = Color.Yellow;      
                    }
                    
                    if (s1[0].Equals(l.PremisesNum))
                    {
                        treeView2.Nodes[i].BackColor = Color.OrangeRed;
                        treeView2.Nodes[i].Expand();
                    }
                }
                i++;
            }
            //отделить лицевой 
            Class2 class2 = new Class2();
            var occid="";
            if (treeView1.SelectedNode.Level == 0)
            {
                var index = treeView1.SelectedNode.Index;
                occid = inary_flats_list[index].occ_name;
            }
            if (treeView1.SelectedNode.Level!=0)
            {
                var index = treeView1.SelectedNode.Parent.Index;
                occid = inary_flats_list[index].occ_name;
            }
        }
    
    }
}
