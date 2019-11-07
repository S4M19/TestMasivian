using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaMasivian.Models
{
    public class Node
    {
        /// <summary>
        /// Contenido del nodo
        /// </summary>
        public int data;

        /// <summary>
        /// Direcciones del nodo
        /// </summary>
        public Node branchRight, branchLeft;

        /// <summary>
        /// Nodo principal o raiz
        /// </summary>
        public Node root;

        public Node()
        {
            data = 0;
            branchRight = null;
            branchLeft = null;
            root = null;
        }

    }

    public class BinaryTree
    {
        public List<string> binaryTree { get; set; }
    }
}