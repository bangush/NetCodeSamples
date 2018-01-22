using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace tr.mustaliscl.dosyalar
{
    public class Dosya
    {

        public static void Files2TreeView(ref TreeView tv, string directory)
        {
            tv.Nodes.Clear();
            TreeNode node = new TreeNode(new DirectoryInfo(directory).Name);
            PopulateTree(directory, ref node);
            tv.Nodes.Add(node);
        }

        private static void PopulateTree(string dir, ref TreeNode node)
        {
            // get the information of the directory
            DirectoryInfo directory = new DirectoryInfo(dir);
            // loop through each subdirectory
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                // create a new node
                TreeNode t = new TreeNode(d.Name);
                // populate the new node recursively
                PopulateTree(d.FullName, ref t);
                node.Nodes.Add(t); // add the node to the "master" node
            }
            // lastly, loop through each file in the directory, and add these as nodes
            foreach (FileInfo f in directory.GetFiles())
            {
                // create a new node
                TreeNode t = new TreeNode(f.Name);
                // add it to the "master"
                node.Nodes.Add(t);
            }
        }
    }
}
