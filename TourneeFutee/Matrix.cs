namespace TourneeFutee
{
    public class Matrix
    {
        // TODO : ajouter tous les attributs que vous jugerez pertinents 

        /* Crée une matrice de dimensions `nbRows` x `nbColums`.
         * Toutes les cases de cette matrice sont remplies avec `defaultValue`.
         * Lève une ArgumentOutOfRangeException si une des dimensions est négative
         */


        private int nbRows;
        private int nbColumns;
        private float defaultValue;


        // Utilisation de List<List<float>> au lieu de float[,]
        // Cela permet une gestion plus rapide pour l'ajout de ligne et la suppression de ligne ( ce qui est demandé dans le cadre du programme est quelque chose qui demande plus d'ajout de ligne et de colonne que de Lecture écriture.
        // Ainsi dans notre cas du à la contrainte de l'exercice, nous partons sur une list de list plutot que tab[][] de float.
        // En effet si il y avait eu un graphe qui change peu, et beaucoup de demande de lecture, écriture, il aurait été judicieux de passé par un tab de float
      
        private List<List<float>> mat;

        public Matrix(int nbRows = 0, int nbColumns = 0, float defaultValue = 0)
        {
            // Vérification dimensions négatives
            if (nbRows < 0 || nbColumns < 0)
            {
                throw new ArgumentOutOfRangeException("Les dimensions ne peuvent pas être négatives.");
            }

            this.nbRows = nbRows;
            this.nbColumns = nbColumns;
            this.defaultValue = defaultValue;
            this.mat = new List<List<float>>(nbRows);

            
            // Complexité : O(NbRows * NbColumns). 
            for (int i = 0; i < nbRows; i++)
            {
                List<float> newRow = new List<float>(nbColumns);
                for (int j = 0; j < nbColumns; j++)
                {
                    newRow.Add(defaultValue);
                }
                this.mat.Add(newRow);
            }
        }

        // Propriété : valeur par défaut utilisée pour remplir les nouvelles cases
        // Lecture seule
        public float DefaultValue
        {
            get { return defaultValue; } // TODO : implémenter
                                         // pas de set
        }

        // Propriété : nombre de lignes
        // Lecture seule
        public int NbRows
        {
            get { return nbRows; } // TODO : implémenter
                                   // pas de set
        }

        // Propriété : nombre de colonnes
        // Lecture seule
        public int NbColumns
        {
            get { return nbColumns; } // TODO : implémenter
                                      // pas de set
        }

        /* Insère une ligne à l'indice `i`. Décale les lignes suivantes vers le bas.
         * Toutes les cases de la nouvelle ligne contiennent DefaultValue.
         * Si `i` = NbRows, insère une ligne en fin de matrice
         * Lève une ArgumentOutOfRangeException si `i` est en dehors des indices valides
         */
        public void AddRow(int i)
        {
            // TODO : implémenter
            if (i < 0 || i > nbRows)
            {
                throw new ArgumentOutOfRangeException("L'indice i est invalide.");
            }

            
            List<float> newRow = new List<float>(nbColumns);
            for (int k = 0; k < nbColumns; k++)
            {
                newRow.Add(defaultValue);
            }

            // Complexité : O(NbRows) (pour décaler les pointeurs) + O(NbColumns) (création ligne).
            mat.Insert(i, newRow);

            this.nbRows++;
        }

        /* Insère une colonne à l'indice `j`. Décale les colonnes suivantes vers la droite.
         * Toutes les cases de la nouvelle ligne contiennent DefaultValue.
         * Si `j` = NbColums, insère une colonne en fin de matrice
         * Lève une ArgumentOutOfRangeException si `j` est en dehors des indices valides
         */
        public void AddColumn(int j)
        {
            // TODO : implémenter
            if (j < 0 || j > nbColumns)
            {
                throw new ArgumentOutOfRangeException("L'indice j est invalide.");
            }

            // NOTE COMPLEXITÉ :
            // Complexité : O(NbRows * NbColumns).
            for (int i = 0; i < nbRows; i++)
            {
                mat[i].Insert(j, defaultValue);
            }

            this.nbColumns++;
        }

        // Supprime la ligne à l'indice `i`. Décale les lignes suivantes vers le haut.
        // Lève une ArgumentOutOfRangeException si `i` est en dehors des indices valides
        public void RemoveRow(int i)
        {
            // TODO : implémenter
            if (i < 0 || i >= nbRows)
            {
                throw new ArgumentOutOfRangeException("L'indice i est invalide.");
            }

              
            // Complexité : O(NbRows). On évite de recopier toutes les valeurs.
            mat.RemoveAt(i);

            this.nbRows--;
        }

        // Supprime la colonne à l'indice `j`. Décale les colonnes suivantes vers la gauche.
        // Lève une ArgumentOutOfRangeException si `j` est en dehors des indices valides
        public void RemoveColumn(int j)
        {
            // TODO : implémenter
            if (j < 0 || j >= nbColumns)
            {
                throw new ArgumentOutOfRangeException("L'indice j est invalide.");
            }

            
            // Complexité : O(NbRows * NbColumns).
            for (int i = 0; i < nbRows; i++)
            {
                mat[i].RemoveAt(j);
            }

            this.nbColumns--;
        }

        // Renvoie la valeur à la ligne `i` et colonne `j`
        // Lève une ArgumentOutOfRangeException si `i` ou `j` est en dehors des indices valides
        public float GetValue(int i, int j)
        {
            // TODO : implémenter
            if (i < 0 || i >= nbRows || j < 0 || j >= nbColumns)
            {
                throw new ArgumentOutOfRangeException("Indices hors limites.");
            }

            
            // Complexité : O(1) (Bien que normalement 0(2) mais négligeable).
            return mat[i][j];
        }

        // Affecte la valeur à la ligne `i` et colonne `j` à `v`
        // Lève une ArgumentOutOfRangeException si `i` ou `j` est en dehors des indices valides
        public void SetValue(int i, int j, float v)
        {
            // TODO : implémenter
            if (i < 0 || i >= nbRows || j < 0 || j >= nbColumns)
            {
                throw new ArgumentOutOfRangeException("Indices hors limites.");
            }

            mat[i][j] = v;
        }

        // Affiche la matrice
        public void Print()
        {
            // TODO : implémenter
            Console.WriteLine("Matrice (Liste de Listes) " + nbRows + "x " + nbColumns + " :");
            for (int i = 0; i < nbRows; i++)
            {
                for (int j = 0; j < nbColumns; j++)
                {
                    Console.Write(mat[i][j] + "\t");
                }
                Console.WriteLine();
            }
        }


        // TODO : ajouter toutes les méthodes que vous jugerez pertinentes 

    }


}