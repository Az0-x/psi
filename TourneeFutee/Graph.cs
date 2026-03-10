using System;
using System.Collections.Generic;

namespace TourneeFutee
{
    public class Graph
    {
        // --- Attributs ---

        private bool directed;
        private float noEdgeValue;

        // La matrice d'adjacence stocke les poids des arcs
        private Matrix adjacencyMatrix;

        // Dictionnaire pour retrouver l'index d'un sommet à partir de son nom (Complexité O(1))
        private Dictionary<string, int> vertexIndices;

        // Liste pour retrouver le nom d'un sommet à partir de son index (Complexité O(1))
        private List<string> vertexNames;

        // Liste pour stocker les valeurs associé aux sommets (ex: temps de visite, priorité...)
        private List<float> vertexValues;


        // --- Construction du graphe ---

        // Contruit un graphe (`directed`=true => orienté)
        // La valeur `noEdgeValue` est le poids modélisant l'absence d'un arc (0 par défaut)
        public Graph(bool directed, float noEdgeValue = 0)
        {
            this.directed = directed;
            this.noEdgeValue = noEdgeValue;

            
            this.adjacencyMatrix = new Matrix(0, 0, noEdgeValue);

            this.vertexIndices = new Dictionary<string, int>();
            this.vertexNames = new List<string>();
            this.vertexValues = new List<float>();
        }


        // --- Propriétés ---

        // Propriété : ordre du graphe (nombre de sommets)
        // Lecture seul
        public int Order
        {
            get { return vertexNames.Count; }
        }

        // Propriété : graphe orienté ou non
        //Lecture seul
        public bool Directed
        {
            get { return directed; }
        }


        // --- Gestion des sommets ---

        // Ajoute le sommet de nom `name` et de valeur `value` (0 par défaut) dans le graphe
        // Lève une ArgumentException s'il existe déjà un sommet avec le même nom dans le graphe
        public void AddVertex(string name, float value = 0)
        {
            if (vertexIndices.ContainsKey(name))
            {
                throw new ArgumentException($"Le sommet '{name}' existe déjà.");
            }

            int newIndex = this.Order; // L'index sera à la fin

            
            vertexIndices.Add(name, newIndex);
            vertexNames.Add(name);
            vertexValues.Add(value);

            
            adjacencyMatrix.AddRow(newIndex);
            adjacencyMatrix.AddColumn(newIndex);
        }


        // Supprime le sommet de nom `name` du graphe (et tous les arcs associés)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void RemoveVertex(string name)
        {
            if (!vertexIndices.ContainsKey(name))
            {
                throw new ArgumentException($"Le sommet '{name}' n'existe pas.");
            }

            int indexToRemove = vertexIndices[name];

            adjacencyMatrix.RemoveRow(indexToRemove);
            adjacencyMatrix.RemoveColumn(indexToRemove);

            
            vertexNames.RemoveAt(indexToRemove);
            vertexValues.RemoveAt(indexToRemove);
            vertexIndices.Remove(name);

            for (int i = indexToRemove; i < vertexNames.Count; i++)
            {
                string nameToUpdate = vertexNames[i];
                vertexIndices[nameToUpdate] = i;
            }
        }

        // Renvoie la valeur du sommet de nom `name`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public float GetVertexValue(string name)
        {
            if (!vertexIndices.ContainsKey(name))
            {
                throw new ArgumentException($"Le sommet '{name}' n'existe pas.");
            }

            int index = vertexIndices[name];
            return vertexValues[index];
        }

        // Affecte la valeur du sommet de nom `name` à `value`
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public void SetVertexValue(string name, float value)
        {
            if (!vertexIndices.ContainsKey(name))
            {
                throw new ArgumentException($"Le sommet '{name}' n'existe pas.");
            }

            int index = vertexIndices[name];
            vertexValues[index] = value;
        }


        // Renvoie la liste des noms des voisins du sommet de nom `vertexName`
        // (si ce sommet n'a pas de voisins, la liste sera vide)
        // Lève une ArgumentException si le sommet n'a pas été trouvé dans le graphe
        public List<string> GetNeighbors(string vertexName)
        {
            if (!vertexIndices.ContainsKey(vertexName))
            {
                throw new ArgumentException($"Le sommet '{vertexName}' n'existe pas.");
            }

            List<string> neighborNames = new List<string>();
            int sourceIndex = vertexIndices[vertexName];
            int order = this.Order;

            for (int col = 0; col < order; col++)
            {
                float weight = adjacencyMatrix.GetValue(sourceIndex, col);

                if (weight != noEdgeValue)
                {
                    neighborNames.Add(vertexNames[col]);
                }
            }

            return neighborNames;
        }

