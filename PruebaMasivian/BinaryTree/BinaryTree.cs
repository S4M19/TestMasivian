using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    class BinaryTree
    {
        class Node
        {
            public int value;

            public Node branchRight, branchLeft;
        }

        private Node root;

        private List<int> routeOne, routeTwo;

        public BinaryTree()
        {
            root = null;
            routeOne = new List<int>();
            routeTwo = new List<int>();
        }

        private void InsertNewNode(int value)
        {
            if (!NodeValueExists(value))
            {
                Node novo = new Node
                {
                    value = value,
                    branchRight = null,
                    branchLeft = null
                };
                if (root == null)
                {
                    root = novo;
                }
                else
                {
                    Node previous = null, aux;
                    aux = root;
                    while (aux != null)
                    {
                        previous = aux;
                        if (value < aux.value)
                            aux = aux.branchLeft;
                        else
                            aux = aux.branchRight;
                    }
                    if (value < previous.value)
                    {
                        previous.branchLeft = novo;
                    }
                    else
                    {
                        previous.branchRight = novo;
                    }

                }
            }
            else
            {
                Console.WriteLine("El valor: " + value + " ya se encuentra en el árbol. No es posible agregarlo.");
            }
        }

        public bool NodeValueExists(int value)
        {
            Node aux = root;
            while (aux != null)
            {
                if (value == aux.value)
                    return true;
                else
                    if (value > aux.value)
                    aux = aux.branchRight;
                else
                    aux = aux.branchLeft;
            }
            return false;
        }

        private void PrintNodes(Node aux)
        {
            if (aux != null)
            {
                PrintNodes(aux.branchLeft);
                Console.Write(aux.value + " ");
                PrintNodes(aux.branchRight);
            }
        }

        public void PrintNodes()
        {
            PrintNodes(root);
            Console.WriteLine();
        }

        private void ExistNodes(Node node, int value, List<int> route)
        {
            if (node != null)
            {
                if (value == node.value)
                {
                    route.Add(node.value);
                    //Console.WriteLine("El valor: " + node.value + " ha sido encontrado en el árbol.");
                }
                else if (value < node.value)
                {
                    route.Add(node.value);
                    ExistNodes(node.branchLeft, value, route);
                }
                else
                {
                    route.Add(node.value);
                    ExistNodes(node.branchRight, value, route);
                }
            }
            else
            {
                Console.WriteLine("El valor: " + value + " no ha sido encontrado en el árbol.");
            }
        }

        private void ExistsNode(int value, List<int> route)
        {
            ExistNodes(root, value, route);
        }
        
        private void ClearRoutes()
        {
            routeOne.Clear();
            routeTwo.Clear();
        }

        private int Parent()
        {
            int aux = 0;
            for (int listTwo = (int)routeTwo.LongCount() - 1; listTwo >= 0; listTwo--)
            {
                for (int listOne = (int)routeOne.LongCount() - 1; listOne >= 0; listOne--)
                {
                    if (routeTwo[listTwo] == routeOne[listOne])
                    {
                        aux = routeTwo[listTwo];
                        ClearRoutes();
                        return aux;
                    }
                }
            }
            return 0;
        }

        private int ParentNode(int one, int two)
        {
            ExistsNode(one, routeOne);
            ExistsNode(two, routeTwo);
            return Parent();
        }

        static void Main(string[] args)
        {
            BinaryTree bt = new BinaryTree();

            /* Se construye el arbol binario mediante un array */
            int[] numbers = new int[] { 67, 39, 28, 44, 29, 76, 74, 85, 83, 87 };
            for (int l = 0; (l < numbers.Length); l++)
            {
                bt.InsertNewNode(numbers[l]);
            }

            Console.WriteLine("LowestCommonAncestor(tree, " + 29 + ", " + 44 + ") = " + bt.ParentNode(29, 44));
            Console.WriteLine("LowestCommonAncestor(tree, " + 44 + ", " + 85 + ") = " + bt.ParentNode(44, 85));
            Console.WriteLine("LowestCommonAncestor(tree, " + 83 + ", " + 87 + ") = " + bt.ParentNode(83, 87));
            Console.WriteLine("LowestCommonAncestor(tree, " + 87 + ", " + 74 + ") = " + bt.ParentNode(87, 74));
            Console.WriteLine("LowestCommonAncestor(tree, " + 83 + ", " + 28 + ") = " + bt.ParentNode(83, 28));

            Console.ReadKey();
        }

    }
}
