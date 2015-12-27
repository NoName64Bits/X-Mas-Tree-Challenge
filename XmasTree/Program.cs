using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XmasTree
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                int n = Int32.Parse(args[0]);
                int seed = Int32.Parse(args[1]);
                string path = args[2];

                Tree tree = new Tree();
                Branch branch = new Branch();
                Mechanics mechanic = new Mechanics(seed);

                tree.path = path;

                int c = 1;
                int h = n; //test only
                int add = 2;
                int max = c + (add * h);

                bool start = true;

                branch.renderTop(max);

                for (int i = 0; i < h; i++)
                {
                    for (int k = 0; k < (max - c) / 2; k++)
                    {
                        branch.addTo(Nodes.singleSpace);
                    }
                    for (int j = 0; j < c; j++)
                    {
                        if (!start)
                            if(j == 0) branch.addTo(SpecialNodes.branchStart);
                            else if(j == c - 1) branch.addTo(SpecialNodes.branchEnd);
                            else branch.addTo(mechanic.getRandomNode(Branch.possibleNodes));
                        else
                        {
                            branch.addTo(SpecialNodes.top);
                            start = false;
                        }
                    }
                    branch.addTo(SpecialNodes.newLine);
                    //tree.branches.Add(branch);
                    //branch.clear();
                    c += add;
                }

                int m = max / 2;
                m--;
                int r = mechanic.getRand(m);

                branch.renderSequence();

                branch.renderBottom(m, r);

                tree.branches.Add(branch);

                tree.sendTreeToFile();
            }
        }
    }
}