        // --- Gestion des arcs ---

        /* Ajoute un arc allant du sommet nommé `sourceName` au sommet nommé `destinationName`, avec le poids `weight` (1 par défaut)
         * Si le graphe n'est pas orienté, ajoute aussi l'arc inverse, avec le même poids
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - il existe déjà un arc avec ces extrémités
         */
        public void AddEdge(string sourceName, string destinationName, float weight = 1)
        {
            if (!vertexIndices.ContainsKey(sourceName) || !vertexIndices.ContainsKey(destinationName))
            {
                throw new ArgumentException("Un des sommets n'existe pas.");
            }

            int srcIndex = vertexIndices[sourceName];
            int destIndex = vertexIndices[destinationName];

            if (adjacencyMatrix.GetValue(srcIndex, destIndex) != noEdgeValue)
            {
                throw new ArgumentException("L'arc existe déjà.");
            }

            adjacencyMatrix.SetValue(srcIndex, destIndex, weight);

            if (!directed)
            {
                if (srcIndex != destIndex)
                {
                    adjacencyMatrix.SetValue(destIndex, srcIndex, weight);
                }
            }
        }

        /* Supprime l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` du graphe
         * Si le graphe n'est pas orienté, supprime aussi l'arc inverse
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public void RemoveEdge(string sourceName, string destinationName)
        {
            if (!vertexIndices.ContainsKey(sourceName) || !vertexIndices.ContainsKey(destinationName))
            {
                throw new ArgumentException("Un des sommets n'existe pas.");
            }

            int srcIndex = vertexIndices[sourceName];
            int destIndex = vertexIndices[destinationName];

            if (adjacencyMatrix.GetValue(srcIndex, destIndex) == noEdgeValue)
            {
                throw new ArgumentException("L'arc n'existe pas.");
            }

            // Suppression (on remet la valeur par défaut "pas d'arc")
            adjacencyMatrix.SetValue(srcIndex, destIndex, noEdgeValue);

            if (!directed)
            {
                if (srcIndex != destIndex)
                {
                    adjacencyMatrix.SetValue(destIndex, srcIndex, noEdgeValue);
                }
            }
        }

        /* Renvoie le poids de l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName`
         * Si le graphe n'est pas orienté, GetEdgeWeight(A, B) = GetEdgeWeight(B, A) 
         * Lève une ArgumentException dans les cas suivants :
         * - un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         * - l'arc n'existe pas
         */
        public float GetEdgeWeight(string sourceName, string destinationName)
        {
            if (!vertexIndices.ContainsKey(sourceName) || !vertexIndices.ContainsKey(destinationName))
            {
                throw new ArgumentException("Un des sommets n'existe pas.");
            }

            int srcIndex = vertexIndices[sourceName];
            int destIndex = vertexIndices[destinationName];
            float val = adjacencyMatrix.GetValue(srcIndex, destIndex);

            if (val == noEdgeValue)
            {
                throw new ArgumentException("L'arc n'existe pas.");
            }

            return val;
        }

        /* Affecte le poids l'arc allant du sommet nommé `sourceName` au sommet nommé `destinationName` à `weight` 
         * Si le graphe n'est pas orienté, affecte le même poids à l'arc inverse
         * Lève une ArgumentException si un des sommets n'a pas été trouvé dans le graphe (source et/ou destination)
         */
        public void SetEdgeWeight(string sourceName, string destinationName, float weight)
        {
            
            if (!vertexIndices.ContainsKey(sourceName) || !vertexIndices.ContainsKey(destinationName))
            {
                throw new ArgumentException("Un des sommets n'existe pas.");
            }

            int srcIndex = vertexIndices[sourceName];
            int destIndex = vertexIndices[destinationName];

            adjacencyMatrix.SetValue(srcIndex, destIndex, weight);

            if (!directed)
            {
                if (srcIndex != destIndex)
                {
                    adjacencyMatrix.SetValue(destIndex, srcIndex, weight);
                }
            }
        }

        
    }
}