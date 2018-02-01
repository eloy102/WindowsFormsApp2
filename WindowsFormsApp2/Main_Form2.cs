using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Main_Form2 : Form
    {
        public Main_Form2()
        {
            InitializeComponent();
        }
        List<Org> org;
        List<Buildings> buildings;
        List<inary_Flats1> inary_flats1;
        List<GisFlats> gis_flats;
        private async void Main_Form2_Load(object sender, EventArgs e)
        {
            Работа_с_базой2 db = new Работа_с_базой2();
            org = await db.ReadOrgAsync();
            int i = 0;
            foreach(var o in org)
            {
                comboBox1.Items.Add(o.Name);     
            }
        }
         
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Работа_с_базой2 db = new Работа_с_базой2();
            treeView1.Nodes.Clear();
            var s1 = comboBox1.SelectedIndex;
            buildings = await db.ReadAllBuldingsAsync(org[s1].id, 201712);
            foreach (var b in buildings)
            {
                treeView1.Nodes.Add(b.Address);
            }

           
        }

        async Task show_buildings(TreeNode selectedItem)
        {
            var ind1 = selectedItem.Index;
            Работа_с_базой2 db = new Работа_с_базой2();
            //Если выбран дом
            if (selectedItem.Level == 0)
            {
                treeView1.Nodes[ind1].Nodes.Clear();
                inary_flats1 = await db.ReadFlatsAsync(buildings[ind1].bldn_id, 201712);
                int i = 0;
                int err = 0;
                foreach (var f in inary_flats1)
                {
                    treeView1.Nodes[ind1].Nodes.Add("кв " + f.flat_no);
                    if ((f.PremisesGUID is null) || (f.PremisesNum is null) || (f.PremisesUniqueNumber is null)) { treeView1.Nodes[ind1].Nodes[i].BackColor = Color.Red; err++; }
                    i++;
                }
                treeView1.Nodes[ind1].Expand();
                toolStripStatusLabel4.Text = "Несвязанных записей в доме: " + err.ToString();
                //Вывод квартир
                var f1 = inary_flats1;
                for (int k = 0; k < f1.Count; k++)
                {
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Адрес: " + buildings[ind1].Address + "  кв " + f1[k].flat_no);
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Площадь: " + f1[k].total_sq);
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Тип помещения: " + f1[k].roomtype_id);
                    if (f1[k].closed == true) treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Cтатус: Закрыто");
                    else treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Статус: Открыто");
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("GUID Помещения: " + f1[k].PremisesGUID);
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Номер помещения в ГИС: " + f1[k].PremisesNum);
                    treeView1.Nodes[ind1].Nodes[k].Nodes.Add("Лицевые счета");
                    List<occupations> occ = await db.ReadOccAsync(f1[k].flat_id, buildings[ind1].bldn_id, 201712);//ИСПРАВИТЬ
                    foreach (var o in occ)
                    {
                        treeView1.Nodes[ind1].Nodes[k].Nodes[6].Nodes.Add("Лицевой счет: " + o.occ_name);
                    }
                    for (int j = 0; j < occ.Count; j++)
                    {
                        treeView1.Nodes[ind1].Nodes[k].Nodes[6].Nodes[j].Nodes.Add("ФИО: " + occ[j].FIO);
                        treeView1.Nodes[ind1].Nodes[k].Nodes[6].Nodes[j].Nodes.Add("GUID аккаунта: " + occ[j].AccountGuid);
                        treeView1.Nodes[ind1].Nodes[k].Nodes[6].Nodes[j].Nodes.Add("GUID помещения: " + occ[j].PremisesGUID);
                    }
                }
            }
        }

        private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selectedItem = treeView1.SelectedNode;
            selectedItem.Expand();
            await show_buildings(selectedItem);

            //вывод из гис
            
            if (selectedItem.Level == 0)
            {
                Работа_с_базой2 db = new Работа_с_базой2();
                gis_flats = await db.read_gis_flats(buildings[selectedItem.Index].FiasGuid);
                treeView2.Nodes.Clear();
                treeView2.Nodes.Add(selectedItem.Text);
                treeView2.Nodes[0].Expand();
                
                int i = 0;
                foreach (var g in gis_flats)
                {
                    treeView2.Nodes[0].Nodes.Add("кв " + g.PremisesNum);
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("Адрес " + g.Address + ", кв " + g.PremisesNum);
                    if (g.FIO is null) g.FIO = "Не заполнено";
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("ФИО собственника: " + g.FIO);
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("Лицевой счет: " + g.AccountNumber);
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("Площадь: " + g.TotalArea);
                    if (g.Living == 1) treeView2.Nodes[0].Nodes[i].Nodes.Add("Жилое помещение: Да");
                    else treeView2.Nodes[0].Nodes[i].Nodes.Add("Жилое помещение: Нет");
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("GUID помещения: " + g.PremisesGUID);
                    treeView2.Nodes[0].Nodes[i].Nodes.Add("Уникальный номер помещения: " + g.PremisesUniqueNumber);
                    i++;
                }
            }
            if (selectedItem.Level == 1)
            {
                treeView2.Nodes[0].Expand();
                selectedItem.ExpandAll();
                var guid = inary_flats1[selectedItem.Index].PremisesGUID;
                var f = inary_flats1[selectedItem.Index];
                int i = 0;
                int q1 = 0; //счетчик возможных совпадений
                int q2 = 0; //счетчик связанных записей
                int q3 = 0;//счеьчик точных совпадений

                foreach (var g in gis_flats)
                {
                    treeView2.Nodes[0].Nodes[i].BackColor = Color.White;
                    treeView2.Nodes[0].Nodes[i].Collapse();
                    if ((f.flat_no.Contains(g.PremisesNum)) || (g.PremisesNum.Contains(f.flat_no))) { treeView2.Nodes[0].Nodes[i].BackColor = Color.Yellow;q1++; }
                    if (f.flat_no.Equals(g.PremisesNum)) { treeView2.Nodes[0].Nodes[i].BackColor = Color.DarkOrange; q3++; }
                    if (g.PremisesGUID == guid)
                    {
                        treeView2.Nodes[0].Nodes[i].BackColor = Color.DarkGreen;
                        treeView2.Nodes[0].Nodes[i].Expand();
                        q2++;
                    }
                    i++;
                    toolStripStatusLabel7.Text = "Возможных совпадений: "+q1.ToString();
                    toolStripStatusLabel6.Text = "Точных совпадений: " + q3.ToString();
                    toolStripStatusLabel5.Text = "Связанных записей: " + q2.ToString();
                }
            }
                      
        }
    }
}
