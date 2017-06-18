using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //for reading files

namespace AutocompleteMe
{
    class WordsDictionary
    {
        public static string[] wordsDictionary;

        public static void readDictionary(string fileName)
        {
            try
            {
                FileStream fr = new FileStream(fileName, FileMode.Open);
                StreamReader sr = new StreamReader(fr);
                int counter, index = 0;
                int.TryParse(sr.ReadLine(), out counter);
                wordsDictionary = new string[counter];
                while (sr.Peek() != -1)
                {
                    wordsDictionary[index] = sr.ReadLine().ToLower();
                    index++;
                }//end while

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }//end readDictionary

        private static int minNumberOfChanges(string s1, string s2)
        {
            try
            {
                int[,] storageMatrix = new int[s2.Length + 1, s1.Length + 1];
                storageMatrix[0, 0] = 0;
                int insertionsVariableA, deletionsVariableB, replacesVariableC;

                for (int i = 1; i < s1.Length + 1; i++)
                {
                    storageMatrix[0, i] = i;
                }
                for (int i = 1; i < s2.Length + 1; i++)
                {
                    storageMatrix[i, 0] = i;
                }
                for (int i = 1; i < s2.Length + 1; i++)
                {
                    for (int j = 1; j < s1.Length + 1; j++)
                    {
                        if (s2[i - 1] == s1[j - 1])
                        {
                            storageMatrix[i, j] = storageMatrix[i - 1, j - 1];
                        }
                        else
                        {
                            insertionsVariableA = storageMatrix[i - 1, j];
                            deletionsVariableB = storageMatrix[i, j - 1];
                            replacesVariableC = storageMatrix[i - 1, j - 1];
                            if (insertionsVariableA < deletionsVariableB)
                            {
                                if (insertionsVariableA < replacesVariableC)
                                {
                                    storageMatrix[i, j] = insertionsVariableA + 1;
                                }
                                else
                                {
                                    storageMatrix[i, j] = replacesVariableC + 1;
                                }
                            }
                            else
                            {
                                if (deletionsVariableB < replacesVariableC)
                                {
                                    storageMatrix[i, j] = deletionsVariableB + 1;
                                }
                                else
                                {
                                    storageMatrix[i, j] = replacesVariableC + 1;
                                }
                            }
                        }//end else

                    }//end j for
                }//end i for

                return storageMatrix[s2.Length, s1.Length];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }//end minNumberOfChanges

        public static Result[] closestWords(string userString) //O(N*userStringLength*eachWordLength)
        {
            Result[] results = new Result[wordsDictionary.Length];
            try
            {
                for (int i = 0; i < results.Length; i++) //O(N*userStringLength*eachWordLength)
                {
                    results[i] = new Result();
                    results[i].sentence = wordsDictionary[i];
                    results[i].weight = minNumberOfChanges(userString, results[i].sentence); //O(1 * (N*userStringLength*eachWordLength))
                }//end foreach

                Sort.mergeSort(ref results, 0, results.Length - 1);
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }//end closestWords

        public static void dictionaryMergeSort(string[] dic, int left, int right)
        {
            if (left < right)
            {
                int m = left + (right - left) / 2;

                dictionaryMergeSort(dic, left, m);
                dictionaryMergeSort(dic, m + 1, right);
                dictionaryMerge(dic, left, m, right);
            }//end if
        }//end mergesort

        private static void dictionaryMerge(string[] dic, int left, int mid, int right)
        {
            int i, j, k;
            int nLeft = mid - left + 1;
            int nright = right - mid;
            string[] arrLeft = new string[nLeft];
            string[] arrRight = new string[nright];

            for (i = 0; i < nLeft; i++)
                arrLeft[i] = dic[left + i];

            for (j = 0; j < nright; j++)
                arrRight[j] = dic[mid + 1 + j];

            i = 0;
            j = 0;
            k = left;

            while (i < nLeft && j < nright)
            {
                bool l = false, r = false;
                for (int x = 0; x < Math.Min(arrLeft[i].Length, arrRight[j].Length); x++)
                {
                    if (arrLeft[i][x] < arrRight[j][x])
                    {
                        l = true;
                        break;
                    }
                    else if (arrLeft[i][x] > arrRight[j][x])
                    {
                        r = true;
                        break;
                    }
                }
                if (l == true)
                {
                    dic[k] = arrLeft[i];
                    i++;
                }
                else if (r == true)
                {
                    dic[k] = arrRight[j];
                    j++;
                }
                else if (l == false && r == false)
                {
                    if (arrLeft[i].Length < arrRight[j].Length)
                    {
                        dic[k] = arrLeft[i];
                        i++;
                    }
                    else
                    {
                        dic[k] = arrRight[j];
                        j++;
                    }
                }
                k++;
            }

            while (i < nLeft)
            {
                dic[k] = arrLeft[i];
                i++;
                k++;
            }//end while

            while (j < nright)
            {
                dic[k] = arrRight[j];
                j++;
                k++;
            }//end while
        }//end merge

        public static int binarySearch(int size, string searchvalue)
        {
            int first = 0,        // First array element
            last = size,      // Last array element
            mid,                  // Mid point of search
            index = -1;           // Position of search value
            bool found = false;   // Flag

            while (!found && first <= last)
            {
                mid = (first + last) / 2;
                if (wordsDictionary[mid] == searchvalue)
                {
                    found = true;
                    index = mid;
                    return index;
                }

                else
                {
                    int length;
                    if (wordsDictionary[mid].Length < searchvalue.Length)
                        length = wordsDictionary[mid].Length;
                    else
                        length = searchvalue.Length;
                    bool sub = true;
                    for (int i = 0; i < length; i++)
                    {
                        if (searchvalue[i] < wordsDictionary[mid][i])
                        {
                            last = mid - 1;
                            sub = false;
                            break;
                        }
                        else if (searchvalue[i] > wordsDictionary[mid][i])
                        {
                            first = mid + 1;
                            sub = false;
                            break;
                        }
                    }
                    if (sub == true)
                    {
                        if (searchvalue.Length < wordsDictionary[mid].Length)
                        {
                            last = mid - 1;
                        }
                        else
                        {
                            first = mid + 1;
                        }
                    }
                }//end else
            }
            return index;
        }//end binarysearch

    }//end class
}