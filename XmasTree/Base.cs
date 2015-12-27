using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XmasTree
{

    class Mechanics
    {
        Random rand;

        int sd;

        public Mechanics(int seed)
        {
            rand = new Random();
            sd = seed;
        }

        public int getRand(int max)
        {
            return rand.Next(0, max);
        }

        public Node getRandomNode(List<Node> s)
        {
            int x = rand.Next(0, 100);
            return s[x % s.Count()];
        }
    }

    class SpecialNodes
    {
        public static Node present = new Node("PR", " -- ", " -- ");

        public static Node top = new Node("TP", "  ", "/\\");
        public static Node bottom = new Node("BT", "  ", "  ");

        public static Node newLine = new Node("NL", " \n", " \n");

        public static Node branchStart = new Node("BS", " /", "/*");
        public static Node branchEnd = new Node("BE", "\\ ", "*\\");
    }

    class Nodes
    {
        public static Node singleSpace = new Node("SS", "  ", "  ");
        public static Node doubleSpace = new Node("DS", "    ", "    ");

        public static Node stdNodeA = new Node("SA", "/\\", "/\\");
        public static Node stdNodeB = new Node("SB", "\\/", "\\/");
        public static Node stdNodeC = new Node("SC", "-*", "*-");

        public static Node smallGlobe1 = new Node("sg", "\\ ", "O/");
        public static Node smallGlobe2 = new Node("SG", "/ ", "O/");

        public static Node mediumGlobe = new Node("MG", "**", "**");

        public static Node bigGlobe = new Node("BG", "/\\", "\\/");
    }

    class Node
    {
        public string type { get; set; }
        public string up { get; set; }
        public string down { get; set; }

        public Node()
        {

        }

        public Node(string t, string u, string d)
        {
            type = t;
            up = u;
            down = d;
        }

        public void set(string t, string u, string d)
        {
            type = t;
            up = u;
            down = d;
        }
    }

    class Branch
    {
        List<string> upLines = new List<string>();
        List<string> downLines = new List<string>();

        string line = "";

        public string str { get; set; }

        public static List<Node> possibleNodes { get; set; }

        public List<Node> sequence { get; set; }

        public Branch()
        {
            sequence = new List<Node>();
            possibleNodes = new List<Node>();

            //if you put it more .. you get more chances

            possibleNodes.Add(Nodes.stdNodeA);
            possibleNodes.Add(Nodes.stdNodeA);
            possibleNodes.Add(Nodes.stdNodeA);

            possibleNodes.Add(Nodes.stdNodeB);
            possibleNodes.Add(Nodes.stdNodeB);
            possibleNodes.Add(Nodes.stdNodeB);

            possibleNodes.Add(Nodes.stdNodeC);
            possibleNodes.Add(Nodes.stdNodeC);
            possibleNodes.Add(Nodes.stdNodeC);

            possibleNodes.Add(Nodes.smallGlobe1);
            possibleNodes.Add(Nodes.smallGlobe2);
            
            possibleNodes.Add(Nodes.mediumGlobe);
            
            possibleNodes.Add(Nodes.bigGlobe);

            str = "";
        }

        public void addTo(Node pos)
        {
            sequence.Add(pos);
        }

        public void clear()
        {
            sequence = new List<Node>();
            str = "";
        }

        void renderLineA(Node part)
        {
            line += part.up;
        }

        void renderLineB(Node part)
        {
            line += part.down;
        }

        public void renderSequence()
        {
            foreach (Node part in sequence)
            {
                if (part != SpecialNodes.newLine)
                {
                    renderLineA(part);
                }
                else
                {
                    upLines.Add(line);
                    line = "";
                }
            }

            line = "";

            foreach (Node part in sequence)
            {
                if (part != SpecialNodes.newLine)
                {
                    renderLineB(part);
                }
                else
                {
                    downLines.Add(line);
                    line = "";
                }
            }

            for (int i = 0; i < downLines.Count; i++)
            {
                str += upLines[i];
                str += "\n";
                str += downLines[i];
                str += "\n";
            }
        }
        public void renderTop(int max)
        {
        /*    string[] light = { "   .--._.--.--.__.--.--.__.--.--.__.--.--._.--.    ", " _(_      _Y_      _Y_      _Y_      _Y_      _)_  ", "[___]    [___]    [___]    [___]    [___]    [___] ", "/:' \\    /:' \\    /:' \\    /:' \\    /:' \\    /:' \\ ", "|::  |  |::   |  |::   |  |::   |  |::   |  |::   |", "\\::. /  \\::.  /  \\::.  /  \\::.  /  \\::.  /  \\::.  /", "\\::./    \\::./    \\::./    \\::./    \\::./    \\::./ ", "]'='      '='      '='      '='      '='      '='  " };

            foreach (string lg in light)
            {
                str += lg;
                str += "\n"; 
            }
         */
            string[] star = {" _/^\\_ ", "<  *  >", "/.....\\ "};

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < max - 4; j++)
                {
                    str += " ";
                }
                str += star[i];
                if(i < 2) str += "\n";
            }
        }
        public void renderBottom(int max, int rand)
        {
            string[] spres = {"                      ", "        /_\\/_\\        ", ".=======\\_\\/_/=======.", "|        //\\\\\\______ |", "|       //  ||to you||", "|        |  | `\"\"\"\"\"`|", "|        |  |        |", "|        |  |        |", "'===================='"};

            string[] bott = { "|   |", "|   |", "|   |", "|   |", "|   |", "|   |", "|   |", "|   |", "=====" };

            string[] lns = { "", "", "", "", "", "", "", "", "", "", ""};
            for (int j = 0; j < 9; j++)
            {
                for (int i = 0; i < max - rand - 3; i++)
                {
                    lns[j] += " ";
                }

                lns[j] += spres[j];

                for (int i = max - rand; i < (2 * max) - (max / 2) - 2; i++)
                {
                    lns[j] += " ";
                }

                lns[j] += bott[j];
                str += lns[j]; str += "\n";
            }
        }
    }

    class Tree
    {
        public List<Branch> branches { get; set; }
        public string path { get; set; }

        public Tree()
        {
            branches = new List<Branch>();
            path = "";
        }

        public void sendTreeToFile()
        {
            string cont = "";
            foreach (Branch branch in branches)
            {
                cont += branch.str; 
                cont += "\n";
            }
            File.WriteAllText(path, cont);
        }
    }
}
