using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PruebaMasivian.Models;

namespace PruebaMasivian.Controllers
{
    public class BinaryTreeController : ApiController
    {
        /// <summary>
        /// Objeto de la clase publica Node
        /// </summary>
        public Node node;


        /// <summary>
        /// Se utiliza para guardar el recorrido cuando se buscan nodos
        /// </summary>
        private List<int> routeOne, routeTwo;

        /// <summary>
        /// Nodo principal o raiz del arbol
        /// </summary>
        private Node root;

        /// <summary>
        /// Se utiliza para validar si el valor existe en el arbol
        /// </summary>
        /// <param name="value"> valor del nodo que se quiere agregar</param>
        /// <returns>retorna si existe o no el valor</returns>
        private bool ExistValue (int value)
        {
            Node aux = node.root;

            while (aux != null)
            {
                if (value == aux.value)
                    return true;
                else if (value > aux.value)
                    aux = aux.branchRight;
                else
                    aux = aux.branchLeft;
            }
            return false;
        }

        /// <summary>
        /// Se utiliza para agregar un nuevo nodo al arbol
        /// </summary>
        /// <param name="value"> valor del nodo que se quiere agregar</param>
        private void InsertNewNode(int value)
        {
            if (!ExistValue(value))
            {
                Node novo = new Node
                {
                    value = value,
                    branchRight = null,
                    branchLeft = null
                };
                if (node.root == null)
                {
                    node.root = novo;
                }
                else
                {
                    Node previous = null, aux;
                    aux = node.root;
                    while(aux != null)
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

        /// <summary>
        /// Se utiliza para buscar el valor de un nodo y guardar la ruta que toma
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        /// <param name="route"></param>
        private void ExistNodes(Node node, int value, List<int> route)
        {
            if (node != null)
            {
                if(value == node.value)
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

        /// <summary>
        /// Se utiliza para acceder al metodo ExistNodes, teniendo como parametro el nodo principal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="route"></param>
        private void ExistsNode(int value, List<int> route)
        {
            ExistNodes(root, value, route);
        }

        /// <summary>
        /// Se utiliza para limpiar las listas de las rutas recorridas
        /// </summary>
        private void ClearRoutes()
        {
            routeOne.Clear();
            routeTwo.Clear();
        }

        /// <summary>
        /// Se utiliza para obtener el valor del primer nodo padre en comun de la ruta
        /// </summary>
        /// <returns></returns>
        private int Parent()
        {
            int aux = 0;
            for (int listTwo = (int)routeTwo.LongCount() - 1; listTwo >= 0; listTwo--)
            {
                for (int listOne = (int)routeOne.LongCount() - 1; listOne >= 0; listOne--)
                {
                    if(routeTwo[listTwo] == routeOne[listOne])
                    {
                        aux = routeTwo[listTwo];
                        ClearRoutes();
                        return aux;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Recibe los valores que se van a buscar para obtener el valor del primer nodo padre en comun
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        private int ParentNode(int one, int two)
        {
            ExistsNode(one, routeOne);
            ExistsNode(two, routeTwo);
            return Parent();
        }

        /// <summary>
        /// Se utiliza para la creacion del arbol binario
        /// </summary>
        /// <param name="binaryTree">Json que contine los valores para construir el arbol binario</param>
        /// <returns>Estado y mensaje de la petición</returns>
        [HttpPost]
        public IHttpActionResult CreateBinaryTree(BinaryTree binaryTree)
        {
            node = new Node();
            int value = 0;
            foreach (string num in binaryTree.binaryTree)
            {
                // Se valida la conversión de los datos del Json 
                // Si la conversión falla, se cierra el proceso y se envia un mensaje de error Status 400
                if (!Int32.TryParse(num, out value))
                {
                    return BadRequest("Ha ocurrido un error en el intento de construir el árbol, por favor valide la información ingresada.");
                }
                InsertNewNode(value);
            }
            // Mensaje de Status 200; solicitud efectuada correctamente!
            return Ok("El árbol ha sido construído de manera exitosa!.");
        }

        /// <summary>
        /// Se utiliza para la creacion del arbol binario y para la consulta del nodo padre
        /// </summary>
        /// <param name="binaryTreeNodes"></param>
        /// <returns>Estado y mensaje de la petición</returns>
        [HttpPost]
        public IHttpActionResult ParentNode(BinaryTreeNodes binaryTreeNodes)
        {
            node = new Node();
            int value = 0;
            foreach (string num in binaryTreeNodes.binaryTree)
            {
                // Se valida la conversión de los datos del Json 
                // Si la conversión falla, se cierra el proceso y se envia un mensaje de error Status 400
                if(!Int32.TryParse(num, out value))
                {
                    return BadRequest("Ha ocurrido un error en el intento de construir el árbol, por favor valide la información ingresada.");
                }
                InsertNewNode(value);
            }

            routeOne = new List<int>();
            routeTwo = new List<int>();
            root = node.root;

            if(!Int32.TryParse(binaryTreeNodes.nodeOne, out value))
            {
                return BadRequest("Ha ocurrido un error en el momento de la validación del valor del nodo uno.");
            }
            int nodeOne = value;

            if (!Int32.TryParse(binaryTreeNodes.nodeTwo, out value))
            {
                return BadRequest("Ha ocurrido un error en el momento de la validación del valor del nodo dos.");
            }
            int nodeTwo = value;

            // Mensaje de Status 200; solicitud efectuada correctamente!
            return Ok("El ancestro común más cercano es: " + ParentNode(nodeOne, nodeTwo));
        }

    }
}