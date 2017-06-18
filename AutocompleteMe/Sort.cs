using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutocompleteMe
{
    public class Result
    {
        public string sentence { get; set; }
        public int weight { get; set; }
    }//end class
    class Sort
    {
        public static void mergeSort(ref Result[] arrOfResults, int left, int right)
        {
            if (left < right)
            {
                int m = left + (right - left) / 2;

                mergeSort(ref arrOfResults, left, m);
                mergeSort(ref arrOfResults, m + 1, right);
                merge(ref arrOfResults, left, m, right);
            }//end if
        }//end mergesort

        private static void merge(ref Result[] arrOfResults, int left, int mid, int right)
        {
            int i, j, k;
            int n1 = mid - left + 1;
            int n2 = right - mid;
            Result[] L = new Result[n1];
            Result[] R = new Result[n2];

            for (i = 0; i < n1; i++)
                L[i] = arrOfResults[left + i];

            for (j = 0; j < n2; j++)
                R[j] = arrOfResults[mid + 1 + j];

            i = 0;
            j = 0;
            k = left;

            while (i < n1 && j < n2)
            {
                if (L[i].weight >= R[j].weight)           // for decending way 
                {
                    arrOfResults[k] = L[i];
                    i++;
                }//end if
                else
                {
                    arrOfResults[k] = R[j];
                    j++;
                }//end else

                k++;
            }//end while

            while (i < n1)
            {
                arrOfResults[k] = L[i];
                i++;
                k++;
            }//end while

            while (j < n2)
            {
                arrOfResults[k] = R[j];
                j++;
                k++;
            }//end while
        }//end merge

        public static void bubbleSort(ref Result[] arrOfResults)
        {

            int length = arrOfResults.Length;
            Result temp = new Result();

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (arrOfResults[i].weight <= arrOfResults[j].weight)
                    {
                        temp = arrOfResults[i];
                        arrOfResults[i] = arrOfResults[j];
                        arrOfResults[j] = temp;
                    }//end if
                }//end nested for
            }//end for

        }//end bubblesort

    }//end class
}